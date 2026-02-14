using CSF.Screenplay.Reporting;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Reporting
{
    /// <summary>
    /// An <see cref="IValueFormatter"/> for Selenium <see cref="Screenshot"/> objects, for reporting.
    /// </summary>
    public class ScreenshotFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is Screenshot;

        /// <inheritdoc/>
        public string FormatForReport(object value) => $"A Selenium screenshot";
    }
}