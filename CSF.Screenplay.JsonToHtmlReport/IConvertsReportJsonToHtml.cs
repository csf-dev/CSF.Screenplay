using System.Threading.Tasks;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// An object which can convert a JSON Screenplay report to an HTML format.
    /// </summary>
    public interface IConvertsReportJsonToHtml
    {
        /// <summary>
        /// Converts the JSON Screenplay report data to HTML asynchronously.
        /// </summary>
        /// <param name="options">The options for the report conversion.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task ConvertAsync(ReportConverterOptions options);
    }
}