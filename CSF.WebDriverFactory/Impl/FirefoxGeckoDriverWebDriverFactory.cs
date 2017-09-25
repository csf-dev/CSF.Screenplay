using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which is suitable for Firefox versions 47+, using the
  /// <c>geckodriver</c> executable.
  /// </summary>
  public class FirefoxGeckoDriverWebDriverFactory : IWebDriverFactory
  {
    /// <summary>
    /// Gets or sets the timeout (in seconds) between issuing a command to the web driver and receiving a response.
    /// </summary>
    /// <value>The command timeout seconds.</value>
    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the TCP port on which the web driver process will listen.
    /// </summary>
    /// <value>The gecko driver port.</value>
    public int? DriverPort { get; set; }

    /// <summary>
    /// Gets or sets the filesystem path to the web-driver executable (<c>chromedriver</c>).
    /// </summary>
    /// <value>The gecko driver path.</value>
    public string DriverPath { get; set; }

    /// <summary>
    /// Gets or sets the filesystem path to the Mozilla Firefox web browser executable.
    /// </summary>
    /// <value>The firefox executable path.</value>
    public string BrowserExecutablePath { get; set; }

    /// <summary>
    /// Gets the name of the web browser that this factory will create.
    /// </summary>
    /// <returns>The browser name.</returns>
    public string GetBrowserName() => "Firefox";

    /// <summary>
    /// Gets the version of the web browser that this factory will create.
    /// </summary>
    /// <returns>The browser version.</returns>
    public string GetBrowserVersion() => null;

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      return GetWebDriver(null);
    }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver(IDictionary<string,object> capabilities)
    {
      var driverService = GetDriverService();
      var options = GetFirefoxOptions();

      if(capabilities != null)
      {
        foreach(var cap in capabilities)
        {
          options.AddAdditionalCapability(cap.Key, cap.Value);
        }
      }

      var timeout = GetTimeout();
      return new FirefoxDriver(driverService, options, timeout);
    }

    TimeSpan GetTimeout()
    {
      return TimeSpan.FromSeconds(CommandTimeoutSeconds);
    }

    FirefoxDriverService GetDriverService()
    {
      FirefoxDriverService output;

      if(String.IsNullOrEmpty(DriverPath))
        output = FirefoxDriverService.CreateDefaultService();
      else
        output = FirefoxDriverService.CreateDefaultService(DriverPath);

      output.HideCommandPromptWindow = true;
      output.SuppressInitialDiagnosticInformation = true;

      if(DriverPort.HasValue)
        output.Port = DriverPort.Value;

      if(!String.IsNullOrEmpty(BrowserExecutablePath))
        output.FirefoxBinaryPath = BrowserExecutablePath;

      return output;
    }

    FirefoxOptions GetFirefoxOptions()
    {
      var output = new FirefoxOptions();

      if(!String.IsNullOrEmpty(BrowserExecutablePath))
        output.BrowserExecutableLocation = BrowserExecutablePath;

      return output;
    }
  }
}
