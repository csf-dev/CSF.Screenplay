using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web
{
  public static class WebBrowsingIntegrationHelperExtensions
  {
    public static void UseWebDriver(this IIntegrationConfigBuilder helper,
                                    Func<IScreenplayScenario,IWebDriver> factory,
                                    string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      helper.RegisterServices.Add((builder) => {
        builder.RegisterPerScenario(factory, name);
      });

      helper.AfterScenario.Add((scenario) => {
        var webDriver = scenario.GetService<IWebDriver>();
        webDriver.Dispose();
      });
    }

    public static void UseWebDriver(this IIntegrationConfigBuilder helper,
                                    Func<IWebDriver> initialiser,
                                    string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(initialiser == null)
        throw new ArgumentNullException(nameof(initialiser));

      helper.RegisterServices.Add((builder) => {
        builder.RegisterSingleton(initialiser, name);
      });
    }

    public static void UseUriTransformer(this IIntegrationConfigBuilder helper,
                                              Func<IServiceResolver,IUriTransformer> factory,
                                              string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      helper.RegisterServices.Add((builder) => {
        builder.RegisterPerScenario(factory, name);
      });
    }

    public static void UseUriTransformer(this IIntegrationConfigBuilder helper,
                                              IUriTransformer transformer,
                                              string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(transformer == null)
        throw new ArgumentNullException(nameof(transformer));

      helper.RegisterServices.Add((builder) => {
        builder.RegisterSingleton(transformer, name);
      });
    }

    public static void UseWebBrowser(this IIntegrationConfigBuilder helper,
                                     Func<IScreenplayScenario,BrowseTheWeb> factory = null,
                                     string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      factory = factory?? CreateWebBrowser;

      helper.RegisterServices.Add(builder => builder.RegisterPerScenario(factory, name));
    }

    static BrowseTheWeb CreateWebBrowser(IServiceResolver resolver)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      var driver = resolver.GetService<IWebDriver>();
      var transformer = resolver.GetOptionalService<IUriTransformer>();
      return new BrowseTheWeb(driver, transformer?? NoOpUriTransformer.Default);
    }
  }
}
