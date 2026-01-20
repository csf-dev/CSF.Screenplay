using System;
using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSF.Screenplay.Selenium;

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