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

    public int? ChromeDriverPort { get; set; }

    public string ChromeDriverPath { get; set; }

    public string ChromeExecutablePath { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      var driverService = GetDriverService();
      var options = GetChromeOptions();
      var timeout = GetTimeout();
      return new ChromeDriver(driverService, options, timeout);
    }

    TimeSpan GetTimeout()
    {
      return TimeSpan.FromSeconds(CommandTimeoutSeconds);
    }

    ChromeDriverService GetDriverService()
    {
      ChromeDriverService output;

      if(String.IsNullOrEmpty(ChromeDriverPath))
        output = ChromeDriverService.CreateDefaultService();
      else
        output = ChromeDriverService.CreateDefaultService(ChromeDriverPath);

      output.HideCommandPromptWindow = true;
      output.SuppressInitialDiagnosticInformation = false;

      if(ChromeDriverPort.HasValue)
        output.Port = ChromeDriverPort.Value;

      return output;
    }

    ChromeOptions GetChromeOptions()
    {
      var output = new ChromeOptions();

      if(!String.IsNullOrEmpty(ChromeExecutablePath))
        output.BinaryLocation = ChromeExecutablePath;

      return output;
    }

    public ChromeWebDriverFactory()
    {
      CommandTimeoutSeconds = 60;
    }
  }
}
