using System;
using System.IO;
using CSF.Screenplay.Actors;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Tests
{
  [SetUpFixture]
  public class WebdriverTestSetup
  {
    [OneTimeSetUp]
    public void OnetimeSetup()
    {
      Stage.UriTransformer = new RootUriPrependingTransformer("http://localhost:8080/");
      Stage.Reporter = new ReportBuildingReporter();
      Stage.Cast.NewActorCallback = ConfigureActor;
    }

    [OneTimeTearDown]
    public void OnetimeTeardown()
    {
      Stage.DisposeCurrentWebDriver();
      Stage.Reporter.CompleteTestRun();
      WriteReport();
    }

    void WriteReport()
    {
      if(Stage.Reporter is IModelBuildingReporter)
      {
        var report = ((IModelBuildingReporter) Stage.Reporter).GetReport();
        using(var writer = new StreamWriter("screenplay-report.txt"))
        {
          var reportWriter = new TextReportWriter(writer);
          reportWriter.Write(report);
          writer.Flush();
        }
      }
    }

    void ConfigureActor(IActor actor)
    {
      Stage.Reporter.Subscribe(actor);

      var browseTheWeb = Stage.GetDefaultWebBrowsingAbility();
      actor.IsAbleTo(browseTheWeb);
    }
  }
}
