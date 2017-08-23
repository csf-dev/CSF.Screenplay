using System;
using System.Collections.Generic;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayRegistrations : ServiceRegistrationProvider
  {
    protected override void RegisterServices(IServiceRegistryBuilder builder)
    {
      builder.RegisterDefaultModelBuildingReporter();
      builder.RegisterCast();
      builder.RegisterUriTransformer(GetUriTransformer);
      builder.RegisterWebDriver(GetWebDriver);
      builder.RegisterWebBrowser();
    }

    IUriTransformer GetUriTransformer(IServiceResolver res)
      => new RootUriPrependingTransformer("http://localhost:8080/");

    IWebDriver GetWebDriver(IScreenplayScenario scenario)
    {
      var provider = new ConfigurationWebDriverFactoryProvider();
      var factory = provider.GetFactory();

      var caps = new Dictionary<string,object>();

      if(factory is SauceConnectWebDriverFactory)
      {
        caps.Add(SauceConnectWebDriverFactory.TestNameCapability, GetTestName(scenario));
      }

      return factory.GetWebDriver(caps);
    }

    string GetTestName(IScreenplayScenario scenario)
      => $"{scenario.FeatureId.Name} -> {scenario.ScenarioId.Name}";
  }
}
