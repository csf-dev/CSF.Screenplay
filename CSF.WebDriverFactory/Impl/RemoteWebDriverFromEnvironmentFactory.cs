using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets a web driver for a remote web browser, but also
  /// is capable of configuring that remote web driver instance via environment variables.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This factory type is intended for usage with CI build systems, in which they may perform multiple builds,
  /// with differing environment variables between each build.  This may be used with this factory to trigger different
  /// builds to use different web browsers (for example).
  /// </para>
  /// </remarks>
  public class RemoteWebDriverFromEnvironmentFactory : RemoteWebDriverFactory
  {
    /// <summary>
    /// Gets or sets the name of the environment variable used to override the configured browser.
    /// </summary>
    /// <value>The browser name variable.</value>
    public string BrowserNameVar { get; set; }

    /// <summary>
    /// Gets or sets the name of the environment variable used to override the configured browser version.
    /// </summary>
    /// <value>The browser version variable.</value>
    public string BrowserVersionVar { get; set; }

    /// <summary>
    /// Gets or sets the name of the environment variable used to override the configured platform.
    /// </summary>
    /// <value>The platform variable.</value>
    public string PlatformVar { get; set; }

    /// <summary>
    /// Gets or sets the name of the environment variable used to override the configured remote web driver address.
    /// </summary>
    /// <value>The remote address variable.</value>
    public string RemoteAddressVar { get; set; }

    /// <summary>
    /// Gets or sets the name of the environment variable used to override the configured command timeout.
    /// </summary>
    /// <value>The command timeout seconds variable.</value>
    public string CommandTimeoutSecondsVar { get; set; }

    /// <summary>
    /// Gets the resolved name of the web browser.
    /// </summary>
    /// <returns>The resolved browser name.</returns>
    public string GetResolvedBrowserName()
    {
      return GetFromEnvironmentOrSetting(BrowserNameVar, BrowserName);
    }

    /// <summary>
    /// Gets the resolved browser version.
    /// </summary>
    /// <returns>The resolved browser version.</returns>
    public string GetResolvedBrowserVersion()
    {
      return GetFromEnvironmentOrSetting(BrowserVersionVar, BrowserVersion);
    }

    /// <summary>
    /// Gets the resolved platform name.
    /// </summary>
    /// <returns>The resolved platform.</returns>
    public string GetResolvedPlatform()
    {
      return GetFromEnvironmentOrSetting(PlatformVar, Platform);
    }

    /// <summary>
    /// Gets the URI to the remote web driver.
    /// </summary>
    /// <returns>The remote URI.</returns>
    protected override Uri GetRemoteUri()
    {
      if(String.IsNullOrEmpty(RemoteAddressVar))
      {
        return base.GetRemoteUri();
      }

      var uri = GetEnv(RemoteAddressVar);
      return new Uri(uri);
    }

    /// <summary>
    /// Gets the timeout.
    /// </summary>
    /// <returns>The timeout.</returns>
    protected override TimeSpan GetTimeout()
    {
      if(String.IsNullOrEmpty(CommandTimeoutSecondsVar))
      {
        return base.GetTimeout();
      }

      int seconds;
      var val = GetEnv(CommandTimeoutSecondsVar);
      if(Int32.TryParse(val, out seconds))
      {
        return TimeSpan.FromSeconds(seconds);
      }

      return base.GetTimeout();
    }

    /// <summary>
    /// Configures the capabilities desired for the current instance.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected override void ConfigureCapabilities(DesiredCapabilities caps)
    {
      SetCapabilityToEnvOrSetting(caps, CapabilityType.BrowserName, BrowserNameVar, BrowserName);
      SetCapabilityToEnvOrSetting(caps, CapabilityType.Version, BrowserVersionVar, BrowserVersion);
      SetCapabilityToEnvOrSetting(caps, CapabilityType.Platform, PlatformVar, Platform);
    }

    /// <summary>
    /// Sets the value of a single capability into a capabilities instance, using either the given environment
    /// variable name (if provided) or the functionality from
    /// <see cref="RemoteWebDriverFactory.SetCapabilityIfNotNull"/> if not.
    /// </summary>
    /// <param name="caps">The capabilities instance to modify.</param>
    /// <param name="name">The capability name.</param>
    /// <param name="value">The capability value.</param>
    /// <param name="envName">The name of an environment variable from which to get a value.</param>
    protected virtual void SetCapabilityToEnvOrSetting(DesiredCapabilities caps, string name, string envName, string value)
    {
      var resolvedValue = GetFromEnvironmentOrSetting(envName, value);
      base.SetCapabilityIfNotNull(caps, name, resolvedValue);
    }

    /// <summary>
    /// Gets a value from either the environment variable or a provided setting.
    /// </summary>
    /// <returns>The resolved value.</returns>
    /// <param name="envName">Environment variable name.</param>
    /// <param name="value">The provided setting.</param>
    protected virtual string GetFromEnvironmentOrSetting(string envName, string value)
    {
      if(String.IsNullOrEmpty(envName))
        return value;

      return GetEnv(envName);
    }

    string GetEnv(string name)
    {
      return Environment.GetEnvironmentVariable(name);
    }
  }
}
