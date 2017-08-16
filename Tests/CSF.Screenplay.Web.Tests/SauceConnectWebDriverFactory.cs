using System;
using CSF.WebDriverFactory.Impl;

namespace CSF.Screenplay.Web.Tests
{
  public class SauceConnectWebDriverFactory : RemoteWebDriverFromEnvironmentFactory
  {
    const string
      TunnelIdCapability = "tunnel-identifier",
      UsernameCapability = "username",
      ApiKeyCapability = "accessKey",
      TunnelIdEnvVariable = "TRAVIS_JOB_NUMBER",
      SauceUsernameEnvVariable = "SAUCE_USERNAME",
      SauceApiKeyEnvVariable = "SAUCE_ACCESS_KEY";

    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);

      caps.SetCapability(TunnelIdCapability, GetTunnelId());
      caps.SetCapability(UsernameCapability, GetSauceUsername());
      caps.SetCapability(ApiKeyCapability, GetSauceAccessKey());
    }

    string GetTunnelId() => Environment.GetEnvironmentVariable(TunnelIdEnvVariable);

    string GetSauceUsername() => Environment.GetEnvironmentVariable(SauceUsernameEnvVariable);

    string GetSauceAccessKey() => Environment.GetEnvironmentVariable(SauceApiKeyEnvVariable);
  }
}
