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
    /// <summary>
    /// Gets or sets the timeout (in seconds) between the web driver issuing a command to the browser and receiving
    /// a response.
    /// </summary>
    /// <value>The command timeout seconds.</value>
    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the TCP port on which the web driver process will listen.
    /// </summary>
    /// <value>The chrome driver port.</value>
    public int? ChromeDriverPort { get; set; }

    /// <summary>
    /// Gets or sets the filesystem path to the web-driver executable (<c>chromedriver</c>).
    /// </summary>
    /// <value>The chrome driver path.</value>
    public string ChromeDriverPath { get; set; }

    /// <summary>
    /// Gets or sets the filesystem path to the Google Chrome web browser executable.
    /// </summary>
    /// <value>The chrome executable path.</value>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverFactory.Impl.ChromeWebDriverFactory"/> class.
    /// </summary>
    public ChromeWebDriverFactory()
    {
      CommandTimeoutSeconds = 60;
    }
  }
}
