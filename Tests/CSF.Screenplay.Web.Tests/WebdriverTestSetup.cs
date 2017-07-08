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
