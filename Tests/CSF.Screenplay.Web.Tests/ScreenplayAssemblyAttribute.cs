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
    IModelBuildingReporter reporter;

    protected override void RegisterServices(IServiceRegistryBuilder builder)
    {
      reporter = GetReporter();

      builder.RegisterCast();
      builder.RegisterReporter(reporter);
      builder.RegisterWebDriver(GetWebDriver);
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

    IWebDriver GetWebDriver(IServiceResolver res)
    {
      var provider = new ConfigurationWebDriverFactoryProvider();
      var factory = provider.GetFactory();
      return factory.GetWebDriver();
    }

    void OnCompleteTestRun(object sender, EventArgs ev)
    {
      WriteReport();
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
