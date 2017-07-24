using System;
using System.IO;
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
    static IUriTransformer defaultUriTransformer;
    static string screenshotDir;
    internal static TextReporter Reporter;

    public static IWebDriver WebDriver => webDriver;

    public static IUriTransformer DefaultUriTransformer => defaultUriTransformer;

    public static BrowseTheWeb GetDefaultWebBrowsingAbility()
    {
      return new BrowseTheWeb(WebDriver, DefaultUriTransformer, true);
    }

    public static Actor GetJoe()
    {
      var joe = new Actor("Joe");

      Reporter.Subscribe(joe);

      var browseTheWeb = GetDefaultWebBrowsingAbility();
      joe.IsAbleTo(browseTheWeb);

      joe.BeginThen += (sender, e) => {
        TakeScreenshotBeforeThen();
      };

      return joe;
    }

    static void TakeScreenshotBeforeThen()
    {
      var screenshotService = new ScreenshotService(WebDriver, new DirectoryInfo(screenshotDir));

      screenshotService.TakeAndSaveScreenshot(GetCurrentTestName());
    }

    static void DeleteScreenshotsDir()
    {
      var dir = new DirectoryInfo(screenshotDir);
      if(!dir.Exists)
        return;

      dir.Delete(true);
    }

    [OneTimeSetUp]
    public void OnetimeSetup()
    {
      DeleteScreenshotsDir();
      webDriver = GetWebDriver();

      Reporter = new TextReporter(TestContext.Out);
      Reporter.BeginNewTestRun();
    }

    [OneTimeTearDown]
    public void OnetimeTeardown()
    {
      webDriver.Dispose();
      Reporter.CompleteTestRun();
      TestContext.Out.Flush();
    }

    static string GetCurrentTestName() => TestContext.CurrentContext.Test.FullName;

    IWebDriver GetWebDriver()
    {
      var webdriverFactoryProvider = new ConfigurationWebDriverFactoryProvider();
      var webdriverFactory = webdriverFactoryProvider.GetFactory();
      return webdriverFactory.GetWebDriver();
    }

    static WebdriverTestSetup()
    {
      screenshotDir = Path.Combine(Environment.CurrentDirectory, "Screenshots");
      defaultUriTransformer = new RootUriPrependingTransformer("http://localhost:8080/");
    }
  }
}
