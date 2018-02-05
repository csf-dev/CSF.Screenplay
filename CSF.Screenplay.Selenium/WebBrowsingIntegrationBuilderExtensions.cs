using System;
using CSF.FlexDi;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Abilities;
using CSF.WebDriverExtras;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to the registration of web-browsing-related services into Screenplay.
  /// </summary>
  public static class WebBrowsingIntegrationBuilderExtensions
  {
    /// <summary>
    /// Registers a <c>ICreatesWebDriver</c> for the creation of web drivers, using WebDriverExtras
    /// configuration-based factory system.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="name">Name.</param>
    public static void UseWebDriverFromConfiguration(this IIntegrationConfigBuilder builder,
                                                     string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      builder.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterFactory(() => GetWebDriverFactory.FromConfiguration())
         .AsOwnType()
         .WithName(name);
      });

      builder.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterFactory((ICreatesWebDriver factory, IScenario scenario) => {
          return factory.CreateWebDriver(scenarioName: GetHumanReadableName(scenario));
        });
      });

      builder.AfterScenario.Add(MarkWebDriverWithOutcome(name));
    }

    /// <summary>
    /// Registers a Selenium IWebDriver into Screenplay, making use of a given factory.  This Web Driver
    /// will be created afresh and disposed with each scenario.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseWebDriver(this IIntegrationConfigBuilder builder,
                                    Func<IResolvesServices,IWebDriver> factory,
                                    string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      builder.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterDynamicFactory(factory).AsOwnType().WithName(name);
      });

      builder.AfterScenario.Add(MarkWebDriverWithOutcome(name));
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
        b.RegisterDynamicFactory(factory).AsOwnType().WithName(name);
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
      var transformer = resolver.TryResolve<IUriTransformer>() ?? NoOpUriTransformer.Default;

      return new BrowseTheWeb(driver, transformer);
    }

    static string GetHumanReadableName(IScenario scenario) => $"{scenario.FeatureId.Name}: {scenario.ScenarioId.Name}";

    static Action<IScenario> MarkWebDriverWithOutcome(string name)
    {
      return scenario => {
        if(!scenario.Success.HasValue) return;
        var success = scenario.Success.Value;

        var webDriver = scenario.DiContainer.TryResolve<IWebDriver>(name);
        if(webDriver == null) return;

        var receivesStatus = webDriver as ICanReceiveScenarioStatus;
        if(receivesStatus == null) return;

        if(success)
          receivesStatus.MarkScenarioAsSuccess();
        else
          receivesStatus.MarkScenarioAsFailure();
      };
    }
  }
}
