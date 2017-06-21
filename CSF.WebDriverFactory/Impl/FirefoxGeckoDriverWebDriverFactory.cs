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
    public string ExecutablePath { get; set; }

    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      var driverService = FirefoxDriverService.CreateDefaultService();
      driverService.FirefoxBinaryPath = ExecutablePath;
      driverService.HideCommandPromptWindow = true;
      driverService.SuppressInitialDiagnosticInformation = true;
      return new FirefoxDriver(driverService, new FirefoxOptions(), TimeSpan.FromSeconds(CommandTimeoutSeconds));
    }

    public FirefoxGeckoDriverWebDriverFactory()
    {
      CommandTimeoutSeconds = 60;
    }
  }
}
