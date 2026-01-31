using System;
using SysColor = System.Drawing.Color;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Immutable type represents a color on the web, in a manner which may be converted and compared between web-supported formats.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An uninitialized instance of this type will represent "fully-transparent black".  This is equivalent to the value available at
    /// <see cref="Colors.TRANSPARENT"/>.
    /// Internally, this type stores color as three unsigned <see cref="byte"/> values (Red, Green, Blue) and a
    /// <see cref="double"/> representing the transparency (Alpha).  The Alpha value must be between 0 (fully transparent)
    /// and 1 (fully opaque).
    /// </para>
    /// <para>
    /// For a list of the well-known named colors, such as <c>green</c> or <c>rosybrown</c>, see the <see cref="Colors"/> class.
    /// This has static properties to get instances of <see cref="Color"/> for each of these.
    /// It also has methods to get a color based upon a string color name.
    /// </para>
    /// <para>
    /// This struct contains functionality to compare equality with, and convert to/from the .NET built-in <see cref="System.Drawing.Color"/>.
    /// It also contains parsing logic to parse/format an instance from/to string representations of color which are used by web browsers.
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/Reference/Values/color_value">the MDN writeup of web color</see> for
    /// more information about the valid formats.
    /// </para>
    /// <para>
    /// When using a sufficiently high version of .NET &amp; the C# language (.NET 6 or higher), this type is a <c>readonly record struct</c>.
    /// This offers improved and extended functionality, such as access to
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#nondestructive-mutation">non-destructive
    /// mutation using the <c>with</c> keyword</see>.  In lower .NET/C# versions, this type is a plain <c>struct</c> with functionality
    /// supported by that language version.
    /// </para>
    /// </remarks>
#if RECORD_STRUCT_SUPPORT
    public readonly record struct Color
#else
    public struct Color
#endif
        : IEquatable<SysColor>, IEquatable<Color>, IEquatable<string>
    {
        readonly double alpha;

        /// <summary>
        /// Gets the red component in the sRGB color space.
        /// </summary>
#if RECORD_STRUCT_SUPPORT
        public readonly byte Red { get; init; }
#else
        public byte Red { get; private set; }
#endif

        /// <summary>
        /// Gets the green component in the sRGB color space.
        /// </summary>
#if RECORD_STRUCT_SUPPORT
        public readonly byte Green { get; init; }
#else
        public byte Green { get; private set; }
#endif

        /// <summary>
        /// Gets the blue component in the sRGB color space.
        /// </summary>
#if RECORD_STRUCT_SUPPORT
        public readonly byte Blue { get; init; }
#else
        public byte Blue { get; private set; }
#endif

        /// <summary>
        /// Gets the alpha (transparency) component in the sRGB color space.
        /// </summary>
        /// <remarks>
        /// <para>The value of this property will always be between zero (fully transparent) and one (fully opaque).</para>
        /// </remarks>
#if RECORD_STRUCT_SUPPORT
        public readonly double Alpha
        {
            get => alpha;
            init => this.alpha = SanitizeAlpha(value);
        }
#else
        public double Alpha { get => alpha; }
#endif

        /// <summary>
        /// Gets a representation of <see cref="Alpha"/>, as a byte.
        /// </summary>
        public byte AlphaAsByte => (byte) Math.Floor(Math.Round(Alpha * 255, 0, MidpointRounding.AwayFromZero));

        /// <summary>
        /// Converts this color to a <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In some situations, it might be useful to convert to/from the .NET built-in <see cref="System.Drawing.Color"/>
        /// type.  Use this method and <see cref="FromSystemDrawingColour"/> to do so.
        /// </para>
        /// </remarks>
        /// <returns>A <see cref="System.Drawing.Color"/> representation of this color.</returns>
        public SysColor ToSystemDrawingColor() => SysColor.FromArgb(AlphaAsByte, Red, Green, Blue);

        /// <inheritdoc/>
        public bool Equals(SysColor other) => ToSystemDrawingColor().Equals(other);

        /// <inheritdoc/>
        public bool Equals(Color other) => other.Red == Red
                                        && other.Green == Green
                                        && other.Blue == Blue
                                        && other.AlphaAsByte == AlphaAsByte;

        /// <inheritdoc/>
        public bool Equals(string other) => TryParse(other, out var color) && Equals(color);

#if !RECORD_STRUCT_SUPPORT
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if(obj is Color color) return Equals(color);
            if(obj is SysColor sysColor) return Equals(sysColor);
            if(obj is string strColor) return Equals(strColor);
            return false;
        }
