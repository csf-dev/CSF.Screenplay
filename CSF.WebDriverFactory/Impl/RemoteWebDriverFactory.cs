using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets a web driver for a remote web browser.
  /// </summary>
  public class RemoteWebDriverFactory : IWebDriverFactory
  {
    public string BrowserType { get; set; }

    public string RemoteAddress { get; set; }

    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver()
    {
      var capabilities = GetCapabilities();
      var uri = GetRemoteUri();
      return new RemoteWebDriver(uri, capabilities, TimeSpan.FromSeconds(CommandTimeoutSeconds));
    }

    protected virtual ICapabilities GetCapabilities()
    {
      switch(BrowserType)
      {
      case "Chrome":
      case "chrome":
        return DesiredCapabilities.Chrome();

      case "Firefox":
      case "firefox":
        return DesiredCapabilities.Firefox();

      case "IE":
      case "ie":
      case "Explorer":
      case "explorer":
      case "InternetExplorer":
      case "internetexplorer":
      case "Internet Explorer":
      case "internet explorer":
        return DesiredCapabilities.InternetExplorer();

      default:
        throw new InvalidOperationException($"Unreconised requested browser type: {BrowserType}");
      }
    }

    protected virtual Uri GetRemoteUri()
    {
      return new Uri(RemoteAddress);
    }

    public RemoteWebDriverFactory()
    {
      BrowserType = "Chrome";
      RemoteAddress = "http://127.0.0.1:4444/wd/hub";
      CommandTimeoutSeconds = 60;
    }
  }
}
