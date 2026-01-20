using System;
using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSF.Screenplay.Selenium;

/// <summary>
/// An implementation of <see cref="ICreatesWebDriverFromOptions"/> which writes verbose logs to a file.
/// </summary>
/// <remarks>
/// <para>
/// This driver factory is unused most of the time, but it's helpful if you're trying to diagnose problems with ChromeDriver,
/// say in a CI build that's been failing.  Select this factory by changing the <c>appsettings.json</c> file and you'll get
/// log files.
/// </para>
/// </remarks>
public class VerboseChromeDriverFactory : ICreatesWebDriverFromOptions
{
    public WebDriverAndOptions GetWebDriver(WebDriverCreationOptions options, Action<DriverOptions> supplementaryConfiguration = null)
    {
        var chromeOptions = (ChromeOptions) options.OptionsFactory();
        var service = ChromeDriverService.CreateDefaultService();
        service.EnableVerboseLogging = true;
        service.LogPath = "chrome-verbose-log.txt";
        var driver = new ChromeDriver(service, chromeOptions);

        return new(driver, chromeOptions);
    }
}