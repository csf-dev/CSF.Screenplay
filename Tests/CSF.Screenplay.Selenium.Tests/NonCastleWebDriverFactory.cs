using System;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium.Chrome;

namespace CSF.Screenplay.Selenium;

/// <summary>
/// Implementation of <see cref="IGetsWebDriver"/> which does not use the universal web driver factory.
/// </summary>
/// <remarks>
/// <para>
/// I've written this because I have suspicions that the universal factory is interfering with the BrowserStack SDK.
/// This may be used to override the universal factory.
/// </para>
/// </remarks>
public class NonCastleWebDriverFactory : IGetsWebDriver
{
    public WebDriverAndOptions GetDefaultWebDriver(Action<OpenQA.Selenium.DriverOptions>? supplementaryConfiguration = null)
    {
        var options = new ChromeOptions();
        var driver = new ChromeDriver(options);
        return new WebDriverAndOptions(driver, options);
    }

    public WebDriverAndOptions GetWebDriver(string configurationName, Action<OpenQA.Selenium.DriverOptions>? supplementaryConfiguration = null)
        => GetDefaultWebDriver();
}
