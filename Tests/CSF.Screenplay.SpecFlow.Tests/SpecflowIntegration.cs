using System;
using System.IO;
using BoDi;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Scenarios;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class SpecflowIntegration : ScreenplayBinding
  {
    static IModelBuildingReporter reporter;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
      var builder = new ServiceRegistryBuilder();
      RegisterServices(builder);
      ServiceRegistry = builder.BuildRegistry();
      NotifyBeginTestRun();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
      NotifyCompleteTestRun();
      WriteReport();
    }

    static void RegisterServices(IServiceRegistryBuilder builder)
    {
      reporter = GetReporter();

      builder.RegisterCast();
      builder.RegisterReporter(reporter);
    }

    static IModelBuildingReporter GetReporter() => new ReportBuildingReporter();

    static void WriteReport()
    {
      if(reporter == null)
        return;

      var report = reporter.GetReport();
      TextReportWriter.WriteToFile(report, "SpecFlow.report.txt");
    }

    public SpecflowIntegration(IObjectContainer container) : base(container) {}
  }
}
