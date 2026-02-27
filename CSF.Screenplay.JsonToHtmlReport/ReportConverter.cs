using System;
using System.IO;
using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Provides functionality to convert JSON reports to HTML format.
    /// </summary>
    public class ReportConverter : IConvertsReportJsonToHtml
    {
        readonly IGetsHtmlTemplate templateReader;
        readonly IEmbedsReportAssets assetsEmbedder;
        readonly IDeserializesReport reportReader;
        readonly ISerializesReport reportWriter;

        /// <inheritdoc/>
        public async Task ConvertAsync(ReportConverterOptions options)
        {
            var report = await ReadReport(options.ReportPath).ConfigureAwait(false);
            await assetsEmbedder.EmbedReportAssetsAsync(report, options).ConfigureAwait(false);
            var reportJson = await GetModifiedReportJsonAsync(report).ConfigureAwait(false);
            var template = await templateReader.ReadTemplate().ConfigureAwait(false);
            var assembledTemplate = template.Replace("<!-- REPORT_PLACEHOLDER -->", reportJson);
            await WriteReport(options.OutputPath, assembledTemplate).ConfigureAwait(false);
        }

        async Task<ScreenplayReport> ReadReport(string path)
        {
            using (var stream = File.OpenRead(path))
                return await reportReader.DeserializeAsync(stream).ConfigureAwait(false);
        }

        async Task<string> GetModifiedReportJsonAsync(ScreenplayReport report)
        {
            var stream = await reportWriter.SerializeAsync(report).ConfigureAwait(false);
            using(var textReader = new StreamReader(stream))
                return await textReader.ReadToEndAsync().ConfigureAwait(false);
        }

        async Task WriteReport(string path, string report)
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
        /// <param name="assetsEmbedder">A service which embeds asset data into the JSON report.</param>
        /// <param name="reportReader">A report deserializer</param>
        /// <param name="reportWriter">A report serializer</param>
        public ReportConverter(IGetsHtmlTemplate templateReader,
                               IEmbedsReportAssets assetsEmbedder,
                               IDeserializesReport reportReader,
                               ISerializesReport reportWriter)
        {
            this.templateReader = templateReader ?? throw new ArgumentNullException(nameof(templateReader));
            this.assetsEmbedder = assetsEmbedder ?? throw new ArgumentNullException(nameof(assetsEmbedder));
            this.reportReader = reportReader ?? throw new ArgumentNullException(nameof(reportReader));
            this.reportWriter = reportWriter ?? throw new ArgumentNullException(nameof(reportWriter));
        }
    }
}