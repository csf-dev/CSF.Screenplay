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
            services.AddLogging(l => l.AddConsole(c => c.LogToStandardErrorThreshold = LogLevel.Warning));
            services.AddSelenium();
            services.AddTransient<Webster>();
            services.AddTransient<Pattie>();
        });

        return screenplay;
    }

    static IConfiguration GetConfiguration() => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
}