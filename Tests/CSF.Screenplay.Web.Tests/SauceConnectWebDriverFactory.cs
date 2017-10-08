using System;

namespace CSF.Screenplay.Web.Tests
{
  public class SauceConnectWebDriverFactory : CSF.WebDriverFactory.Impl.SauceConnectWebDriverFactory
  {
    const string
      TunnelIdEnvVariable = "TRAVIS_JOB_NUMBER",
      SauceUsernameEnvVariable = "SAUCE_USERNAME",
      SauceApiKeyEnvVariable = "SAUCE_ACCESS_KEY",
      TravisJobNumberEnvVariable = "TRAVIS_JOB_NUMBER";

    protected override string GetSauceUsername() => Environment.GetEnvironmentVariable(SauceUsernameEnvVariable);

    protected override string GetSauceAccessKey() => Environment.GetEnvironmentVariable(SauceApiKeyEnvVariable);

    protected override string GetSauceTunnelId() => Environment.GetEnvironmentVariable(TunnelIdEnvVariable);

    protected override string GetSauceBuildName()=> $"Travis job {GetJobNumber()}; {GetBrowserName()}";

    string GetJobNumber() => Environment.GetEnvironmentVariable(TravisJobNumberEnvVariable);
  }
}
