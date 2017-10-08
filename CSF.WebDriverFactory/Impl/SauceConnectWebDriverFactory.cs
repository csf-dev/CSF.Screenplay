using System;

namespace CSF.WebDriverFactory.Impl
{
  /// <summary>
  /// Web driver factory base type designed for integration with Sauce Connect by Sauce Labs:
  /// https://saucelabs.com/
  /// </summary>
  public abstract class SauceConnectWebDriverFactory : RemoteWebDriverFromEnvironmentFactory
  {
    const string
      TunnelIdCapability = "tunnel-identifier",
      UsernameCapability = "username",
      ApiKeyCapability = "accessKey",
      TestNameCapability = "name",
      BuildNameCapability = "build";

    /// <summary>
    /// Configures the capabilities desired for the current instance.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);
      ConfigureSauceConnectCapabilities(caps);
      ConfigureSauceConnectTestName(caps);
    }

    /// <summary>
    /// Configures capabilities specific to Sauce Connect.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected virtual void ConfigureSauceConnectCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      caps.SetCapability(TunnelIdCapability, GetTunnelId());
      caps.SetCapability(UsernameCapability, GetSauceUsername());
      caps.SetCapability(ApiKeyCapability, GetSauceAccessKey());
      caps.SetCapability(BuildNameCapability, GetSauceBuildName());
    }

    /// <summary>
    /// Configures the optional 'test name' Sauce Connect capability.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected virtual void ConfigureSauceConnectTestName(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      if(SauceTestNameCallback == null)
        return;

      var testName = SauceTestNameCallback();
      if(testName != null)
        caps.SetCapability(TestNameCapability, testName);
    }

    /// <summary>
    /// Gets the tunnel identifier.
    /// </summary>
    /// <returns>The tunnel identifier.</returns>
    protected abstract string GetTunnelId();

    /// <summary>
    /// Gets the Sauce Connect username.
    /// </summary>
    /// <returns>The sauce username.</returns>
    protected abstract string GetSauceUsername();

    /// <summary>
    /// Gets the Sauce Connect access key.
    /// </summary>
    /// <returns>The sauce access key.</returns>
    protected abstract string GetSauceAccessKey();

    /// <summary>
    /// Gets the Sauce Labs 'build name' (a name for the current test run).
    /// </summary>
    /// <returns>The sauce build name.</returns>
    protected abstract string GetSauceBuildName();

    /// <summary>
    /// An optional function/callback which provides the name of the current test scenario (the Sauce Labs 'test name').
    /// </summary>
    /// <returns>The sauce test name callback.</returns>
    public virtual Func<string> SauceTestNameCallback { get; set; }
  }
}
