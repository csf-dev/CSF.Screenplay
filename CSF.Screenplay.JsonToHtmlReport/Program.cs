#if !NETSTANDARD2_0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// The main entry point class for the JSON to HTML report converter application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point method for the JSON to HTML report converter application.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method scaffolds the application using the Generic Host pattern, then runs that host.
        /// This method is available only when the project is built as an executable.  It is unavailable for the
        /// <c>netstandard2.0</c> target framework.
        /// </para>
        /// </remarks>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(RegisterServices)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });

        static void RegisterServices(HostBuilderContext hostContext, IServiceCollection services)
            => new ServiceRegistrations().RegisterServices(services);
    }
}

#endif