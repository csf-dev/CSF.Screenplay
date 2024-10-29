using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Provides methods to register services for the JsonToHtmlReport application (or library).
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is consumed by the JSON to HTML report converter when it is built as an application,
    /// but it may also be used when consuming this project as a library, for integrating it into other solutions.
    /// </para>
    /// </remarks>
    public static class ServiceRegistrations
    {
        /// <summary>
        /// Registers the services required for the JsonToHtmlReport application (or library).
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IConvertsReportJsonToHtml, ReportConverter>();
            services.AddTransient<IGetsHtmlTemplate, TemplateReader>();
        }
    }
}