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
    static IWebDriver webDriver;
    readonly IUriTransformer defaultUriTransformer;

    [OneTimeSetUp]
    public void OnetimeSetup()
    {
      webDriver = GetWebDriver();

      Stage.Reporter = new TextReporter(TestContext.Out);
      Stage.Cast.NewActorCallback = ConfigureActor;
    }

    [OneTimeTearDown]
    public void OnetimeTeardown()
    {
      webDriver.Dispose();
      Stage.Reporter.CompleteTestRun();
      TestContext.Out.Flush();
    }

    void ConfigureActor(IActor actor)
    {
      Stage.Reporter.Subscribe(actor);

      var browseTheWeb = new BrowseTheWeb(webDriver, defaultUriTransformer, true);
      actor.IsAbleTo(browseTheWeb);
    }

    IWebDriver GetWebDriver()
    {
      var webdriverFactoryProvider = new ConfigurationWebDriverFactoryProvider();
      var webdriverFactory = webdriverFactoryProvider.GetFactory();
      return webdriverFactory.GetWebDriver();
    }

    public WebdriverTestSetup()
    {
      defaultUriTransformer = new RootUriPrependingTransformer("http://localhost:8080/");
    }
  }
}
