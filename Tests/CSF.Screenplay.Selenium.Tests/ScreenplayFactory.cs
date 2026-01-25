using CSF.Extensions.WebDriver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CSF.Screenplay.Selenium;

public class ScreenplayFactory : IGetsScreenplay
{
    public Screenplay GetScreenplay()
    {
        var screenplay = Screenplay.Create(services =>
        {
            services.AddSingleton(GetConfiguration());
            services.AddWebDriverFactory();
            services.AddLogging(l => l.AddConsole());

            services.AddTransient<Webster>();
            services.AddTransient<Pattie>();
        }, options =>
        {
            options.ValueFormatters.Add(typeof(Reporting.OptionsFormatter));
            options.ValueFormatters.Add(typeof(Reporting.ScreenshotFormatter));
        });

        return screenplay;
    }

    static IConfiguration GetConfiguration() => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
}