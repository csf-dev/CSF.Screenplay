using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Static type containing the pre-defined colors for the web.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each static property of this type corresponds to a well-known pre-defined color, according to
    /// <see href="http://www.w3.org/TR/css3-color/#html4">the W3C HTML4 spec</see>.
    /// </para>
    /// </remarks>
    public static class Colors
    {
        /// <summary>
        /// Caches the named colors and their names, for performance reasons when using <see cref="GetNamedColor(string)"/> and/or
        /// <see cref="TryGetNamedColor(string, out Color)"/>.
        /// </summary>
        static readonly ReadOnlyDictionary<string,Color> namedColors;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Color TRANSPARENT => new Color(0, 0, 0, 0);
        public static Color ALICEBLUE => new Color(240, 248, 255, 1);
        public static Color ANTIQUEWHITE => new Color(250, 235, 215, 1);
        public static Color AQUA => new Color(0, 255, 255, 1);
        public static Color AQUAMARINE => new Color(127, 255, 212, 1);
        public static Color AZURE => new Color(240, 255, 255, 1);
        public static Color BEIGE => new Color(245, 245, 220, 1);
        public static Color BISQUE => new Color(255, 228, 196, 1);
        public static Color BLACK => new Color(0, 0, 0, 1);
        public static Color BLANCHEDALMOND => new Color(255, 235, 205, 1);
        public static Color BLUE => new Color(0, 0, 255, 1);
        public static Color BLUEVIOLET => new Color(138, 43, 226, 1);
        public static Color BROWN => new Color(165, 42, 42, 1);
        public static Color BURLYWOOD => new Color(222, 184, 135, 1);
        public static Color CADETBLUE => new Color(95, 158, 160, 1);
        public static Color CHARTREUSE => new Color(127, 255, 0, 1);
        public static Color CHOCOLATE => new Color(210, 105, 30, 1);
        public static Color CORAL => new Color(255, 127, 80, 1);
        public static Color CORNFLOWERBLUE => new Color(100, 149, 237, 1);
        public static Color CORNSILK => new Color(255, 248, 220, 1);
        public static Color CRIMSON => new Color(220, 20, 60, 1);
        public static Color CYAN => new Color(0, 255, 255, 1);
        public static Color DARKBLUE => new Color(0, 0, 139, 1);
        public static Color DARKCYAN => new Color(0, 139, 139, 1);
        public static Color DARKGOLDENROD => new Color(184, 134, 11, 1);
        public static Color DARKGRAY => new Color(169, 169, 169, 1);
        public static Color DARKGREEN => new Color(0, 100, 0, 1);
        public static Color DARKGREY => new Color(169, 169, 169, 1);
        public static Color DARKKHAKI => new Color(189, 183, 107, 1);
        public static Color DARKMAGENTA => new Color(139, 0, 139, 1);
        public static Color DARKOLIVEGREEN => new Color(85, 107, 47, 1);
        public static Color DARKORANGE => new Color(255, 140, 0, 1);
        public static Color DARKORCHID => new Color(153, 50, 204, 1);
        public static Color DARKRED => new Color(139, 0, 0, 1);
        public static Color DARKSALMON => new Color(233, 150, 122, 1);
        public static Color DARKSEAGREEN => new Color(143, 188, 143, 1);
        public static Color DARKSLATEBLUE => new Color(72, 61, 139, 1);
        public static Color DARKSLATEGRAY => new Color(47, 79, 79, 1);
        public static Color DARKSLATEGREY => new Color(47, 79, 79, 1);
        public static Color DARKTURQUOISE => new Color(0, 206, 209, 1);
        public static Color DARKVIOLET => new Color(148, 0, 211, 1);
        public static Color DEEPPINK => new Color(255, 20, 147, 1);
        public static Color DEEPSKYBLUE => new Color(0, 191, 255, 1);
        public static Color DIMGRAY => new Color(105, 105, 105, 1);
        public static Color DIMGREY => new Color(105, 105, 105, 1);
        public static Color DODGERBLUE => new Color(30, 144, 255, 1);
        public static Color FIREBRICK => new Color(178, 34, 34, 1);
        public static Color FLORALWHITE => new Color(255, 250, 240, 1);
        public static Color FORESTGREEN => new Color(34, 139, 34, 1);
        public static Color FUCHSIA => new Color(255, 0, 255, 1);
        public static Color GAINSBORO => new Color(220, 220, 220, 1);
        public static Color GHOSTWHITE => new Color(248, 248, 255, 1);
        public static Color GOLD => new Color(255, 215, 0, 1);
        public static Color GOLDENROD => new Color(218, 165, 32, 1);
        public static Color GRAY => new Color(128, 128, 128, 1);
        public static Color GREY => new Color(128, 128, 128, 1);
        public static Color GREEN => new Color(0, 128, 0, 1);
        public static Color GREENYELLOW => new Color(173, 255, 47, 1);
        public static Color HONEYDEW => new Color(240, 255, 240, 1);
        public static Color HOTPINK => new Color(255, 105, 180, 1);
        public static Color INDIANRED => new Color(205, 92, 92, 1);
        public static Color INDIGO => new Color(75, 0, 130, 1);
        public static Color IVORY => new Color(255, 255, 240, 1);
        public static Color KHAKI => new Color(240, 230, 140, 1);
        public static Color LAVENDER => new Color(230, 230, 250, 1);
        public static Color LAVENDERBLUSH => new Color(255, 240, 245, 1);
        public static Color LAWNGREEN => new Color(124, 252, 0, 1);
        public static Color LEMONCHIFFON => new Color(255, 250, 205, 1);
        public static Color LIGHTBLUE => new Color(173, 216, 230, 1);
        public static Color LIGHTCORAL => new Color(240, 128, 128, 1);
        public static Color LIGHTCYAN => new Color(224, 255, 255, 1);
        public static Color LIGHTGOLDENRODYELLOW => new Color(250, 250, 210, 1);
        public static Color LIGHTGRAY => new Color(211, 211, 211, 1);
        public static Color LIGHTGREEN => new Color(144, 238, 144, 1);
        public static Color LIGHTGREY => new Color(211, 211, 211, 1);
        public static Color LIGHTPINK => new Color(255, 182, 193, 1);
        public static Color LIGHTSALMON => new Color(255, 160, 122, 1);
        public static Color LIGHTSEAGREEN => new Color(32, 178, 170, 1);
        public static Color LIGHTSKYBLUE => new Color(135, 206, 250, 1);
        public static Color LIGHTSLATEGRAY => new Color(119, 136, 153, 1);
        public static Color LIGHTSLATEGREY => new Color(119, 136, 153, 1);
        public static Color LIGHTSTEELBLUE => new Color(176, 196, 222, 1);
        public static Color LIGHTYELLOW => new Color(255, 255, 224, 1);
        public static Color LIME => new Color(0, 255, 0, 1);
        public static Color LIMEGREEN => new Color(50, 205, 50, 1);
        public static Color LINEN => new Color(250, 240, 230, 1);
        public static Color MAGENTA => new Color(255, 0, 255, 1);
        public static Color MAROON => new Color(128, 0, 0, 1);
        public static Color MEDIUMAQUAMARINE => new Color(102, 205, 170, 1);
        public static Color MEDIUMBLUE => new Color(0, 0, 205, 1);
        public static Color MEDIUMORCHID => new Color(186, 85, 211, 1);
        public static Color MEDIUMPURPLE => new Color(147, 112, 219, 1);
        public static Color MEDIUMSEAGREEN => new Color(60, 179, 113, 1);
        public static Color MEDIUMSLATEBLUE => new Color(123, 104, 238, 1);
        public static Color MEDIUMSPRINGGREEN => new Color(0, 250, 154, 1);
        public static Color MEDIUMTURQUOISE => new Color(72, 209, 204, 1);
        public static Color MEDIUMVIOLETRED => new Color(199, 21, 133, 1);
        public static Color MIDNIGHTBLUE => new Color(25, 25, 112, 1);
        public static Color MINTCREAM => new Color(245, 255, 250, 1);
        public static Color MISTYROSE => new Color(255, 228, 225, 1);
        public static Color MOCCASIN => new Color(255, 228, 181, 1);
        public static Color NAVAJOWHITE => new Color(255, 222, 173, 1);
        public static Color NAVY => new Color(0, 0, 128, 1);
        public static Color OLDLACE => new Color(253, 245, 230, 1);
        public static Color OLIVE => new Color(128, 128, 0, 1);
        public static Color OLIVEDRAB => new Color(107, 142, 35, 1);
        public static Color ORANGE => new Color(255, 165, 0, 1);
        public static Color ORANGERED => new Color(255, 69, 0, 1);
        public static Color ORCHID => new Color(218, 112, 214, 1);
        public static Color PALEGOLDENROD => new Color(238, 232, 170, 1);
        public static Color PALEGREEN => new Color(152, 251, 152, 1);
        public static Color PALETURQUOISE => new Color(175, 238, 238, 1);
        public static Color PALEVIOLETRED => new Color(219, 112, 147, 1);
        public static Color PAPAYAWHIP => new Color(255, 239, 213, 1);
        public static Color PEACHPUFF => new Color(255, 218, 185, 1);
        public static Color PERU => new Color(205, 133, 63, 1);
        public static Color PINK => new Color(255, 192, 203, 1);
        public static Color PLUM => new Color(221, 160, 221, 1);
        public static Color POWDERBLUE => new Color(176, 224, 230, 1);
        public static Color PURPLE => new Color(128, 0, 128, 1);
        public static Color REBECCAPURPLE => new Color(102, 51, 153, 1);
        public static Color RED => new Color(255, 0, 0, 1);
        public static Color ROSYBROWN => new Color(188, 143, 143, 1);
        public static Color ROYALBLUE => new Color(65, 105, 225, 1);
        public static Color SADDLEBROWN => new Color(139, 69, 19, 1);
        public static Color SALMON => new Color(250, 128, 114, 1);
        public static Color SANDYBROWN => new Color(244, 164, 96, 1);
        public static Color SEAGREEN => new Color(46, 139, 87, 1);
        public static Color SEASHELL => new Color(255, 245, 238, 1);
        public static Color SIENNA => new Color(160, 82, 45, 1);
        public static Color SILVER => new Color(192, 192, 192, 1);
        public static Color SKYBLUE => new Color(135, 206, 235, 1);
        public static Color SLATEBLUE => new Color(106, 90, 205, 1);
        public static Color SLATEGRAY => new Color(112, 128, 144, 1);
        public static Color SLATEGREY => new Color(112, 128, 144, 1);
        public static Color SNOW => new Color(255, 250, 250, 1);
        public static Color SPRINGGREEN => new Color(0, 255, 127, 1);
        public static Color STEELBLUE => new Color(70, 130, 180, 1);
        public static Color TAN => new Color(210, 180, 140, 1);
        public static Color TEAL => new Color(0, 128, 128, 1);
        public static Color THISTLE => new Color(216, 191, 216, 1);
        public static Color TOMATO => new Color(255, 99, 71, 1);
        public static Color TURQUOISE => new Color(64, 224, 208, 1);
        public static Color VIOLET => new Color(238, 130, 238, 1);
        public static Color WHEAT => new Color(245, 222, 179, 1);
        public static Color WHITE => new Color(255, 255, 255, 1);
        public static Color WHITESMOKE => new Color(245, 245, 245, 1);
        public static Color YELLOW => new Color(255, 255, 0, 1);
        public static Color YELLOWGREEN => new Color(154, 205, 50, 1);
 #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Gets a <see cref="Color"/> instance which matches the specified color-name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will throw an exception if <paramref name="colorName"/> does not correspond to a well-known color
        /// name.  This is a case-insensitive match.
        /// If you are uncertain whether the <paramref name="colorName"/> is a well-known color name or not then consider
        /// <see cref="TryGetNamedColor(string, out Color)"/> instead.
        /// </para>
        /// </remarks>
        /// <param name="colorName">A named color (case insensitive)</param>
        /// <returns>A <see cref="Color"/> which corresponds to the specified named color.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="colorName"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">If <paramref name="colorName"/> does not correspond to a well-known named color</exception>
        public static Color GetNamedColor(string colorName)
        {
            if (colorName is null) throw new ArgumentNullException(nameof(colorName));
            if(namedColors.TryGetValue(colorName, out var color)) return color;
            throw new ArgumentException($"'{colorName}' is not a well-known named color.");
        }

        /// <summary>
        /// Gets a value indicating whether <paramref name="colorName"/> is a well-known color name.
        /// Exposes a <see cref="Color"/> instance which corresponds to that name if it is.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will never throw an exception for any value of <paramref name="colorName"/>.
        /// Note that match with a well-known color name is performed in a case-insensitive manner.
        /// </para>
        /// </remarks>
        /// <param name="colorName">A string which might correspond to a well-known color name.</param>
        /// <param name="color">If this method returns <see langword="true"/> then this is a <see cref="Color"/>
        /// which corresponds to the color indicated by <paramref name="colorName"/>; if it returns <see langword="false"/>
        /// then this value is undefined and must be ignored.</param>
        /// <returns><see langword="true"/> if <paramref name="colorName"/> is a well-known color name; <see langword="false"/> if not.</returns>
        public static bool TryGetNamedColor(string colorName, out Color color)
        {
            color = BLACK;
            if (colorName is null) return false;
            return namedColors.TryGetValue(colorName, out color);
        }

        /// <summary>
        /// Static constructor initializes a cache of the named colors.
        /// </summary>
        static Colors()
        {
            var namedColorValues = typeof(Colors)
                .GetProperties(BindingFlags.Static)
                .ToDictionary(k => k.Name, v => (Color) v.GetValue(null), StringComparer.InvariantCultureIgnoreCase);
            namedColors = new ReadOnlyDictionary<string, Color>(namedColorValues);
        }
   }
}