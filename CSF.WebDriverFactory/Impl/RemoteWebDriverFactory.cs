using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets a web driver for a remote web browser.
  /// </summary>
  public class RemoteWebDriverFactory : IWebDriverFactory
  {
    /// <summary>
    /// Gets or sets the desired browser name.
    /// </summary>
    /// <value>The name of the browser.</value>
    public string BrowserName { get; set; }

    /// <summary>
    /// Gets or sets the desired browser version.
    /// </summary>
    /// <value>The browser version.</value>
    public string BrowserVersion { get; set; }

    /// <summary>
    /// Gets or sets the desired platform.
    /// </summary>
    /// <value>The platform.</value>
    public string Platform { get; set; }

    /// <summary>
    /// Gets or sets the endpoint address for the remote web browser driver.
    /// </summary>
    /// <value>The remote address.</value>
    public string RemoteAddress { get; set; }

    /// <summary>
    /// Gets or sets the timeout (in seconds) between issuing a command to the web driver and receiving a response.
    /// </summary>
    /// <value>The command timeout seconds.</value>
    public int CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public virtual IWebDriver GetWebDriver()
    {
      return GetWebDriver(null);
    }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver(IDictionary<string,object> capabilities)
    {
      var baseCapabilities = GetCapabilities();
      var uri = GetRemoteUri();

      if(capabilities != null)
      {
        foreach(var cap in capabilities)
        {
          baseCapabilities.SetCapability(cap.Key, cap.Value);
        }
      }

      var timeout = GetTimeout();
      return new RemoteWebDriver(uri, baseCapabilities, timeout);
    }

    /// <summary>
    /// Gets a set of <c>ICapabilities</c> from the current state of this instance.
    /// </summary>
    /// <returns>The capabilities.</returns>
    protected virtual DesiredCapabilities GetCapabilities()
    {
      var caps = new DesiredCapabilities();
      ConfigureCapabilities(caps);
      return caps;
    }

    /// <summary>
    /// Configures the capabilities desired for the current instance.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected virtual void ConfigureCapabilities(DesiredCapabilities caps)
    {
      caps.SetCapability(CapabilityType.BrowserName, BrowserName);

      SetCapabilityIfNotNull(caps, CapabilityType.Version, BrowserVersion);
      SetCapabilityIfNotNull(caps, CapabilityType.Platform, Platform);
    }

    /// <summary>
    /// Gets the timeout.
    /// </summary>
    /// <returns>The timeout.</returns>
    protected virtual TimeSpan GetTimeout()
    {
      return TimeSpan.FromSeconds(CommandTimeoutSeconds);
    }

    /// <summary>
    /// Sets a desired capability key/value pair into the given capabilities instance, but only if the capability
    /// value is not <c>null</c>.
    /// </summary>
    /// <param name="caps">The capabilities instance to modify.</param>
    /// <param name="name">The capability name.</param>
    /// <param name="value">The capability value.</param>
    protected virtual void SetCapabilityIfNotNull(DesiredCapabilities caps, string name, string value)
    {
      if(value == null)
        return;

      caps.SetCapability(name, value);
    }

    /// <summary>
    /// Gets the URI to the remote web driver.
    /// </summary>
    /// <returns>The remote URI.</returns>
    protected virtual Uri GetRemoteUri()
    {
      return new Uri(RemoteAddress);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverFactory.Impl.RemoteWebDriverFactory"/> class.
    /// </summary>
    public RemoteWebDriverFactory()
    {
      BrowserName = "Chrome";
      RemoteAddress = "http://127.0.0.1:4444/wd/hub";
      CommandTimeoutSeconds = 60;
    }
  }
}
