using System;

namespace CSF.WebDriverFactory.Impl
{
  public abstract class SauceConnectWebDriverFactory : RemoteWebDriverFromEnvironmentFactory
  {
    const string
      TunnelIdCapability = "tunnel-identifier",
      UsernameCapability = "username",
      ApiKeyCapability = "accessKey",
      TestNameCapabilityName = "name",
      BuildNameCapability = "build";

    public static string TestNameCapability => TestNameCapabilityName;

    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);

      caps.SetCapability(TunnelIdCapability, GetTunnelId());
      caps.SetCapability(UsernameCapability, GetSauceUsername());
      caps.SetCapability(ApiKeyCapability, GetSauceAccessKey());
      caps.SetCapability(BuildNameCapability, GetSauceBuildName());
    }

    protected abstract string GetTunnelId();

    protected abstract string GetSauceUsername();

    protected abstract string GetSauceAccessKey();

    protected abstract string GetSauceBuildName();
  }
}
