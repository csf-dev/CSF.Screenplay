using System;
using BoDi;
using CSF.Screenplay.Integration;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Binding type for the SpecFlow/Screenplay integration.
  /// </summary>
  [Binding]
  public class ScreenplayBinding
  {
    static Lazy<IScreenplayIntegration> integration;
    static IScreenplayIntegration Integration => integration.Value;

    readonly IObjectContainer container;

    /// <summary>
    /// Executed before each scenario.
    /// </summary>
    [Before]
    public void BeforeScenario()
    {
      var scenarioContext = container.Resolve<ScenarioContext>();
      var featureContext = container.Resolve<FeatureContext>();

      var adapter = new ScenarioAdapter(scenarioContext, featureContext, Integration);
      var scenario = adapter.CreateScenario();
      container.RegisterInstanceAs(scenario);

      Integration.BeforeScenario(scenario);
    }

    /// <summary>
    /// Executed after each scenario.
    /// </summary>
    [After]
    public void AfterScenario()
    {
      var scenario = container.Resolve<IScreenplayScenario>();
      var success = ScenarioAdapter.GetScenarioSuccess(container.Resolve<ScenarioContext>());
      Integration.AfterScenario(scenario, success);
    }

    /// <summary>
    /// Executed before a test run.
    /// </summary>
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
      Integration.BeforeExecutingFirstScenario();
    }

    /// <summary>
    /// Executed after a test run.
    /// </summary>
    [AfterTestRun]
    public static void AfterTestRun()
    {
      Integration.AfterExecutedLastScenario();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.SpecFlow.ScreenplayBinding"/> class.
    /// </summary>
    /// <param name="container">Container.</param>
    public ScreenplayBinding(IObjectContainer container)
    {
      this.container = container;
    }

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.SpecFlow.ScreenplayBinding"/> class.
    /// </summary>
    static ScreenplayBinding()
    {
      var provider = new IntegrationProvider();
      integration = provider.GetIntegration();
    }
  }
}
