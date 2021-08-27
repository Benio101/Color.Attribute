namespace Color.Attribute
{
	using Color = System.Windows.Media.Color;

	internal static class Default
	{
		internal static class Colors
		{
			private  static readonly Color Red       = Color.FromRgb(224, 128, 128);
			private  static readonly Color Yellow    = Color.FromRgb(224, 224, 128);
			private  static readonly Color Green     = Color.FromRgb(128, 224, 128);
			private  static readonly Color Blue      = Color.FromRgb(128, 176, 224);
			private  static readonly Color Violet    = Color.FromRgb(128, 128, 224);
			private  static readonly Color Gray      = Color.FromRgb(128, 128, 128);
			private  static readonly Color WhiteDark = Color.FromRgb(176, 176, 176);

			internal static readonly Color Punct     = Gray;
			internal static readonly Color Keyword   = Blue;
			internal static readonly Color Flow      = Violet;
			internal static readonly Color Positive  = Green;
			internal static readonly Color Warning   = Yellow;
			internal static readonly Color Negative  = Red;
			internal static readonly Color String    = Red;
			internal static readonly Color Plain     = WhiteDark;
		}
	}
}