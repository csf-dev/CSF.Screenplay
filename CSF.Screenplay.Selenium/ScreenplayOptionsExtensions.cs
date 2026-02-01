namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Extension methods for <see cref="ScreenplayOptions"/>.
    /// </summary>
    public static class ScreenplayOptionsExtensions
    {
        /// <summary>
        /// Adds Screenplay Options which support the Selenium extension.
        /// </summary>
        /// <param name="options">The Screenplay Options</param>
        /// <returns>The same Screenplay Options, so calls may be chained.</returns>
        public static ScreenplayOptions AddSeleniumOptions(this ScreenplayOptions options)
        {
            options.ValueFormatters.Add(typeof(Reporting.OptionsFormatter));
            options.ValueFormatters.Add(typeof(Reporting.ScreenshotFormatter));
            return options;
        }
    }
}