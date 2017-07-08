using System;
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

    public static IWebDriver WebDriver => webDriver;

    public static IUriTransformer DefaultUriTransformer => defaultUriTransformer;

    public static BrowseTheWeb GetDefaultWebBrowsingAbility()
    {
      return new BrowseTheWeb(WebDriver, DefaultUriTransformer, true);
    }

    public static void TakeScreenshot(Type clazz, string testName)
    {
      var shotTaker = WebDriver as ITakesScreenshot;

      if(shotTaker == null)
        return;

      // shotTaker.GetScreenshot().SaveAsFile($"{clazz.Name}.{testName}.jpeg", ScreenshotImageFormat.Jpeg);
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

      return joe;
    }

    [OneTimeSetUp]
    public void OnetimeSetup()
    {
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
      defaultUriTransformer = new RootUrlAppendingTransformer("http://localhost:8080/");
    }
  }
}
