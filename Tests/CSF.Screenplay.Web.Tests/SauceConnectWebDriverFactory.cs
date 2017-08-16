using System;
using CSF.WebDriverFactory.Impl;

namespace CSF.Screenplay.Web.Tests
{
  public class SauceConnectWebDriverFactory : RemoteWebDriverFromEnvironmentFactory
  {
    const string
      TunnelIdCapability = "tunnel-identifier",
      TunnelIdEnvVariable = "TRAVIS_JOB_NUMBER";

    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);
      caps.SetCapability(TunnelIdCapability, Environment.GetEnvironmentVariable(TunnelIdEnvVariable));
    }
  }
}
