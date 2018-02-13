using System;
using CSF.FlexDi;
using CSF.Screenplay.Integration;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Selenium.Abilities;
using CSF.WebDriverExtras;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to the registration of Selenium web drivers for Screenplay.
  /// </summary>
  public static class WebDriverIntegrationBuilderExtensions
  {
    /// <summary>
    /// Registers an <c>ICreatesWebDriver</c> for the creation of web drivers, using WebDriverExtras
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
        b.RegisterDynamicFactory(GetWebDriverResolver(name))
         .AsOwnType()
         .WithName(name);
      });

      builder.AfterScenario.Add(MarkWebDriverWithOutcome(name));
    }

    /// <summary>
    /// Registers an <c>ICreatesWebDriver</c> for the creation of web drivers which resolves the web driver from a
    /// factory for each scenario.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseWebDriver(this IIntegrationConfigBuilder builder,
                                    Func<IResolvesServices,ICreatesWebDriver> factory,
                                    string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      builder.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterDynamicFactory(factory).AsOwnType().WithName(name);

        b.RegisterDynamicFactory(GetWebDriverResolver(name))
         .AsOwnType()
         .WithName(name);
      });

      builder.AfterScenario.Add(MarkWebDriverWithOutcome(name));
    }

    /// <summary>
    /// Registers an <c>ICreatesWebDriver</c> for the creation of web drivers which resolves the web driver from a
    /// factory for each scenario.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseWebDriver(this IIntegrationConfigBuilder builder,
                                    ICreatesWebDriver factory,
                                    string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      builder.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterInstance(factory).AsOwnType().WithName(name);
      });

      builder.ServiceRegistrations.PerScenario.Add(b => {
        b.RegisterDynamicFactory(GetWebDriverResolver(name))
         .AsOwnType()
         .WithName(name);
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
        b.RegisterDynamicFactory(factory)
         .AsOwnType()
         .WithName(name);
      });

      builder.AfterScenario.Add(MarkWebDriverWithOutcome(name));
    }

    /// <summary>
    /// Registers a Selenium IWebDriver into Screenplay, making use of a given initialiser.  This single Web Driver
    /// created will be used/shared throughout the entire test run.
    /// </summary>
    /// <param name="helper">Helper.</param>
    /// <param name="lazyWebDriver">Initialiser.</param>
    /// <param name="name">Name.</param>
    public static void UseSharedWebDriver(this IIntegrationConfigBuilder helper,
                                          Lazy<IWebDriver> lazyWebDriver,
                                          string name = null)
    {
      if(helper == null)
        throw new ArgumentNullException(nameof(helper));
      if(lazyWebDriver == null)
        throw new ArgumentNullException(nameof(lazyWebDriver));

      helper.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterDynamicFactory(r => lazyWebDriver.Value).AsOwnType().WithName(name);
      });
    }

    /// <summary>
    /// Registers an <c>ICreatesWebDriver</c> which will create a single web driver, shared between all test scenarios.
    /// </summary>
    /// <param name="builder">Builder.</param>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    public static void UseSharedWebDriver(this IIntegrationConfigBuilder builder,
                                          ICreatesWebDriver factory,
                                          string name = null)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      builder.ServiceRegistrations.PerTestRun.Add(b => {
        b.RegisterInstance(factory).AsOwnType().WithName(name);

        b.RegisterDynamicFactory(resolver => {
           var fac = resolver.Resolve<ICreatesWebDriver>(name);
           var flagsProvider = resolver.TryResolve<IGetsBrowserFlags>();

           return factory.CreateWebDriver(flagsProvider: flagsProvider);
         })
         .AsOwnType()
         .WithName(name);
      });
    }

    static Func<IResolvesServices,IWebDriver> GetWebDriverResolver(string name)
    {
      return resolver => {
        var factory = resolver.Resolve<ICreatesWebDriver>(name);
        var scenario = resolver.Resolve<IScenarioName>();
        var flagsProvider = resolver.TryResolve<IGetsBrowserFlags>();

        return factory.CreateWebDriver(flagsProvider: flagsProvider,
                                       scenarioName: GetHumanReadableName(scenario));
      };
    }

    static string GetHumanReadableName(IScenarioName scenario) => $"{scenario.FeatureId.Name}: {scenario.ScenarioId.Name}";

    static Action<IScenario> MarkWebDriverWithOutcome(string name)
    {
      return scenario => {
        if(!scenario.Success.HasValue) return;
        var success = scenario.Success.Value;

        var webDriver = scenario.DiContainer.TryResolve<IWebDriver>(name);
        if(webDriver == null) return;

        var receivesStatus = webDriver as ICanReceiveScenarioOutcome;
        if(receivesStatus == null) return;

        receivesStatus.MarkScenarioWithOutcome(success);
      };
    }
  }
}
