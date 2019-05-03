using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Color.Attribute
{
	internal static class Definitions
	{
		// > The field is never used
		// Reason The field is used by MEF.
		#pragma warning disable 169
		#pragma warning disable IDE0051

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
		[Name("Attribute.nodiscard")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_nodiscard;

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
		#region Attribute.Contract

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Expression")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Expression;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Assert.Mark")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Assert_Mark;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Ensures.Mark")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Ensures_Mark;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Ensures.Identifier")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Ensures_Identifier;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Expects.Mark")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Expects_Mark;

		#region Attribute.Contract.Level

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Level.Audit")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Level_Audit;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Level.Axiom")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Level_Axiom;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name("Attribute.Contract.Level.Default")]
		private static readonly ClassificationTypeDefinition
		Definition_Attribute_Contract_Level_Default;

		#endregion
		#endregion
		
		#pragma warning restore IDE0051
		#pragma warning restore 169
	}
}