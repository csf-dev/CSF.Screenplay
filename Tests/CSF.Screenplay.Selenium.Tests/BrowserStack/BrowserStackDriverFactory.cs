using System;
using System.Collections.Generic;
using CSF.Extensions.WebDriver.Factories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using static CSF.Screenplay.Selenium.BrowserStack.BrowserStackEnvironment;

namespace CSF.Screenplay.Selenium.BrowserStack;

/// <summary>
/// Implementation of <see cref="ICreatesWebDriverFromOptions"/> which creates drivers for BrowserStack.
/// </summary>
/// <remarks>
/// <para>
/// I'm using this instead of the BrowserStack SDK because I'm already modifying the way that tests run via Screenplay, so I want to avoid
/// messing with them twice.  Later, I can give a try with the official SDK to see if its compatible with Screenplay.
/// See <see href="https://github.com/csf-dev/CSF.Screenplay/issues/272">Issue #272</see> for more info.
/// </para>
/// </remarks>
public class BrowserStackDriverFactory : ICreatesWebDriverFromOptions
{
    const string BrowserStackOptionsCapability = "bstack:options";

    const string GridUrl = "https://hub-cloud.browserstack.com/wd/hub/";

    public WebDriverAndOptions GetWebDriver(WebDriverCreationOptions options, Action<DriverOptions>? supplementaryConfiguration = null)
    {
        var driverOptions = GetDriverOptions();
        driverOptions.SetLoggingPreference(LogType.Browser, LogLevel.Info);
        driverOptions.AddAdditionalOption(BrowserStackOptionsCapability, GetBrowserStackOptions());
        var driver = new RemoteWebDriver(new Uri(GridUrl), driverOptions);
        return new (driver, driverOptions);
    }

    static DriverOptions GetDriverOptions()
    {
        var browserName = GetBrowserName();
        DriverOptions options = browserName switch
        {
            "Chrome" => new ChromeOptions(),
            "Edge" => new EdgeOptions(),
            "Firefox" => new FirefoxOptions(),
            "Safari" => new SafariOptions(),
            _ => throw new InvalidOperationException($"The {BrowserName} environment variable: '{GetBrowserName()}' must indicate a supported browser"),
        };

        if (options is ChromeOptions chromeOptions)
        {
            // Must be set as an additional option, not via SetLoggingPreference,
            // to survive the W3C capability negotiation with BrowserStack's remote node.
            chromeOptions.AddAdditionalOption("goog:loggingPrefs",
                new Dictionary<string, string> { { LogType.Browser, LogLevel.Info.ToString().ToUpperInvariant() } });
        }

        return options;
    }

    static Dictionary<string, object?> GetBrowserStackOptions()
    {
        return new ()
        {
            { "os", GetOperatingSystem() },
            { "osVersion", GetOperatingSystemVersion() },
            { "browserVersion", GetBrowserVersion() },
            { "userName", GetBrowserStackUserName() },
            { "accessKey", GetBrowserStackAccessKey() },
            { "local", bool.TrueString.ToLowerInvariant() },
            { "projectName", GetProjectName() },
            { "buildName", GetBuildName() },
            { "sessionName", GetTestName() },
            { "consoleLogs", "verbose" }
        };
    }

    static string GetTestName()
    {
        var className = TestContext.CurrentContext.Test.ClassName;
        var methodName = TestContext.CurrentContext.Test.MethodName;

        return (className is null || methodName is null)
            ? TestContext.CurrentContext.Test.FullName
            : $"{className}.{methodName}";
        
    }
}
