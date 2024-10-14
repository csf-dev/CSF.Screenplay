#if !NETSTANDARD2_0

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// An application/background service that begins the JSON to HTML report conversion process.
    /// </summary>
    public class ReportConverterApplication : BackgroundService
    {
        readonly IOptions<ReportConverterOptions> options;
        readonly IConvertsReportJsonToHtml reportConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportConverterApplication"/> class.
        /// </summary>
        /// <param name="options">The options for performing the conversion.</param>
        /// <param name="reportConverter">The report converter instance to use for conversion.</param>
        public ReportConverterApplication(IOptions<ReportConverterOptions> options,
                                          IConvertsReportJsonToHtml reportConverter)
        {
            this.options = options ?? throw new System.ArgumentNullException(nameof(options));
            this.reportConverter = reportConverter ?? throw new System.ArgumentNullException(nameof(reportConverter));
        }

        /// <summary>
        /// Executes the background service operation.
        /// </summary>
        /// <param name="stoppingToken">A token that can be used to stop the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
            => reportConverter.ConvertAsync(options.Value);
    }
}

#endif