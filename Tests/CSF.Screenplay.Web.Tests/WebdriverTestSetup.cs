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
      Stage.UriTransformer = new RootUriPrependingTransformer("http://localhost:80/");
      Stage.Reporter = new TextReporter(TestContext.Out);
      Stage.Cast.NewActorCallback = ConfigureActor;
    }

    [OneTimeTearDown]
    public void OnetimeTeardown()
    {
      Stage.DisposeCurrentWebDriver();
      Stage.Reporter.CompleteTestRun();
      TestContext.Out.Flush();
    }

    void ConfigureActor(IActor actor)
    {
      Stage.Reporter.Subscribe(actor);

      var browseTheWeb = Stage.GetDefaultWebBrowsingAbility();
      actor.IsAbleTo(browseTheWeb);
    }
  }
}
