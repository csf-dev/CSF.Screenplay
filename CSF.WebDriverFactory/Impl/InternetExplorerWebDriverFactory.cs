using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets an Internet Explorer web driver.
  /// </summary>
  public class InternetExplorerWebDriverFactory : IWebDriverFactory
  {
    /// <summary>
    /// Gets or sets the timeout (in seconds) between issuing a command to the web driver and receiving a response.
    /// </summary>
    /// <value>The command timeout seconds.</value>
    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the TCP port on which the web driver process will listen.
    /// </summary>
    /// <value>The IE driver port.</value>
    public int? DriverPort { get; set; }

    /// <summary>
    /// Gets or sets the filesystem path to the web-driver executable (<c>chromedriver</c>).
    /// </summary>
    /// <value>The IE driver path.</value>
    public string DriverPath { get; set; }

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
      var options = GetIEOptions();

      if(capabilities != null)
      {
        foreach(var cap in capabilities)
        {
          options.AddAdditionalCapability(cap.Key, cap.Value);
        }
      }

      var timeout = GetTimeout();
      return new InternetExplorerDriver(driverService, options, timeout);
    }

    TimeSpan GetTimeout()
    {
      return TimeSpan.FromSeconds(CommandTimeoutSeconds);
    }

    InternetExplorerDriverService GetDriverService()
    {
      InternetExplorerDriverService output;

      if(String.IsNullOrEmpty(DriverPath))
        output = InternetExplorerDriverService.CreateDefaultService();
      else
        output = InternetExplorerDriverService.CreateDefaultService(DriverPath);

      output.HideCommandPromptWindow = true;
      output.SuppressInitialDiagnosticInformation = false;

      if(DriverPort.HasValue)
        output.Port = DriverPort.Value;

      return output;
    }

    InternetExplorerOptions GetIEOptions()
    {
      var output = new InternetExplorerOptions();

      return output;
    }
  }
}
