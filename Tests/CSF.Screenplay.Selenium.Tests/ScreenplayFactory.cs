using CSF.Extensions.WebDriver;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Selenium;

public class ScreenplayFactory : IGetsScreenplay
{
    public Screenplay GetScreenplay()
    {
        var screenplay = Screenplay.Create(services =>
        {
            services.AddSingleton(GetConfiguration());
            services.AddWebDriverFactory();

            services.AddTransient<Webster>();
        });

        return screenplay;
    }

    static IConfiguration GetConfiguration() => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();
}