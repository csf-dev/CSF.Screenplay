using System;
using System.IO;
using BoDi;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Reporting.Models;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class SpecflowIntegration : ScreenplayBinding
  {
    readonly IObjectContainer container;

    public SpecflowIntegration(IObjectContainer container)
    {
      this.container = container;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
      RegisterCast();
      RegisterReporter();
      ConfigureActorsInCast();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
      DisposeWebBrowsingAbility();
      InformReporterOfCompletion();

      var report = GetReportModel();
      if(report != null)
        WriteReport(report);
    }

    public override void BeforeScenario()
    {
      base.BeforeScenario();
      container.RegisterInstanceAs(Context);
    }

    static void WriteReport(Report report)
    {
      using(var writer = new StreamWriter("SpecFlow.report.txt"))
      {
        var reportWriter = new TextReportWriter(writer);
        reportWriter.Write(report);
        writer.Flush();
      }
    }
  }
}
