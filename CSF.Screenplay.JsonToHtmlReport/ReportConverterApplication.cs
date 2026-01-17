#if !NETSTANDARD2_0

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// An application/background service that begins the JSON to HTML report conversion process.
    /// </summary>
    public class ReportConverterApplication : IHostedService
    {
        readonly IOptions<ReportConverterOptions> options;
        readonly IConvertsReportJsonToHtml reportConverter;
        readonly IHostApplicationLifetime lifetime;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportConverterApplication"/> class.
        /// </summary>
        /// <param name="options">The options for performing the conversion.</param>
        /// <param name="reportConverter">The report converter instance to use for conversion.</param>
        /// <param name="lifetime">The application lifetime</param>
        public ReportConverterApplication(IOptions<ReportConverterOptions> options,
                                          IConvertsReportJsonToHtml reportConverter,
                                          IHostApplicationLifetime lifetime)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.reportConverter = reportConverter ?? throw new ArgumentNullException(nameof(reportConverter));
            this.lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
        }

        /// <summary>
        /// Executes the application, to perform its work.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await reportConverter.ConvertAsync(options.Value);
            Console.WriteLine("Conversion complete; HTML report available at {0}", options.Value.OutputPath);
            lifetime.StopApplication();
        }

        /// <summary>
        /// Unused, always returns a completed task.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task</returns>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

#endif