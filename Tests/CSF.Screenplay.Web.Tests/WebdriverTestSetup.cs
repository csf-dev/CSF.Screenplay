using System;
using System.IO;
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

    public static IWebDriver WebDriver => webDriver;

    public static IUriTransformer DefaultUriTransformer => defaultUriTransformer;

    public static BrowseTheWeb GetDefaultWebBrowsingAbility()
    {
      return new BrowseTheWeb(WebDriver, DefaultUriTransformer, true);
    }

    public static Actor GetJoe()
    {
      var joe = new Actor("Joe");

      var browseTheWeb = GetDefaultWebBrowsingAbility();
      joe.IsAbleTo(browseTheWeb);

      joe.BeginPerformance += (sender, e) => {
        Console.WriteLine(e.Performable.GetReport(e.Actor));
      };
      joe.PerformanceResult += (sender, e) => {
        Console.WriteLine("  the result was {0}", e.Result);
      };
      joe.PerformanceFailed += (sender, e) => {
        Console.WriteLine("-- FAILED --\n\n{0}", e.Exception);
      };
      joe.BeginThen += (sender, e) => {
        TakeScreenshotBeforeThen();
      };

      return joe;
    }

    static void TakeScreenshotBeforeThen()
    {
      var ctx = TestContext.CurrentContext;
      var testName = ctx.Test.FullName;

      var screenshotService = new ScreenshotService(WebDriver, new DirectoryInfo(screenshotDir));

      screenshotService.TakeAndSaveScreenshot(testName);
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
    }

    [OneTimeTearDown]
    public void OnetimeTeardown()
    {
      webDriver.Dispose();
    }

    IWebDriver GetWebDriver()
    {
      var webdriverFactoryProvider = new ConfigurationWebDriverFactoryProvider();
      var webdriverFactory = webdriverFactoryProvider.GetFactory();
      return webdriverFactory.GetWebDriver();
    }

    static WebdriverTestSetup()
    {
      screenshotDir = Path.Combine(Environment.CurrentDirectory, "Screenshots");
      defaultUriTransformer = new RootUrlAppendingTransformer("http://localhost:8080/");
    }
  }
}
