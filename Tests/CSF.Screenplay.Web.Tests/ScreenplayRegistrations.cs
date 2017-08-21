using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Tests
{
  public class ScreenplayRegistrations : ServiceRegistrationProvider
  {
    protected override void RegisterServices(IServiceRegistryBuilder builder)
    {
      builder.RegisterDefaultModelBuildingReporter();
      builder.RegisterCast();
      builder.RegisterUriTransformer(GetUriTransformer);
      builder.RegisterWebDriver(GetWebDriver());
      builder.RegisterWebBrowser();
    }

    IUriTransformer GetUriTransformer(IServiceResolver res)
      => new RootUriPrependingTransformer("http://localhost:8080/");

    IWebDriver GetWebDriver()
    {
      var provider = new ConfigurationWebDriverFactoryProvider();
      var factory = provider.GetFactory();
      return factory.GetWebDriver();
    }
  }
}