#endif

        /// <summary>
        /// Gets a representation of the current instance in the format <c>rgb(RRR, GGG, BBB)</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The characters <c>RRR</c>, <c>GGG</c> and <c>BBB</c> in the format example correpsond to three decimal
        /// unsigned byte values (0 to 255) which indicate the red, green and blue components of the color respectively.
        /// </para>
        /// <para>
        /// Note that this format omits the alpha (transparency) component of the color.  Thus, the resulting string
        /// will not be equal to the <see cref="Color"/> value which created it if the alpha component is not equal to 1.
        /// The only string-formatting methods which preserve all of the information in the color instance are
        /// <see cref="ToRgbaString"/> and the default <see cref="ToString"/> method, which both return equivalent results.
        /// </para>
        /// </remarks>
        /// <returns>An RGB-format string.</returns>
        public string ToRgbString() => $"rgb({Red}, {Green}, {Blue})";

        /// <summary>
        /// Gets a representation of the current instance in the format <c>rgba(RRR, GGG, BBB, A)</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The characters <c>RRR</c>, <c>GGG</c> and <c>BBB</c> in the format example correpsond to three decimal
        /// unsigned byte values (0 to 255) which indicate the red, green and blue components of the color respectively.
        /// The character <c>A</c> corresponds to the alpha (transparency) component of the color.  It will be either
        /// the numbers 1 or 0, or it will be a decimal number between one and zero.
        /// </para>
        /// <para>
        /// Out of the string-formatting methods available on the <see cref="Color"/> type, only this one and the default
        /// <see cref="ToString"/> method preserve all information in the color instance.  Thus this method may be used
        /// alongside <see cref="Parse"/> or <see cref="TryParse(string, out Color)"/> to reliably round-trip an RGBA
        /// string color representation.
        /// </para>
        /// </remarks>
        /// <returns>An RGBA-format string.</returns>
        public string ToRgbaString() => $"rgba({Red}, {Green}, {Blue}, {Alpha})";

        /// <summary>
        /// Gets a representation of the current instance in the format <c>#RRGGBB</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The characters <c>RR</c>, <c>GG</c> and <c>BB</c> in the format example correpsond to three hexadecimal
        /// unsigned byte values (00 to FF, corresponding to decimal 0 to 255).  These indicate the red, green and
        /// blue components of the color respectively.
        /// </para>
        /// <para>
        /// Note that this format omits the alpha (transparency) component of the color.  Thus, the resulting string
        /// will not be equal to the <see cref="Color"/> value which created it if the alpha component is not equal to 1.
        /// The only string-formatting methods which preserve all of the information in the color instance are
        /// <see cref="ToRgbaString"/> and the default <see cref="ToString"/> method, which both return equivalent results.
        /// </para>
        /// </remarks>
        /// <returns>An RGB-format string.</returns>
        public string ToHexString() => $"#{Red:X}{Green:X}{Blue:X}";

        /// <summary>
        /// Returns a string in the same format as <see cref="ToRgbaString"/>.
        /// </summary>
        /// <remarks>
        /// <para>This method and <see cref="ToRgbaString"/> are equivalent.</para>
        /// </remarks>
        /// <returns>An RGBA-format string.</returns>
        public override string ToString() => ToRgbaString();

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Red, Green, Blue, AlphaAsByte);

        /// <summary>
        /// Creates a new instance of <see cref="Color"/> from the specified RGBA values.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The byte data type for the R, G and B components of the color ensure that it is impossible to specify
        /// values outside the permitted range. As for the <paramref name="alpha"/>, if a value of less than zero is specified
        /// here then it is treated as zero. Likewise if a value of greater than one is specified then it will be treated as one.
        /// </para>
        /// </remarks>
        /// <param name="red">The red component in the sRGB color space</param>
        /// <param name="green">The green component in the sRGB color space</param>
        /// <param name="blue">The blue component in the sRGB color space</param>
        /// <param name="alpha">The alpha (transparency) component, which must be between zero and one.</param>
        public Color(byte red, byte green, byte blue, double alpha = 1D)
        {
            Red = red;
            Green = green;
            Blue = blue;
            this.alpha = SanitizeAlpha(alpha);
        }

        /// <summary>
        /// Sanitizes a value intended for <see cref="Alpha"/>.
        /// </summary>
        /// <remarks>
        /// <para>If <paramref name="alpha"/> is less than zero then this method returns zero.
        /// If <paramref name="alpha"/> is more than one then this method returns one.
        /// Otherwise, this method returns <paramref name="alpha"/>.</para>
        /// </remarks>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>A sanitized alpha value.</returns>
        static double SanitizeAlpha(double alpha) => alpha < 0 ? 0 : alpha > 1 ? 1 : alpha;

        /// <summary>
        /// Converts a <see cref="System.Drawing.Color"/> into a <see cref="Color"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In some situations, it might be useful to convert to/from the .NET built-in <see cref="System.Drawing.Color"/>
        /// type.  Use this method and <see cref="ToSystemDrawingColor"/> to do so.
        /// </para>
        /// </remarks>
        /// <returns>A <see cref="Color"/> representation of the specified color.</returns>
        public static Color FromSystemDrawingColour(SysColor color)
            => new Color(color.R, color.G, color.B, color.A / 255);

        /// <summary>
        /// Gets a value indicating whether the specified <paramref name="webColorValue"/> is a valid string representation
        /// for a <see cref="Color"/>.  If this returns <see langword="true"/> then exposes the parsed <see cref="Color"/>
        /// value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this method returns <see langword="true"/> then the output parameter <paramref name="color"/> will contain
        /// the color parsed from the specified string.  If this method returns <see langword="false"/> then the output parameter
        /// has an undefined value and must not be used.
        /// </para>
        /// <para>This method will not raise an exception and it is safe to use with unknown strings, including those which
        /// might be <see langword="null"/>.</para>
        /// </remarks>
        /// <param name="webColorValue">A string containing a color value which is compatible with the web.
        /// See <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/Reference/Values/color_value">the MDN writeup on
        /// web colors</see> for more information.</param>
        /// <param name="color">If this method returns <see langword="true"/>, then this parameter exposes the parsed color value.
        /// If this method returns <see langword="false"/> then the value of this parameter must be ignored.</param>
        /// <returns><see langword="true"/> if parsing is successful (the <paramref name="webColorValue"/> contains a valid &amp;
        /// recognized color representation); <see langword="false"/> if not</returns>
        public static bool TryParse(string webColorValue, out Color color)
        {
            color = default;
            if(string.IsNullOrWhiteSpace(webColorValue)) return false;

            var allParsers = new IParsesColor[]
            {
                new RgbColorParser(),
                new RgbPercentageColorParser(),
                new RgbaColorParser(),
                new RgbaPercentageColorParser(),
                new Hex6ColorParser(),
                new Hex3ColorParser(),
                new HslColorParser(),
                new HslaColorParser(),
                new NamedColorParser(),
            };

            foreach(var parser in allParsers)
                if(parser.TryParseColor(webColorValue, out color)) return true;

            color = default;
            return false;
        }

        /// <summary>
        /// Parses a <see cref="Color"/> from a string representation of a color.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <paramref name="webColorValue"/> is a valid representation of a web-compatible color value then this method will
        /// return that color as an instance of <see cref="Color"/>.  If not, this method will throw an exception.
        /// </para>
        /// <para>If you are not certain that the <paramref name="webColorValue"/> represents a valid colour then use
        /// <see cref="TryParse(string, out Color)"/> instead.</para>
        /// </remarks>
        /// <param name="webColorValue">A string containing a color value which is compatible with the web.
        /// See <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/Reference/Values/color_value">the MDN writeup on
        /// web colors</see> for more information.</param>
        /// <returns>The parsed color.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="webColorValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">If <paramref name="webColorValue"/> is not a recognized representation of a color.</exception>
        public static Color Parse(string webColorValue)
        {
            if (webColorValue is null)
                throw new ArgumentNullException(nameof(webColorValue));
            
            return TryParse(webColorValue, out var color)
                ? color
                : throw new FormatException("The string color representation must be a in a web-compatible recognized format.");
        }

        /// <summary>
        /// Gets a value indicating whether the specified <see cref="Color"/> and <see cref="System.Drawing.Color"/>
        /// are equal.
        /// </summary>
        /// <remarks>
        /// <para>Despite the type differences, two colors are equal if they represent the same sRGB color.</para>
        /// </remarks>
        /// <param name="color">The <see cref="Color"/></param>
        /// <param name="sysColor">The <see cref="System.Drawing.Color"/></param>
        /// <returns><see langword="true"/> if the two different colors represent the same sRGB color; <see langword="false"/> if not.</returns>
        public static bool operator ==(Color color, SysColor sysColor) => color.Equals(sysColor);

        /// <summary>
        /// Gets a value indicating whether the specified <see cref="Color"/> and <see cref="System.Drawing.Color"/>
        /// are not equal.
        /// </summary>
        /// <remarks>
        /// <para>Despite the type differences, two colors are equal if they represent the same sRGB color.</para>
        /// </remarks>
        /// <param name="color">The <see cref="Color"/></param>
        /// <param name="sysColor">The <see cref="System.Drawing.Color"/></param>
        /// <returns><see langword="false"/> if the two different colors represent the same sRGB color; <see langword="true"/> if not.</returns>
        public static bool operator !=(Color color, SysColor sysColor) => !(color == sysColor);
        
        /// <summary>
        /// Gets a value indicating whether the specified <see cref="Color"/> and <see cref="string"/> representation of a color
        /// are equal.
        /// </summary>
        /// <remarks>
        /// <para>A color is equal to a string if the result of <see cref="TryParse(string, out Color)"/> is <see langword="true"/> 
        /// and if the resulting parsed <see cref="Color"/> instance is equal to the current color.
        /// Note that this means that a color is never equal to a string which is not a valid color representation, or which is <see langword="null"/>.</para>
        /// </remarks>
        /// <param name="color">The <see cref="Color"/></param>
        /// <param name="stringColor">The string which represents a color</param>
        /// <returns><see langword="true"/> if the current color and the string color representation indicate the same sRGB color; <see langword="false"/> if not.</returns>
        public static bool operator ==(Color color, string stringColor) => color.Equals(stringColor);

        /// <summary>
        /// Gets a value indicating whether the specified <see cref="Color"/> and <see cref="string"/> representation of a color
        /// are not equal.
        /// </summary>
        /// <remarks>
        /// <para>A color is equal to a string if the result of <see cref="TryParse(string, out Color)"/> is <see langword="true"/> 
        /// and if the resulting parsed <see cref="Color"/> instance is equal to the current color.
        /// Note that this means that a color is never equal to a string which is not a valid color representation, or which is <see langword="null"/>.</para>
        /// </remarks>
        /// <param name="color">The <see cref="Color"/></param>
        /// <param name="stringColor">The string which represents a color</param>
        /// <returns><see langword="false"/> if the current color and the string color representation indicate the same sRGB color; <see langword="true"/> if not.</returns>
        public static bool operator !=(Color color, string stringColor) => !(color == stringColor);

 #if !RECORD_STRUCT_SUPPORT
       /// <summary>
        /// Gets a value indicating whether the two specified <see cref="Color"/> instances are equal.
        /// </summary>
        /// <remarks>
        /// <para>Two colors are equal if they represent the same sRGB color.</para>
        /// </remarks>
        /// <param name="first">The first <see cref="Color"/></param>
        /// <param name="second">The second <see cref="Color"/></param>
        /// <returns><see langword="true"/> if the two colors represent the same sRGB color; <see langword="false"/> if not.</returns>
        public static bool operator ==(Color first, Color second) => first.Equals(second);

        /// <summary>
        /// Gets a value indicating whether the two specified <see cref="Color"/> instances are not equal.
        /// </summary>
        /// <remarks>
        /// <para>Two colors are equal if they represent the same sRGB color.</para>
        /// </remarks>
        /// <param name="first">The first <see cref="Color"/></param>
        /// <param name="second">The second <see cref="Color"/></param>
        /// <returns><see langword="false"/> if the two colors represent the same sRGB color; <see langword="true"/> if not.</returns>
        public static bool operator !=(Color first, Color second) => !(first == second);
#endif
    }
}