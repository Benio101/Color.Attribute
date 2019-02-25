namespace Color.Attribute
{
	using Color = System.Windows.Media.Color;

	internal static class Default
	{
		internal static class Colors
		{
			private  static readonly Color Blue      = Color.FromRgb(128, 176, 224);
			private  static readonly Color BlueDark  = Color.FromRgb( 96, 128, 224);
			private  static readonly Color Gray      = Color.FromRgb(128, 128, 128);
			private  static readonly Color Green     = Color.FromRgb(176, 224, 128);
			private  static readonly Color Red       = Color.FromRgb(224, 128, 128);
			private  static readonly Color Yellow    = Color.FromRgb(224, 224, 128);
			private  static readonly Color RedDark   = Color.FromRgb(224,  96,  96);
			private  static readonly Color GreenDark = Color.FromRgb(128, 176,  96);
			private  static readonly Color Orange    = Color.FromRgb(224, 176, 128);
			private  static readonly Color WhiteDark = Color.FromRgb(176, 176, 176);

			internal static readonly Color Punct     = Gray;
			internal static readonly Color Keyword   = Blue;
			internal static readonly Color Flow      = BlueDark;
			internal static readonly Color Positive  = Green;
			internal static readonly Color Warning   = Yellow;
			internal static readonly Color Negative  = Red;
			internal static readonly Color Important = RedDark;
			internal static readonly Color String    = GreenDark;
			internal static readonly Color Param     = Orange;
			internal static readonly Color Plain     = WhiteDark;
		}
	}
}