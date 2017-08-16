using System;
using CSF.WebDriverFactory.Impl;

namespace CSF.Screenplay.Web.Tests
{
  public class SauceConnectWebDriverFactory : RemoteWebDriverFromEnvironmentFactory
  {
    const string
      TunnelIdCapability = "tunnel-identifier",
      TunnelIdEnvVariable = "TRAVIS_JOB_NUMBER",
      SauceUsernameEnvVariable = "SAUCE_USERNAME",
      SauceApiKeyEnvVariable = "SAUCE_ACCESS_KEY",
      SauceLabsEndpointFormat = "http://{0}:{1}@ondemand.saucelabs.com/wd/hub";

    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);
      caps.SetCapability(TunnelIdCapability, Environment.GetEnvironmentVariable(TunnelIdEnvVariable));
    }

    protected override Uri GetRemoteUri()
    {
      var username = Environment.GetEnvironmentVariable(SauceUsernameEnvVariable);
      var apiKey = Environment.GetEnvironmentVariable(SauceApiKeyEnvVariable);
      var endpoint = String.Format(SauceLabsEndpointFormat, username, apiKey);
      return new Uri(endpoint);
    }
  }
}
