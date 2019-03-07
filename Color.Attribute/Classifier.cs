﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Color.Attribute
{
	internal class Classifier
		: IClassifier
	{
		private bool IsClassificationRunning;
		private IClassifier IClassifier;

		#pragma warning disable 67
		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
		#pragma warning restore 67

		private readonly Dictionary<string, IClassificationType> SingleAttributes =
			new Dictionary<string, IClassificationType>();

		private readonly Dictionary<string, IClassificationType> CommonStyleDefinitions =
			new Dictionary<string, IClassificationType>();

		readonly IClassificationType Attribute;

		#region Attribute.Deprecated

		readonly IClassificationType Deprecated_Mark;
		readonly IClassificationType Deprecated_Reason;

		#endregion
		#region Attribute.Contract

		readonly IClassificationType Contract_Expression;
		readonly IClassificationType Contract_Assert_Mark;
		readonly IClassificationType Contract_Ensures_Mark;
		readonly IClassificationType Contract_Ensures_Identifier;
		readonly IClassificationType Contract_Expects_Mark;
		readonly IClassificationType Contract_Level_Audit;
		readonly IClassificationType Contract_Level_Axiom;
		readonly IClassificationType Contract_Level_Default;

		#endregion

		internal Classifier(
			IClassificationTypeRegistryService Registry,
			IClassifier Classifier
		){
			IsClassificationRunning = false;
			IClassifier = Classifier;

			Attribute = Registry.GetClassificationType("Attribute");

			foreach (string Attribute in Meta.SingleAttributesList){
				SingleAttributes.Add(
					Attribute,
					Registry.GetClassificationType("Attribute." + Attribute)
				);
			}

			foreach (string Definition in Meta.CommonStyleDefinitions){
				CommonStyleDefinitions.Add(
					Definition,
					Registry.GetClassificationType("Attribute." + Definition)
				);
			}

			#region Attribute.Deprecated

			Deprecated_Mark =
				Registry.GetClassificationType("Attribute.Deprecated.Mark");

			Deprecated_Reason =
				Registry.GetClassificationType("Attribute.Deprecated.Reason");

			#endregion
			#region Attribute.Contract

			Contract_Expression =
				Registry.GetClassificationType("Attribute.Contract.Expression");

			Contract_Assert_Mark =
				Registry.GetClassificationType("Attribute.Contract.Assert.Mark");

			Contract_Ensures_Mark =
				Registry.GetClassificationType("Attribute.Contract.Ensures.Mark");

			Contract_Ensures_Identifier =
				Registry.GetClassificationType("Attribute.Contract.Ensures.Identifier");

			Contract_Expects_Mark =
				Registry.GetClassificationType("Attribute.Contract.Expects.Mark");

			Contract_Level_Audit =
				Registry.GetClassificationType("Attribute.Contract.Level.Audit");

			Contract_Level_Axiom =
				Registry.GetClassificationType("Attribute.Contract.Level.Axiom");

			Contract_Level_Default =
				Registry.GetClassificationType("Attribute.Contract.Level.Default");

			#endregion
		}

		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan Span){
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

		private IList<ClassificationSpan> Classify(SnapshotSpan Span){
			IList<ClassificationSpan> Spans = new List<ClassificationSpan>();

			if (Span.IsEmpty) return Spans;
			string Text = Span.GetText();

			foreach (Match Match in new Regex(
					@"(?<Squares_Open>\[{2})"
				+	@"(?<Attribute_List>.*)"
				+	@"(?<Squares_Close>\]{2})"
			).Matches(Text))
			{
				// Filter out: comments, literals and strings.
				// Check "[[" and "]]" only, not attribute list inside Match
				// as it can contain comments, literals or strings as value.
				foreach (string Square in new[]{"Squares_Open", "Squares_Close"}){
					var MatchedSquares = new SnapshotSpan(Span.Snapshot, new Span(
						Span.Start + Match.Groups[Square].Index,
						Match.Groups[Square].Length
					));

					var Intersections = IClassifier.GetClassificationSpans(MatchedSquares);
					foreach (ClassificationSpan Intersection in Intersections){
						var Classification = Intersection.ClassificationType.Classification;
						if(
								Classification == PredefinedClassificationTypeNames.Comment
							||	Classification == PredefinedClassificationTypeNames.Literal
							||	Classification == PredefinedClassificationTypeNames.String
						){
							goto NextAttribute;
						}
					}
				}

				// Classify "[[" as "Attribute.Squares".
				Spans.Add(new ClassificationSpan(new SnapshotSpan(
					Span.Snapshot, new Span(
						Span.Start + Match.Groups["Squares_Open"].Index,
						Match.Groups["Squares_Open"].Length
					)), CommonStyleDefinitions["Squares"]
				));

				// Classify Attribute List (between "[[" and "]]") as "Attribute".
				Spans.Add(new ClassificationSpan(new SnapshotSpan(
					Span.Snapshot, new Span(
						Span.Start + Match.Groups["Attribute_List"].Index,
						Match.Groups["Attribute_List"].Length
					)), Attribute
				));

				// Classify "]]" as "Attribute.Squares".
				Spans.Add(new ClassificationSpan(new SnapshotSpan(
					Span.Snapshot, new Span(
						Span.Start + Match.Groups["Squares_Close"].Index,
						Match.Groups["Squares_Close"].Length
					)), CommonStyleDefinitions["Squares"]
				));

				// Attribute List (between "[[" and "]]").
				string AttributeList = Match.Groups["Attribute_List"].Value;

				// Attribute List offset (relative to Span.Start).
				int Offset = Match.Groups["Attribute_List"].Index;

				// Offset of iterated attribute (relative to @Offset).
				int Index = 0;

				// @AttributeList splitted by comma (","):
				// - Index1  Attribute's text
				// - Index2  Attribute's offset (used as `start` to construct new Span)
				List<Tuple<string, int>> Attributes = new List<Tuple<string, int>>();

				// Split AttributeList by comma (",").
				foreach (Match Comma in new Regex(@"(?<Comma>,)").Matches(AttributeList)){
					Attributes.Add(new Tuple<string, int>(
						AttributeList.Substring(Index, Comma.Groups["Comma"].Index - Index),
						Span.Start + Offset + Index
					));

					Spans.Add(new ClassificationSpan(new SnapshotSpan(
						Span.Snapshot, new Span(
							Span.Start + Offset + Comma.Groups["Comma"].Index, 1
						)), CommonStyleDefinitions["Punct"]
					));

					Index += Comma.Groups["Comma"].Index - Index + 1;
				}

				// Add last attribute entry from AttributeList.
				// Fragment between last comma (",") and end of AttributeList ("]]").
				Attributes.Add(new Tuple<string, int>(
					AttributeList.Substring(Index), Span.Start + Offset + Index
				));

				foreach (var AttributeTuple in Attributes){
					// Attribute entry (AttributeList fragment split by comma (","))
					// Eg " nodiscard" from "[[maybe_unused, nodiscard]]" (note heading space).
					var Entry = AttributeTuple.Item1;

					// Position of @Entry, see @Attributes.Index2.
					var Position = AttributeTuple.Item2;

					// Trim Entry (eg remove space from " nodiscard").
					foreach(Match Attribute in new Regex(
							@"^[ \t\v\n\f]*"
						+	@"(?<Attribute>.*?)"
						+	@"[ \t\v\n\f]*$"
					).Matches(Entry))
					{
						Entry = Attribute.Groups["Attribute"].Value;
						Position += Attribute.Groups["Attribute"].Index;
					}

					// Ignore empty entries (eg after trailing comma at "[[nodiscard,]]")
					if (Entry.Length == 0) continue;

					// Filter attributes with `using` as they aren't standard.
					if (new Regex(@"^using[ \t\v\n\f]+").IsMatch(Entry)) goto NextAttribute;

					#region Single Attributes

					foreach (string Attribute in Meta.SingleAttributesList){
						foreach (Match Value in new Regex(
							@"^(?<Attribute>" + Attribute + @")$"
						).Matches(Entry))
						{
							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Attribute"].Index,
									Value.Groups["Attribute"].Length
								)), SingleAttributes[Attribute]
							));
						}
					}

					#endregion
					#region Attribute.Deprecated

					foreach (Match Value in new Regex(
							@"^(?<Attribute>deprecated)"       // "deprecated"
						+	@"("
						+		@"[ \t\v\n\f]*"
						+		@"(?<Paren_Open>\()"           // "("
						+		@"[ \t\v\n\f]*"
						+		@"(?<Reason>[^""]*""[^""]*"")" // string-literal (simplified)
						+		@"[ \t\v\n\f]*"
						+		@"(?<Paren_Close>\))"          // ")"
						+	@")?$"
					).Matches(Entry))
					{
						Spans.Add(new ClassificationSpan(new SnapshotSpan(
							Span.Snapshot, new Span(
								Position + Value.Groups["Attribute"].Index,
								Value.Groups["Attribute"].Length
							)), Deprecated_Mark
						));

						if (Value.Groups["Reason"].Length > 0)
						{
							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Paren_Open"].Index,
									Value.Groups["Paren_Open"].Length
								)), CommonStyleDefinitions["Punct"]
							));

							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Reason"].Index,
									Value.Groups["Reason"].Length
								)), Deprecated_Reason
							));

							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Paren_Close"].Index,
									Value.Groups["Paren_Close"].Length
								)), CommonStyleDefinitions["Punct"]
							));
						}
					}

					#endregion
					#region Attribute.Contract

					foreach (string Attribute in Meta.SingleContractAttributeList){
						IClassificationType ClassificationMark = Contract_Assert_Mark;
						if (Attribute == "expects") ClassificationMark = Contract_Expects_Mark;

						foreach (Match Value in new Regex(
								@"^(?<Attribute>" + Attribute + @")"   // attribute-token
							+	@"("
							+		@"[ \t\v\n\f]+"
							+		@"(?<Level>(audit|axiom|default))" // contract-level
							+	@")?"
							+	@"[ \t\v\n\f]*"
							+	@"(?<Punct>:)"                         // ":"
							+	@"(?<Expression>([^\]]|\](?!\]))+)$"   // expression (simplified)
						).Matches(Entry))
						{
							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Attribute"].Index,
									Value.Groups["Attribute"].Length
								)), ClassificationMark
							));

							if (Value.Groups["Level"].Length > 0){
								string Level = Value.Groups["Level"].Value;

								IClassificationType ClassificationLevel = Contract_Level_Default;
								if (Level == "audit") ClassificationLevel = Contract_Level_Audit;
								if (Level == "axiom") ClassificationLevel = Contract_Level_Axiom;

								Spans.Add(new ClassificationSpan(new SnapshotSpan(
									Span.Snapshot, new Span(
										Position + Value.Groups["Level"].Index,
										Value.Groups["Level"].Length
									)), ClassificationLevel
								));
							}

							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Punct"].Index,
									Value.Groups["Punct"].Length
								)), CommonStyleDefinitions["Punct"]
							));

							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Expression"].Index,
									Value.Groups["Expression"].Length
								)), Contract_Expression
							));
						}
					}

					#endregion
					#region Attribute.Contract.Ensures

					foreach (Match Value in new Regex(
							@"^(?<Attribute>ensures)"                        // "ensures"
						+	@"("
						+		@"[ \t\v\n\f]+"
						+		@"(?<Level>(audit|axiom|default))"           // contract-level
						+	@")?"
						+	@"("
						+		@"[ \t\v\n\f]+"
						+		@"(?<Identifier>" + Utils.Identifier + @")?" // identifier
						+	@")?"
						+	@"[ \t\v\n\f]*"
						+	@"(?<Punct>:)"                                   // ":"
						+	@"(?<Expression>([^\]]|\](?!\]))+)$"             // expression (s-fied)
					).Matches(Entry))
					{
						Spans.Add(new ClassificationSpan(new SnapshotSpan(
							Span.Snapshot, new Span(
								Position + Value.Groups["Attribute"].Index,
								Value.Groups["Attribute"].Length
							)), Contract_Ensures_Mark
						));

						if (Value.Groups["Level"].Length > 0){
							string Level = Value.Groups["Level"].Value;

							IClassificationType ClassificationLevel = Contract_Level_Default;
							if (Level == "audit") ClassificationLevel = Contract_Level_Audit;
							if (Level == "axiom") ClassificationLevel = Contract_Level_Axiom;

							Spans.Add(new ClassificationSpan(new SnapshotSpan(
								Span.Snapshot, new Span(
									Position + Value.Groups["Level"].Index,
									Value.Groups["Level"].Length
								)), ClassificationLevel
							));
						}

						if (Value.Groups["Identifier"].Length > 0)
						Spans.Add(new ClassificationSpan(new SnapshotSpan(
							Span.Snapshot, new Span(
								Position + Value.Groups["Identifier"].Index,
								Value.Groups["Identifier"].Length
							)), Contract_Ensures_Identifier
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan(
							Span.Snapshot, new Span(
								Position + Value.Groups["Punct"].Index,
								Value.Groups["Punct"].Length
							)), CommonStyleDefinitions["Punct"]
						));

						Spans.Add(new ClassificationSpan(new SnapshotSpan(
							Span.Snapshot, new Span(
								Position + Value.Groups["Expression"].Index,
								Value.Groups["Expression"].Length
							)), Contract_Expression
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