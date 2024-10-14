using System;
using System.Threading.Tasks;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Provides functionality to convert JSON reports to HTML format.
    /// </summary>
    public class ReportConverter : IConvertsReportJsonToHtml
    {
        /// <inheritdoc/>
        public Task ConvertAsync(ReportConverterOptions options)
        {
            // Implementation for converting JSON to HTML report
            throw new NotImplementedException();
        }
    }
}