using System;
using System.Text.RegularExpressions;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// An object which can get an instance of <see cref="Color"/>.
    /// </summary>
    internal interface IParsesColor
    {
        /// <summary>
        /// Attempts to parse an instance of <see cref="Color"/> from the specified <paramref name="colorValue"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the parsing is successful then this method will return <see langword="true"/> and the <paramref name="color"/>
        /// will contain the parsed color value.
        /// Otherwise it will return <see langword="false"/> and the <paramref name="color"/> parameter must be ignored.
        /// </para>
        /// </remarks>
        /// <param name="colorValue">A string representing a color.</param>
        /// <param name="color">If this method returns <see langword="true"/> then this parameter exposes the parsed
        /// color; if not then this value must be ignored.</param>
        /// <returns><see langword="true"/> if the parsing was a success; <see langword="false"/> if not.</returns>
        bool TryParseColor(string colorValue, out Color color);
    }

    internal abstract class NumericColorParser : IParsesColor
    {
        protected static readonly string
            BytePart = @"\s*(\d{1,3})\s*",
            DecimalAlphaPart = @"\s*(0|1|0\.\d+)\s*",
            PercentDecimalPart = @"\s*(\d{1,3}|\d{1,2}\.\d+)%\s*",
            PercentPart = @"\s*(\d{1,3})%\s*",
            Hex2Part = @"([0-9a-f]{2})",
            Hex1Part = @"([0-9a-f])";

        /// <inheritdoc/>
        public virtual bool TryParseColor(string colorValue, out Color color)
        {
            color = default;
            var match = GetParsingRegex(out var includesAlpha).Match(colorValue);
            if(!match.Success) return false;

            color = new Color(GetByte(match.Groups[1]),
                              GetByte(match.Groups[2]),
                              GetByte(match.Groups[3]),
                              includesAlpha ? double.Parse(match.Groups[4].Value) : 1);
            return true;
        }

        /// <summary>
        /// Gets a regex which parses a string color representation.
        /// </summary>
        /// <param name="includesAlpha"><see langword="true"/> if this regex includes a value for the alpha channel; <see langword="false"/> if not.</param>
        /// <returns>A regex</returns>
        protected virtual Regex GetParsingRegex(out bool includesAlpha)
            => new Regex(GetParsingPattern(out includesAlpha),
                         RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase,
                         TimeSpan.FromMilliseconds(500));

        /// <summary>
        /// Gets a regex pattern which parses a string color representation.
        /// </summary>
        /// <param name="includesAlpha"><see langword="true"/> if this regex includes a value for the alpha channel; <see langword="false"/> if not.</param>
        /// <returns>A regex</returns>
        protected abstract string GetParsingPattern(out bool includesAlpha);

        /// <summary>
        /// Parses a byte value from the value of the match group.
        /// </summary>
        /// <param name="matchGroup">A regex match group</param>
        /// <returns>A byte value</returns>
        protected virtual byte GetByte(Group matchGroup) => byte.Parse(matchGroup.Value);
    }

    internal class RgbColorParser : NumericColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = false;
            return $"^\\s*rgb\\({BytePart},{BytePart},{BytePart}\\)\\s*$";
        }
    }

    internal class RgbaColorParser : NumericColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = true;
            return $"^\\s*rgba\\({BytePart},{BytePart},{BytePart},{DecimalAlphaPart}\\)\\s*$";
        }
    }

    internal class RgbPercentageColorParser : NumericColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = false;
            return $"^\\s*rgb\\({PercentDecimalPart},{PercentDecimalPart},{PercentDecimalPart}\\)\\s*$";
        }

        /// <inheritdoc/>
        protected override byte GetByte(Group matchGroup) => (byte) (double.Parse(matchGroup.Value) / 100D * 255D);
    }

    internal class RgbaPercentageColorParser : RgbPercentageColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = true;
            return $"^\\s*rgba\\({PercentDecimalPart},{PercentDecimalPart},{PercentDecimalPart},{DecimalAlphaPart}\\)\\s*$";
        }
    }

    internal class Hex3ColorParser : NumericColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = false;
            return $"^\\s*#{Hex1Part}{Hex1Part}{Hex1Part}\\s*$";
        }

        /// <inheritdoc/>
        protected override byte GetByte(Group matchGroup) => Convert.ToByte(matchGroup.Value + matchGroup.Value, 16);
    }

    internal class Hex6ColorParser : NumericColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = false;
            return $"^\\s*#{Hex2Part}{Hex2Part}{Hex2Part}\\s*$";
        }

        /// <inheritdoc/>
        protected override byte GetByte(Group matchGroup) => Convert.ToByte(matchGroup.Value, 16);
    }

    internal class HslColorParser : NumericColorParser
    {
        /// <inheritdoc/>
        public override bool TryParseColor(string colorValue, out Color color)
        {
            color = default;
            var match = GetParsingRegex(out var includesAlpha).Match(colorValue);
            if(!match.Success) return false;

            color = HslToRgb(short.Parse(match.Groups[1].Value),
                             byte.Parse(match.Groups[2].Value),
                             byte.Parse(match.Groups[3].Value),
                             includesAlpha ? double.Parse(match.Groups[4].Value) : 1);
            return true;
        }

        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = false;
            return $"^\\s*hsl\\({BytePart},{PercentPart},{PercentPart}\\)\\s*$";
        }

        /// <summary>
        /// Converts an HSL color to the sRGB color space.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is based upon the algorithms available at
        /// <see href="https://github.com/SeleniumHQ/selenium/blob/selenium-4.40.0/java/src/org/openqa/selenium/support/Color.java#L258">The
        /// Selenium 4.40.0 HSL converter for Java</see>.
        /// </para>
        /// </remarks>
        /// <param name="h">The hue value in degrees (0 to 360)</param>
        /// <param name="s">The saturation percentage (0 to 100)</param>
        /// <param name="l">The lightness percentage (0 to 100)</param>
        /// <param name="alpha">The alpha (transparency) value (0 to 1)</param>
        /// <returns>An instance of <see cref="Color"/> in the sRGB color space</returns>
        static Color HslToRgb(short h, byte s, byte l, double alpha)
        {
            var hue = h / 360D;
            var saturation = s / 100D;
            var lightness = l / 100D;

            // FYI: Saturation = zero means gray, making hue irrelevant
            if(s == 0)
            {
                var allChannels = (byte) Math.Round(lightness * 255D, MidpointRounding.AwayFromZero);
                return new Color(allChannels, allChannels, allChannels, alpha);
            }
            
            var luminosity2 = (lightness < 0.5)
                ? lightness * (saturation + 1D)
                : lightness + saturation - lightness * saturation;
            var luminosity1 = 2D * lightness - luminosity2;

            return new Color(HueValueToRgbByte(luminosity1, luminosity2, hue + 1D / 3D),
                             HueValueToRgbByte(luminosity1, luminosity2, hue),
                             HueValueToRgbByte(luminosity1, luminosity2, hue - 1D / 3D),
                             alpha);
        }

        static byte HueValueToRgbByte(double luminosity1,
                                      double luminosity2,
                                      double hueValue)
            => (byte) Math.Round(HueValueToRgb(luminosity1, luminosity2, hueValue) * 255D, MidpointRounding.AwayFromZero);

        static double HueValueToRgb(double luminosity1,
                                    double luminosity2,
                                    double hueValue)
        {
            if (hueValue < 0D) hueValue += 1;
            if (hueValue > 1D) hueValue -= 1.0;
            if (hueValue < 1D / 6D) return luminosity1 + (luminosity2 - luminosity1) * 6D * hueValue;
            if (hueValue < 1D / 2D) return luminosity2;
            if (hueValue < 2D / 3D) return luminosity1 + (luminosity2 - luminosity1) * (2D / 3D - hueValue) * 6D;
            return luminosity1;
        }
    }

    internal class HslaColorParser : HslColorParser
    {
        /// <inheritdoc/>
        protected override string GetParsingPattern(out bool includesAlpha)
        {
            includesAlpha = true;
            return $"^\\s*hsla\\({BytePart},{PercentPart},{PercentPart},{DecimalAlphaPart}\\)\\s*$";
        }
    }

    internal class NamedColorParser : IParsesColor
    {
        /// <inheritdoc/>
        public bool TryParseColor(string colorValue, out Color color)
            => Colors.TryGetNamedColor(colorValue, out color);
    }

}