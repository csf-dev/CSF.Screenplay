using System;
using System.Collections.Generic;
using CSF.Screenplay.Integration;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Tests;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

[assembly:ScreenplayAssembly(typeof(CustomIntegration))]

namespace CSF.Screenplay.Web.Tests
{
  public class CustomIntegration : ScreenplayIntegration
  {
    protected override void CustomiseIntegration(IScreenplayIntegrationHelper integrationHelper)
    {
      integrationHelper.UseCast();
      integrationHelper.UseReporter(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WriteReport(WriteReport);
      });
      integrationHelper.UseUriTransformer(new RootUriPrependingTransformer("http://localhost:8080/"));
      integrationHelper.UseWebDriver(GetWebDriver);
      integrationHelper.UseWebBrowser();
    }

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

    void WriteReport(Reporting.Models.Report report)
    {
      var path = "NUnit.report.txt";
      TextReportWriter.WriteToFile(report, path);
    }
  }
}
