using System.Collections.Generic;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Selenium.Queries;

namespace CSF.Screenplay.Selenium.Reporting
{
    /// <summary>
    /// A value formatter for collections of <see cref="Option"/> for reporting.
    /// </summary>
    public class OptionsFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is IEnumerable<Option>;

        /// <inheritdoc/>
        public string FormatForReport(object value) => $"The options {string.Join(", ", (IEnumerable<Option>) value)}";
    }
}