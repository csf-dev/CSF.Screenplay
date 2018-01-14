using System.Collections.Generic;
using CSF.MicroDi;
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
      builder.UseSharedUriTransformer(new RootUriPrependingTransformer("http://localhost:8080/"));
      builder.UseWebDriverFactory();
      builder.UseWebDriver(GetWebDriver);
      builder.UseWebBrowser(GetWebBrowser);
    }

    IWebDriver GetWebDriver(IResolvesServices resolver)
    {
      var factory = resolver.Resolve<IWebDriverFactory>();

      var caps = new Dictionary<string,object>();
      if(factory is SauceConnectWebDriverFactory)
      {
        var testName = GetTestName(resolver);
        caps.Add(WebDriverFactory.Impl.SauceConnectWebDriverFactory.TestNameCapability, testName);
      }

      return factory.GetWebDriver(caps);
    }

    BrowseTheWeb GetWebBrowser(IResolvesServices resolver)
    {
      var driver = resolver.Resolve<IWebDriver>();

      IWebDriverFactory webDriverFactory;
      if(!resolver.TryResolve(out webDriverFactory))
        webDriverFactory = null;

      IUriTransformer uriTransformer;
      if(!resolver.TryResolve(out uriTransformer))
        uriTransformer = null;

      var ability = new BrowseTheWeb(driver, uriTransformer?? NoOpUriTransformer.Default);

      ConfigureBrowserCapabilities(ability, webDriverFactory);

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

    string GetTestName(IResolvesServices resolver)
    {
      var scenarioName = resolver.Resolve<IScenarioName>();
      return $"{scenarioName.FeatureId.Name} -> {scenarioName.ScenarioId.Name}";
    }

    void WriteReport(IObjectFormattingService formatter, Report report)
    {
      var path = "NUnit.report.txt";
      TextReportWriter.WriteToFile(report, path, formatter);
    }
  }
}
