using System;
using CSF.FlexDi;
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

      builder.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterFactory(() => new ConfigurationWebDriverFactoryProvider());

        b.RegisterFactory((Func<ConfigurationWebDriverFactoryProvider,IWebDriverFactory>) GetWebDriverFactory,
                          typeof(IWebDriverFactory))
         .WithName(name);
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
                                    Func<IResolvesServices,IWebDriver> factory,
                                    string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      helper.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterFactory(factory).AsOwnType().WithName(name);
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
    public static void UseSharedWebDriver(this IIntegrationConfigBuilder helper,
                                          Func<IWebDriver> initialiser,
                                          string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(initialiser == null)
        throw new ArgumentNullException(nameof(initialiser));

      helper.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterFactory(initialiser).AsOwnType().WithName(name);
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
                                         Func<IResolvesServices,IUriTransformer> factory,
                                              string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      helper.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterFactory(factory).AsOwnType().WithName(name);
      });
    }

    /// <summary>
    /// Registers a URI transformer into Screenplay. The single transformer instance will be shared/reused throughout
    /// all scenarios in the test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="transformer">Transformer.</param>
    /// <param name="name">Name.</param>
    public static void UseSharedUriTransformer(this IIntegrationConfigBuilder helper,
                                              IUriTransformer transformer,
                                              string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(transformer == null)
        throw new ArgumentNullException(nameof(transformer));

      helper.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterInstance(transformer).As<IUriTransformer>().WithName(name);
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
                                     Func<IResolvesServices,BrowseTheWeb> factory = null,
                                     string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));

      var fac = factory?? CreateWebBrowser;

      helper.ServiceRegistrations.PerScenario.Add(h => {
        h.RegisterFactory(fac).AsOwnType().WithName(name);
      });
    }

    static BrowseTheWeb CreateWebBrowser(IResolvesServices resolver)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      var driver = resolver.Resolve<IWebDriver>();

      IUriTransformer transformer;
      if(!resolver.TryResolve(out transformer))
        transformer = NoOpUriTransformer.Default;
      
      return new BrowseTheWeb(driver, transformer);
    }

    static IWebDriverFactory GetWebDriverFactory(ConfigurationWebDriverFactoryProvider provider)
    {
      if(provider == null)
        throw new ArgumentNullException(nameof(provider));
      
      return provider.GetFactory();
    }

    static Action<IScreenplayScenario> MarkWebDriverWithOutcome(string name)
    {
      return scenario => {
        var resolver = scenario.Resolver;
        IWebDriverFactory webDriverFactory;

        if(!scenario.Success.HasValue) return;
        if(!resolver.TryResolve(name, out webDriverFactory))
          return;

        var driver = resolver.Resolve<IWebDriver>(name);

        var success = scenario.Success.Value;
        if(success)
          webDriverFactory.MarkTestAsPassed(driver);
        else
          webDriverFactory.MarkTestAsFailed(driver);
      };
    }
  }
}
