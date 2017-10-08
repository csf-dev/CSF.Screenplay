using System;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using CSF.WebDriverFactory;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web
{
  /// <summary>
  /// Extension methods relating to the registration of web-browsing-related services into Screenplay.
  /// </summary>
  public static class WebBrowsingIntegrationBuilderExtensions
  {
    /// <summary>
    /// Registers a <see cref="IWebDriverFactory"/> for the creation of web drivers.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="name">Name.</param>
    public static void UseWebDriverFactory(this IIntegrationConfigBuilder builder,
                                           string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      builder.RegisterServices.Add(cfg => {
        cfg.RegisterSingleton(GetWebDriverFactory, name);
      });
    }

    /// <summary>
    /// Registers a Selenium IWebDriver into Screenplay, making use of a given factory.  This Web Driver
    /// will be created afresh and disposed with each scenario.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseWebDriver(this IIntegrationConfigBuilder helper,
                                    Func<IServiceResolver,IWebDriver> factory,
                                    string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      helper.RegisterServices.Add((builder) => {
        builder.RegisterPerScenario(factory, name);
      });
      helper.AfterScenario.Add(MarkWebDriverWithOutcome(name));
    }

    /// <summary>
    /// Registers a Selenium IWebDriver into Screenplay, making use of a given initialiser.  This single Web Driver
    /// created will be used/shared throughout the entire test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="initialiser">Initialiser.</param>
    /// <param name="name">Name.</param>
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

    /// <summary>
    /// Registers a URI transformer into Screenplay. The transformer will be created afresh for every scenario
    /// in the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
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

    /// <summary>
    /// Registers a URI transformer into Screenplay. The single transformer instance will be shared/reused throughout
    /// all scenarios in the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="transformer">Transformer.</param>
    /// <param name="name">Name.</param>
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

    /// <summary>
    /// Registers the ability to use a web browser with Screenplay.  A fresh ability instance will be created with each
    /// scenario throughout the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseWebBrowser(this IIntegrationConfigBuilder helper,
                                     Func<IServiceResolver,BrowseTheWeb> factory = null,
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

    static IWebDriverFactory GetWebDriverFactory()
    {
      var provider = new ConfigurationWebDriverFactoryProvider();
      return provider.GetFactory();
    }

    static Action<IScreenplayScenario> MarkWebDriverWithOutcome(string name)
    {
      return scenario => {
        var wdFactory = scenario.GetOptionalService<IWebDriverFactory>(name);
        if(wdFactory == null) return;
        if(!scenario.Success.HasValue) return;

        var driver = scenario.GetService<IWebDriver>(name);

        var success = scenario.Success.Value;
        if(success)
          wdFactory.MarkTestAsPassed(driver);
        else
          wdFactory.MarkTestAsFailed(driver);
      };
    }
  }
}
