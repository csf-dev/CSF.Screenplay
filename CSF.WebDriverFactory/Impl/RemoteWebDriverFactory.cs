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
    public string BrowserName { get; set; }

    public string BrowserVersion { get; set; }

    public string Platform { get; set; }

    public string RemoteAddress { get; set; }

    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public virtual IWebDriver GetWebDriver()
    {
      var capabilities = GetCapabilities();
      var uri = GetRemoteUri();
      var timeout = GetTimeout();
      return new RemoteWebDriver(uri, capabilities, timeout);
    }

    protected virtual ICapabilities GetCapabilities()
    {
      var caps = new DesiredCapabilities();

      caps.SetCapability(CapabilityType.BrowserName, BrowserName);

      SetCapabilityIfNotNull(caps, CapabilityType.Version, BrowserVersion);
      SetCapabilityIfNotNull(caps, CapabilityType.Platform, Platform);

      return caps;
    }

    protected virtual TimeSpan GetTimeout()
    {
      return TimeSpan.FromSeconds(CommandTimeoutSeconds);
    }

    protected virtual void SetCapabilityIfNotNull(DesiredCapabilities caps, string name, string value)
    {
      if(value == null)
        return;

      caps.SetCapability(name, value);
    }

    protected virtual Uri GetRemoteUri()
    {
      return new Uri(RemoteAddress);
    }

    public RemoteWebDriverFactory()
    {
      BrowserName = "Chrome";
      RemoteAddress = "http://127.0.0.1:4444/wd/hub";
      CommandTimeoutSeconds = 60;
    }
  }
}
