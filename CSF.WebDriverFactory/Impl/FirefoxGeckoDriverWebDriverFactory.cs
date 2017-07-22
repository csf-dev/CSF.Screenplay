using System;
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
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      var driverService = GetDriverService();
      var options = GetFirefoxOptions();
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
      output.SuppressInitialDiagnosticInformation = false;

      if(DriverPort.HasValue)
        output.Port = DriverPort.Value;

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
