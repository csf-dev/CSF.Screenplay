using CSF.Screenplay.Reporting;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Reporting
{
    /// <summary>
    /// A value formatter for Selenium screenshot objects, for reporting.
    /// </summary>
    public class ScreenshotFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is Screenshot;

        /// <inheritdoc/>
        public string FormatForReport(object value) => $"A Selenium screenshot";
    }
}