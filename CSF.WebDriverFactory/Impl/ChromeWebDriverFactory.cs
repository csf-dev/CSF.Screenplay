using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets a Google Chrome web driver.
  /// </summary>
  public class ChromeWebDriverFactory : IWebDriverFactory
  {
    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      var driverService = ChromeDriverService.CreateDefaultService();
      driverService.HideCommandPromptWindow = true;
      driverService.SuppressInitialDiagnosticInformation = true;
      return new ChromeDriver(driverService, new ChromeOptions(), TimeSpan.FromSeconds(CommandTimeoutSeconds));
    }

    public ChromeWebDriverFactory()
    {
      CommandTimeoutSeconds = 60;
    }
  }
}
