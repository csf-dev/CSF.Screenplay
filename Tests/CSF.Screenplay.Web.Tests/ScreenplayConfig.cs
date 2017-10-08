using System.Collections.Generic;
using CSF.Screenplay.Integration;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Reporting;
using CSF.Screenplay.Web.Tests;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

[assembly: ScreenplayAssembly(typeof(ScreenplayConfig))]

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayConfig : IIntegrationConfig
  {
    public void Configure(IIntegrationConfigBuilder builder)
    {
      builder.UseCast();
      builder.UseReporting(config => {
        config
          .SubscribeToActorsCreatedInCast()
          .WriteReport(WriteReport)
          .WithFormatter<StringArrayFormatter>()
          .WithFormatter<OptionCollectionFormatter>()
          .WithFormatter<ElementCollectionFormatter>();
      });
      builder.UseUriTransformer(new RootUriPrependingTransformer("http://localhost:8080/"));
      builder.UseWebDriverFactory();
      builder.UseWebDriver(GetWebDriver);
      builder.UseWebBrowser(GetWebBrowser);
    }

    IWebDriver GetWebDriver(IServiceResolver scenario)
    {
      var factory = scenario.GetService<IWebDriverFactory>();

      var caps = new Dictionary<string,object>();
      if(factory is SauceConnectWebDriverFactory)
      {
        var testName = GetTestName(scenario);
        caps.Add(WebDriverFactory.Impl.SauceConnectWebDriverFactory.TestNameCapability, testName);
      }

      return factory.GetWebDriver(caps);
    }

    BrowseTheWeb GetWebBrowser(IServiceResolver scenario)
    {
      var factory = scenario.GetOptionalService<IWebDriverFactory>();
      var driver = scenario.GetService<IWebDriver>();
      var transformer = scenario.GetOptionalService<IUriTransformer>();

      var ability = new BrowseTheWeb(driver, transformer?? NoOpUriTransformer.Default);

      ConfigureBrowserCapabilities(ability, factory);

      return ability;
    }

    void ConfigureBrowserCapabilities(BrowseTheWeb ability, IWebDriverFactory factory)
    {
      var browserName = factory.GetBrowserName();

      ability.AddCapabilityExceptWhereUnsupported(Capabilities.ClearDomainCookies,
                                                  browserName,
                                                  BrowserName.Edge);
      ability.AddCapabilityWhereSupported(Capabilities.EnterDatesInLocaleFormat,
                                          browserName,
                                          BrowserName.Chrome);
      ability.AddCapabilityExceptWhereUnsupported(Capabilities.EnterDatesAsIsoStrings,
                                                  browserName,
                                                  BrowserName.Chrome,
                                                  BrowserName.Edge);
    }

    string GetTestName(IServiceResolver resolver)
    {
      var scenarioName = resolver.GetService<IScenarioName>();
      return $"{scenarioName.FeatureId.Name} -> {scenarioName.ScenarioId.Name}";
    }

    void WriteReport(IObjectFormattingService formatter, Report report)
    {
      var path = "NUnit.report.txt";
      TextReportWriter.WriteToFile(report, path, formatter);
    }
  }
}
