using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Color.Attribute
{
	internal class Classifier
	:
		IClassifier
	{
		private          bool        IsClassificationRunning;
		private readonly IClassifier IClassifier;

		#pragma warning disable 67
		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
		#pragma warning restore 67

		private readonly Dictionary<string, IClassificationType> SingleAttributes       = new Dictionary<string, IClassificationType>();
		private readonly Dictionary<string, IClassificationType> CommonStyleDefinitions = new Dictionary<string, IClassificationType>();

		private readonly IClassificationType Attribute;

		#region Attribute.Deprecated

			private readonly IClassificationType Deprecated_Mark;
			private readonly IClassificationType Deprecated_Reason;

		#endregion
		#region Attribute.Nodiscard

			private readonly IClassificationType Nodiscard_Mark;
			private readonly IClassificationType Nodiscard_Reason;

		#endregion
		#region Attribute.Assume

		private readonly IClassificationType Assume_Mark;
		private readonly IClassificationType Assume_Expression;

		#endregion

		internal Classifier
		(
			IClassificationTypeRegistryService Registry,
			IClassifier                        Classifier
		)
		{
			IsClassificationRunning = false;
			IClassifier             = Classifier;
			Attribute               = Registry.GetClassificationType("Attribute");

			foreach (var Attr in Meta.SingleAttributesList)
				SingleAttributes.Add(Attr, Registry.GetClassificationType("Attribute." + Attr));

			foreach (var Definition in Meta.CommonStyleDefinitions)
				CommonStyleDefinitions.Add(Definition, Registry.GetClassificationType("Attribute." + Definition));

			#region Attribute.Deprecated

				Deprecated_Mark   = Registry.GetClassificationType("Attribute.Deprecated.Mark");
				Deprecated_Reason = Registry.GetClassificationType("Attribute.Deprecated.Reason");

			#endregion
			#region Attribute.Nodiscard

				Nodiscard_Mark    = Registry.GetClassificationType("Attribute.Nodiscard.Mark");
				Nodiscard_Reason  = Registry.GetClassificationType("Attribute.Nodiscard.Reason");

			#endregion
			#region Attribute.Assume

				Assume_Mark       = Registry.GetClassificationType("Attribute.Assume.Mark");
				Assume_Expression = Registry.GetClassificationType("Attribute.Assume.Expression");

			#endregion
		}

		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan Span)
		{
			if (IsClassificationRunning) return new List<ClassificationSpan>();

			try
			{
				IsClassificationRunning = true;
				return Classify(Span);
			}
			finally
			{
				IsClassificationRunning = false;
			}
		}

		private IList<ClassificationSpan> Classify(SnapshotSpan Span)
		{
			IList<ClassificationSpan> Spans = new List<ClassificationSpan>();

			if (Span.IsEmpty) return Spans;
			var Text = Span.GetText();

			foreach
			(
				Match Match in new Regex
				(
						@"(?<Squares_Open>\[{2})"
					+	 "(?<Attribute_List>.*?)"
					+	@"(?<Squares_Close>\]{2})"
				)
				.Matches(Text)
			)
			{
				// Filter out: comments, literals and strings.
				// Check "[[" and "]]" only, not attribute list inside Match
				// as it can contain comments, literals or strings as value.
				foreach (var Square in new[]{"Squares_Open", "Squares_Close"})
				{
					var MatchedSquares = new SnapshotSpan(Span.Snapshot, new Span(Span.Start + Match.Groups[Square].Index, Match.Groups[Square].Length));
					var Intersections  = IClassifier.GetClassificationSpans(MatchedSquares);

					foreach (var Intersection in Intersections)
					{
						var Classifications = Intersection.ClassificationType.Classification.Split(new[]{" - "}, StringSplitOptions.None);

						if
						(
							Utils.IsClassifiedAs(Classifications, new[]
							{
								PredefinedClassificationTypeNames.Comment,
								"XML Doc Comment", // Triple slash comment
								PredefinedClassificationTypeNames.Literal,
								PredefinedClassificationTypeNames.String
							})
						)
						{
							goto NextAttribute;
						}
					}
				}

				// Classify "[[" as "Attribute.Squares".
				Spans.Add(new ClassificationSpan(new SnapshotSpan
				(
					Span.Snapshot, new Span
					(
						Span.Start + Match.Groups["Squares_Open"].Index,
						Match.Groups["Squares_Open"].Length
					)), CommonStyleDefinitions["Squares"]
				));

				// Classify everything from "[[" to "]]" (inclusive) as "Attribute".
				Spans.Add(new ClassificationSpan(new SnapshotSpan
				(
					Span.Snapshot, new Span
					(
						Span.Start + Match.Index,
						Match.Length
					)), Attribute
				));

				// Classify "]]" as "Attribute.Squares".
				Spans.Add(new ClassificationSpan(new SnapshotSpan
				(
					Span.Snapshot, new Span
					(
						Span.Start + Match.Groups["Squares_Close"].Index,
						Match.Groups["Squares_Close"].Length
					)), CommonStyleDefinitions["Squares"]
				));

				// Attribute List (between "[[" and "]]").
				var AttributeList = Match.Groups["Attribute_List"].Value;

				// Attribute List offset (relative to Span.Start).
				var Offset = Match.Groups["Attribute_List"].Index;

				// Offset of iterated attribute (relative to @Offset).
				var Index = 0;

				// @AttributeList splitted by comma (","):
				// - Index1  Attribute's text
				// - Index2  Attribute's offset (used as `start` to construct new Span)
				var Attributes = new List<Tuple<string, int>>();

				// Split AttributeList by comma (",").
				foreach (Match Comma in new Regex("(?<Comma>,)").Matches(AttributeList))
				{
					Attributes.Add(new Tuple<string, int>
					(
						AttributeList.Substring(Index, Comma.Groups["Comma"].Index - Index),
						Span.Start + Offset + Index
					));

					Spans.Add(new ClassificationSpan(new SnapshotSpan
					(
						Span.Snapshot, new Span
						(
							Span.Start + Offset + Comma.Groups["Comma"].Index, 1
						)), CommonStyleDefinitions["Punct"]
					));

					Index += Comma.Groups["Comma"].Index - Index + 1;
				}

				// Add last attribute entry from AttributeList.
				// Fragment between last comma (",") and end of AttributeList ("]]").
				Attributes.Add(new Tuple<string, int>
				(
					AttributeList.Substring(Index), Span.Start + Offset + Index
				));

				foreach (var AttributeTuple in Attributes)
				{
					// Attribute entry (AttributeList fragment split by comma (","))
					// Eg " nodiscard" from "[[maybe_unused, nodiscard]]" (note heading space).
					var Entry = AttributeTuple.Item1;

					// Position of @Entry, see @Attributes.Index2.
					var Position = AttributeTuple.Item2;

					// Trim Entry (eg remove space from " nodiscard").
					foreach (Match Attr in new Regex
					(
							@"^[ \t\v\n\f]*"
						+	 "(?<Attribute>.*?)"
						+	@"[ \t\v\n\f]*$"
					)
						.Matches(Entry)
					)
					{
						Entry = Attr.Groups["Attribute"].Value;
						Position += Attr.Groups["Attribute"].Index;
					}

					// Ignore empty entries (eg after trailing comma at "[[nodiscard,]]")
					if (Entry.Length == 0) continue;

					// Filter attributes with `using` as they aren't standard.
					if (new Regex(@"^using[ \t\v\n\f]+").IsMatch(Entry)) goto NextAttribute;

					#region Single Attributes

					foreach (var Attr in Meta.SingleAttributesList)
					{
						foreach (Match Value in new Regex
						(
							"^(?<Attribute>" + Attr + ")$"
						)
							.Matches(Entry)
						)
						{
							Spans.Add(new ClassificationSpan(new SnapshotSpan
							(
								Span.Snapshot, new Span
								(
									Position + Value.Groups["Attribute"].Index,
									Value.Groups["Attribute"].Length
								)), SingleAttributes[Attr]
							));
						}
					}

					#endregion
					#region Attribute.Deprecated

					foreach (Match Value in new Regex
					(
							"^(?<Attribute>deprecated)"        // "deprecated"
						+	"("
						+		@"[ \t\v\n\f]*"
						+		@"(?<Paren_Open>\()"           // "("
						+		@"[ \t\v\n\f]*"
						+		@"(?<Reason>[^""]*""[^""]*"")" // string-literal (simplified)
						+		@"[ \t\v\n\f]*"
						+		@"(?<Paren_Close>\))"          // ")"
						+	")?$"
					)
						.Matches(Entry)
					)
					{
						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Attribute"].Index,
								Value.Groups["Attribute"].Length
							)), Deprecated_Mark
						));

						if (Value.Groups["Reason"].Length == 0) continue;

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Paren_Open"].Index,
								Value.Groups["Paren_Open"].Length
							)), CommonStyleDefinitions["Punct"]
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Reason"].Index,
								Value.Groups["Reason"].Length
							)), Deprecated_Reason
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Paren_Close"].Index,
								Value.Groups["Paren_Close"].Length
							)), CommonStyleDefinitions["Punct"]
						));
					}

					#endregion
					#region Attribute.Nodiscard

					foreach (Match Value in new Regex
					(
							"^(?<Attribute>nodiscard)"        // "nodiscard"
						+   "("
						+       @"[ \t\v\n\f]*"
						+       @"(?<Paren_Open>\()"           // "("
						+       @"[ \t\v\n\f]*"
						+       @"(?<Reason>[^""]*""[^""]*"")" // string-literal (simplified)
						+       @"[ \t\v\n\f]*"
						+       @"(?<Paren_Close>\))"          // ")"
						+   ")?$"
					)
						.Matches(Entry)
					) {
						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Attribute"].Index,
								Value.Groups["Attribute"].Length
							)), Nodiscard_Mark
						));

						if (Value.Groups["Reason"].Length == 0)
							continue;

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Paren_Open"].Index,
								Value.Groups["Paren_Open"].Length
							)), CommonStyleDefinitions["Punct"]
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Reason"].Index,
								Value.Groups["Reason"].Length
							)), Nodiscard_Reason
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Paren_Close"].Index,
								Value.Groups["Paren_Close"].Length
							)), CommonStyleDefinitions["Punct"]
						));
					}

					#endregion
					#region Attribute.Assume

					foreach (Match Value in new Regex
					(
							"^(?<Attribute>assume)"                                                            // "assume"
						+   "("
						+       @"[ \t\v\n\f]*"
						+       @"(?<Paren_Open>\()"                                                           // "("
						+       @"(?<Expression>[^()]*(?>(?>(?<c>\()[^()]*)+(?>(?<-c>\))[^()]*)+)*(?(c)(?!)))" // expression
						+       @"(?<Paren_Close>\))"                                                          // ")"
						+   ")?$"
					)
						.Matches(Entry)
					) {
						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Attribute"].Index,
								Value.Groups["Attribute"].Length
							)), Assume_Mark
						));

						if (Value.Groups["Expression"].Length == 0)
							continue;

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Paren_Open"].Index,
								Value.Groups["Paren_Open"].Length
							)), CommonStyleDefinitions["Punct"]
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Expression"].Index,
								Value.Groups["Expression"].Length
							)), Assume_Expression
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan
						(
							Span.Snapshot, new Span
							(
								Position + Value.Groups["Paren_Close"].Index,
								Value.Groups["Paren_Close"].Length
							)), CommonStyleDefinitions["Punct"]
						));
					}

					#endregion
				}

				NextAttribute:;
			}

			return Spans;
		}
	}
}