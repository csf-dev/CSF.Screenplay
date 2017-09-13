using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Base type for custom screenplay integrations.  This is suitable for subclassing in custom integrations.
  /// </summary>
  public class ScreenplayIntegration : IScreenplayIntegration
  {
    #region fields

    readonly IIntegrationConfigBuilder builder;
    readonly TestRunEvents testRunEvents;
    readonly Lazy<IServiceRegistry> serviceRegistry;

    #endregion

    #region properties

    IServiceRegistry ServiceRegistry => serviceRegistry.Value;

    #endregion

    #region public API

    /// <summary>
    /// Executed once, before the first scenario in the test run is executed.  Note that
    /// all services must have already been registered prior to executing this method.
    /// </summary>
    public void BeforeExecutingFirstScenario()
    {
      var resolver = ServiceRegistry.GetSingletonResolver();

      foreach(var callback in builder.BeforeFirstScenario)
        callback(testRunEvents, resolver);
      
      testRunEvents.NotifyBeginTestRun();
    }

    /// <summary>
    /// Executed before each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    public void BeforeScenario(IScreenplayScenario scenario)
    {
      foreach(var callback in builder.BeforeScenario)
        callback(scenario);

      if(scenario is ICanBeginAndEndScenario)
        ((ICanBeginAndEndScenario) scenario).Begin();
    }

    /// <summary>
    /// Executed after each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    public void AfterScenario(IScreenplayScenario scenario, bool success)
    {
      if(scenario is ICanBeginAndEndScenario)
        ((ICanBeginAndEndScenario) scenario).End(success);

      foreach(var callback in builder.AfterScenario)
        callback(scenario);

      scenario.ReleasePerScenarioServices();
    }

    /// <summary>
    /// Executed after the last scenario in a test run.
    /// </summary>
    public void AfterExecutedLastScenario()
    {
      testRunEvents.NotifyCompleteTestRun();

      var resolver = ServiceRegistry.GetSingletonResolver();

      foreach(var callback in builder.AfterLastScenario)
        callback(resolver);

      ServiceRegistry.GetSingletonResolver().ReleaseLazySingletonServices();
    }

    /// <summary>
    /// Gets the scenario factory.
    /// </summary>
    /// <returns>The scenario factory.</returns>
    public IScenarioFactory GetScenarioFactory()
      => new ScenarioFactory(ServiceRegistry.Registrations);

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Integration.ScreenplayIntegration"/> class.
    /// </summary>
    /// <param name="configBuilder">Config builder.</param>
    /// <param name="registry">Service registry.</param>
    public ScreenplayIntegration(IIntegrationConfigBuilder configBuilder, Lazy<IServiceRegistry> registry)
    {
      if(configBuilder == null)
        throw new ArgumentNullException(nameof(configBuilder));
      if(registry == null)
        throw new ArgumentNullException(nameof(registry));

      builder = configBuilder;
      serviceRegistry = registry;
      testRunEvents = new TestRunEvents();
    }

    #endregion
  }
}
