using System;
using System.IO;
using System.Threading.Tasks;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Provides functionality to convert JSON reports to HTML format.
    /// </summary>
    public class ReportConverter : IConvertsReportJsonToHtml
    {
        readonly IGetsHtmlTemplate templateReader;

        /// <inheritdoc/>
        public async Task ConvertAsync(ReportConverterOptions options)
        {
            var report = await ReadReport(options.ReportPath).ConfigureAwait(false);
            var template = await templateReader.ReadTemplate().ConfigureAwait(false);
            var assembledTemplate = template.Replace("<!-- REPORT_PLACEHOLDER -->", report);
            await WriteReport(options.OutputPath, assembledTemplate).ConfigureAwait(false);
        }

        static async Task<string> ReadReport(string path)
        {
            using (var stream = File.OpenRead(path))
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        static async Task WriteReport(string path, string report)
        {
            using (var stream = File.Create(path))
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(report).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportConverter"/> class.
        /// </summary>
        /// <param name="templateReader">The template reader used to get the HTML template.</param>
        public ReportConverter(IGetsHtmlTemplate templateReader)
        {
            this.templateReader = templateReader ?? throw new ArgumentNullException(nameof(templateReader));
        }
    }
}