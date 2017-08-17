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
      SauceApiKeyEnvVariable = "SAUCE_ACCESS_KEY",
      TestNameCapability = "name",
      BuildNameCapability = "build",
      TravisBuildNumberEnvVariable = "TRAVIS_BUILD_NUMBER",
      TravisCommitEnvVariable = "TRAVIS_COMMIT",
      BrowserNameEnvVariable = "BROWSER_NAME",
      TravisJobNumberEnvVariable = "TRAVIS_JOB_NUMBER";

    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);

      caps.SetCapability(TunnelIdCapability, GetTunnelId());
      caps.SetCapability(UsernameCapability, GetSauceUsername());
      caps.SetCapability(ApiKeyCapability, GetSauceAccessKey());
      caps.SetCapability(BuildNameCapability, GetSauceBuildName());
      caps.SetCapability(TestNameCapability, GetSauceJobName());
    }

    string GetTunnelId() => Environment.GetEnvironmentVariable(TunnelIdEnvVariable);

    string GetSauceUsername() => Environment.GetEnvironmentVariable(SauceUsernameEnvVariable);

    string GetSauceAccessKey() => Environment.GetEnvironmentVariable(SauceApiKeyEnvVariable);

    string GetBuildNumber() => Environment.GetEnvironmentVariable(TravisBuildNumberEnvVariable);

    string GetCommitHash() => Environment.GetEnvironmentVariable(TravisCommitEnvVariable);

    string GetBrowserName() => Environment.GetEnvironmentVariable(BrowserNameEnvVariable);

    string GetJobNumber() => Environment.GetEnvironmentVariable(TravisJobNumberEnvVariable);

    string GetSauceBuildName() => $"build-{GetBuildNumber()}-{GetCommitHash()}";

    string GetSauceJobName() => $"Travis job {GetJobNumber()}; {GetBrowserName()}";
  }
}
