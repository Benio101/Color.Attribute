using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Color.Attribute
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute")]
	[Name("Attribute")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = Priority.High)]
	internal sealed class Format_Attribute
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute()
		{
			DisplayName = "C++ Attribute";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Plain;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Squares")]
	[Name("Attribute.Squares")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Squares
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Squares()
		{
			DisplayName = "C++ Attribute: Punctuation: \"[[\" & \"]]\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Punct;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Punct")]
	[Name("Attribute.Punct")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Punct
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Punct()
		{
			DisplayName = "C++ Attribute: Punctuation";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Punct;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.carries_dependency")]
	[Name("Attribute.carries_dependency")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_carries_dependency
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_carries_dependency()
		{
			DisplayName = "C++ Attribute: \"carries_dependency\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Keyword;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.fallthrough")]
	[Name("Attribute.fallthrough")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_fallthrough
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_fallthrough()
		{
			DisplayName = "C++ Attribute: \"fallthrough\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Flow;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.likely")]
	[Name("Attribute.likely")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_likely
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_likely()
		{
			DisplayName = "C++ Attribute: \"likely\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Positive;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.maybe_unused")]
	[Name("Attribute.maybe_unused")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_maybe_unused
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_maybe_unused()
		{
			DisplayName = "C++ Attribute: \"maybe_unused\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Warning;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.no_unique_address")]
	[Name("Attribute.no_unique_address")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_no_unique_address
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_no_unique_address()
		{
			DisplayName = "C++ Attribute: \"no_unique_address\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Keyword;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.nodiscard")]
	[Name("Attribute.nodiscard")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_nodiscard
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_nodiscard()
		{
			DisplayName = "C++ Attribute: \"nodiscard\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Keyword;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.noreturn")]
	[Name("Attribute.noreturn")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_noreturn
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_noreturn()
		{
			DisplayName = "C++ Attribute: \"noreturn\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Warning;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.optimize_for_synchronized")]
	[Name("Attribute.optimize_for_synchronized")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_optimize_for_synchronized
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_optimize_for_synchronized()
		{
			DisplayName = "C++ Attribute: \"optimize_for_synchronized\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Keyword;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.unlikely")]
	[Name("Attribute.unlikely")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_unlikely
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_unlikely()
		{
			DisplayName = "C++ Attribute: \"unlikely\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Negative;
		}
	}

	#region Attribute.Deprecated

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Deprecated.Mark")]
	[Name("Attribute.Deprecated.Mark")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Deprecated_Mark
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Deprecated_Mark()
		{
			DisplayName = "C++ Attribute: \"deprecated\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Warning;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Deprecated.Reason")]
	[Name("Attribute.Deprecated.Reason")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Deprecated_Reason
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Deprecated_Reason()
		{
			DisplayName = "C++ Attribute: \"deprecated\": Reason";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.String;
		}
	}

	#endregion
	#region Attribute.Contract

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Expression")]
	[Name("Attribute.Contract.Expression")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Expression
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Expression()
		{
			DisplayName = "C++ Attribute: Contract: Expression";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Plain;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Assert.Mark")]
	[Name("Attribute.Contract.Assert.Mark")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Assert
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Assert()
		{
			DisplayName = "C++ Attribute: Contract: \"assert\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Warning;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Ensures.Mark")]
	[Name("Attribute.Contract.Ensures.Mark")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Ensures_Mark
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Ensures_Mark()
		{
			DisplayName = "C++ Attribute: Contract: \"ensures\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Positive;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Ensures.Identifier")]
	[Name("Attribute.Contract.Ensures.Identifier")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Ensures_Identifier
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Ensures_Identifier()
		{
			DisplayName = "C++ Attribute: Contract: \"ensures\": Identifier";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Plain;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Expects.Mark")]
	[Name("Attribute.Contract.Expects.Mark")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Expects
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Expects()
		{
			DisplayName = "C++ Attribute: Contract: \"expects\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Negative;
		}
	}

	#region Attribute.Contract.Level

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Level.Default")]
	[Name("Attribute.Contract.Level.Default")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Level_Default
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Level_Default()
		{
			DisplayName = "C++ Attribute: Contract: Level: \"default\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Negative;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Level.Audit")]
	[Name("Attribute.Contract.Level.Audit")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Level_Audit
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Level_Audit()
		{
			DisplayName = "C++ Attribute: Contract: Level: \"audit\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Warning;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = "Attribute.Contract.Level.Axiom")]
	[Name("Attribute.Contract.Level.Axiom")]
	[BaseDefinition(PredefinedClassificationTypeNames.Other)]
	[UserVisible(true)]
	[Order(After = "Attribute")]
	internal sealed class Format_Attribute_Contract_Level_Axiom
	:
		ClassificationFormatDefinition
	{
		public Format_Attribute_Contract_Level_Axiom()
		{
			DisplayName = "C++ Attribute: Contract: Level: \"axiom\"";

			BackgroundCustomizable = false;
			ForegroundColor = Default.Colors.Positive;
		}
	}

	#endregion
	#endregion
}