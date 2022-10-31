using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Color.Attribute
{
	internal static class Definitions
	{
		#pragma warning disable 169
		#pragma warning disable IDE0051

		// > The field is never used
		// Reason: The field is used by MEF.

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Squares")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Squares;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Punct")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Punct;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.carries_dependency")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_carries_dependency;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.fallthrough")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_fallthrough;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.likely")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_likely;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.maybe_unused")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_maybe_unused;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.no_unique_address")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_no_unique_address;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.noreturn")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_noreturn;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.optimize_for_synchronized")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_optimize_for_synchronized;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.unlikely")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_unlikely;

		#region Attribute.Deprecated

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Deprecated.Mark")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Deprecated_Mark;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Deprecated.Reason")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Deprecated_Reason;

		#endregion
		#region Attribute.Nodiscard

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Nodiscard.Mark")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Nodiscard_Mark;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Nodiscard.Reason")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Nodiscard_Reason;

		#endregion
		#region Attribute.Assume

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Assume.Mark")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Assume_Mark;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Assume.Expression")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Assume_Expression;

		#endregion

		#pragma warning restore IDE0051
		#pragma warning restore 169
	}
}