using CSF.Extensions.WebDriver;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the required services to the service collection, so that performances may incorporate Selenium-based performables
        /// and abilities.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The same service collection, so calls may be chained.</returns>
        public static IServiceCollection AddSelenium(this IServiceCollection services)
        {
            services.AddWebDriverFactory();

            services.AddTransient<Reporting.OptionsFormatter>();
            services.AddTransient<Reporting.ScreenshotFormatter>();
            services.Configure<ScreenplayOptions>(o =>
            {
                o.ValueFormatters.Add(typeof(Reporting.OptionsFormatter));
                o.ValueFormatters.Add(typeof(Reporting.ScreenshotFormatter));
            });

            return services;
        }
    }
}