using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

[assembly: CSF.Screenplay.Web.Tests.ScreenplayAssembly]

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayAssemblyAttribute : NUnit.ScreenplayAssemblyAttribute
  {
    static IWebDriver driver;
    IModelBuildingReporter reporter;

    protected override void RegisterServices(IServiceRegistryBuilder builder)
    {
      reporter = GetReporter();

      builder.RegisterCast();
      builder.RegisterReporter(reporter);
      builder.RegisterUriTransformer(GetUriTransformer);
      builder.RegisterWebDriver(GetWebDriver());
      builder.RegisterWebBrowser();
    }

    protected override void RegisterBeforeAndAfterTestRunEvents(IProvidesTestRunEvents testRunEvents)
    {
      if(reporter != null)
        reporter.Subscribe(testRunEvents);

      testRunEvents.CompleteTestRun += OnCompleteTestRun;

      base.RegisterBeforeAndAfterTestRunEvents(testRunEvents);
    }

    IModelBuildingReporter GetReporter() => new ReportBuildingReporter();

    IWebDriver GetWebDriver()
    {
      var provider = new ConfigurationWebDriverFactoryProvider();
      var factory = provider.GetFactory();
      driver = factory.GetWebDriver();
      return driver;
    }

    IUriTransformer GetUriTransformer(IServiceResolver res)
      => new RootUriPrependingTransformer("http://localhost:8080/");

    void OnCompleteTestRun(object sender, EventArgs ev)
    {
      DisposeWebDriver();
      WriteReport();
    }

    void DisposeWebDriver()
    {
      if(driver != null)
        driver.Dispose();
    }

    void WriteReport()
    {
      if(reporter == null)
        return;

      var path = "NUnit.report.txt";
      var report = reporter.GetReport();

      TextReportWriter.WriteToFile(report, path);
    }
  }
}
