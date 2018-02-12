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
        b.RegisterDynamicFactory(resolver => {
          var factory = resolver.Resolve<ICreatesWebDriver>(name);
          var scenario = resolver.Resolve<IScenarioName>();
          var flagsProvider = resolver.TryResolve<IGetsBrowserFlags>();

          return factory.CreateWebDriver(flagsProvider: flagsProvider,
                                         scenarioName: GetHumanReadableName(scenario));
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
