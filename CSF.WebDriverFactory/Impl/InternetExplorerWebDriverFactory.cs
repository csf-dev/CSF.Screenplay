using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets an Internet Explorer web driver.
  /// </summary>
  public class InternetExplorerWebDriverFactory : IWebDriverFactory
  {
    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      var driverService = InternetExplorerDriverService.CreateDefaultService();
      driverService.HideCommandPromptWindow = true;
      driverService.SuppressInitialDiagnosticInformation = true;
      return new InternetExplorerDriver(driverService,
                                        new InternetExplorerOptions(),
                                        TimeSpan.FromSeconds(CommandTimeoutSeconds));
    }

    public InternetExplorerWebDriverFactory()
    {
      CommandTimeoutSeconds = 60;
    }
  }
}
