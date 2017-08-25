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

    readonly ScreenplayEnvironment environment;
    readonly IIntegrationConfigBuilder builder;
    readonly IIntegrationConfig config;
    bool loaded;

    #endregion

    #region public API

    /// <summary>
    /// Loads the integration customisations and configurations.
    /// </summary>
    public void LoadIntegration()
    {
      if(loaded)
        return;

      CustomiseIntegration(builder);
      environment.ConfigureServiceRegistryIfRequired(GetServiceRegistry);
      loaded = true;
    }

    /// <summary>
    /// Executed once, before the first scenario in the test run is executed.  Note that
    /// all services must have already been registered prior to executing this method.
    /// </summary>
    public void BeforeExecutingFirstScenario()
    {
      BeforeExecutingFirstScenario(environment, CreateSingletonResolver());
      NotifySubscribersOfTestRunStart();
    }

    void BeforeExecutingFirstScenario(IProvidesTestRunEvents testRunEvents,
                                      IServiceResolver serviceResolver)
    {
      foreach(var callback in builder.BeforeFirstScenario)
        callback(testRunEvents, serviceResolver);
    }

    /// <summary>
    /// Executed before each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    public void BeforeScenario(ScreenplayScenario scenario)
    {
      CustomiseScenario(scenario);
      MarkAsBegun(scenario);
    }

    /// <summary>
    /// Executed after each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    public void AfterScenario(ScreenplayScenario scenario, bool success)
    {
      MarkAsEnded(scenario, success);
      AfterScenario(scenario);
    }

    /// <summary>
    /// Executed after the last scenario in a test run.
    /// </summary>
    public void AfterExecutedLastScenario()
    {
      NotifySubscribersOfTestRunCompletion();
      AfterExecutedLastScenario(CreateSingletonResolver());
    }

    /// <summary>
    /// Gets the scenario factory.
    /// </summary>
    /// <returns>The scenario factory.</returns>
    public IScenarioFactory GetScenarioFactory() => environment.GetScenarioFactory();

    #endregion

    #region methods

    void AfterExecutedLastScenario(IServiceResolver serviceResolver)
    {
      foreach(var callback in builder.AfterLastScenario)
        callback(serviceResolver);
    }

    IServiceRegistryFactory GetRegistrationProvider()
    => new ServiceRegistryFactory(builder);

    void CustomiseScenario(ScreenplayScenario scenario)
    {
      foreach(var callback in builder.BeforeScenario)
        callback(scenario);
    }

    void AfterScenario(ScreenplayScenario scenario)
    {
      foreach(var callback in builder.AfterScenario)
        callback(scenario);
    }

    /// <summary>
    /// Marks the scenario instance as having started (informing subscribers where applicable).
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    void MarkAsBegun(ScreenplayScenario scenario) => scenario.Begin();

    /// <summary>
    /// Marks the scenario instance as having ended (informing subscribers where applicable).
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    void MarkAsEnded(ScreenplayScenario scenario, bool success) => scenario.End(success);

    /// <summary>
    /// Creates a service resolver which resolves only singleton service instances.
    /// </summary>
    /// <returns>The singleton resolver.</returns>
    IServiceResolver CreateSingletonResolver() => environment.CreateSingletonResolver();

    /// <summary>
    /// Notifies the subscribers of test run start.
    /// </summary>
    void NotifySubscribersOfTestRunStart() => environment.NotifyBeginTestRun();

    /// <summary>
    /// Notifies the subscribers of test run completion.
    /// </summary>
    void NotifySubscribersOfTestRunCompletion() => environment.NotifyCompleteTestRun();

    void CustomiseIntegration(IIntegrationConfigBuilder configBuilder)
    {
      if(configBuilder == null)
        throw new ArgumentNullException(nameof(configBuilder));

      config.Configure(configBuilder);
    }

    IServiceRegistry GetServiceRegistry()
    {
      var provider = GetRegistrationProvider();
      return provider.GetServiceRegistry();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenplayIntegration"/> class.
    /// </summary>
    public ScreenplayIntegration(IIntegrationConfig config)
    {
      if(config == null)
        throw new ArgumentNullException(nameof(config));

      this.config = config;
      builder = new IntegrationConfigurationBuilder();
      environment = new ScreenplayEnvironment();
    }

    #endregion

    #region static methods

    /// <summary>
    /// Static factory method which creates a new implementation of <see cref="IScreenplayIntegration"/>
    /// from a given configuration type.
    /// </summary>
    /// <param name="configType">Config type.</param>
    public static IScreenplayIntegration Create(Type configType)
    {
      var config = GetConfig(configType);
      return new ScreenplayIntegration(config);
    }

    static IIntegrationConfig GetConfig(Type configType)
    {
      if(configType == null)
        throw new ArgumentNullException(nameof(configType));

      if(!typeof(IIntegrationConfig).IsAssignableFrom(configType))
      {
        throw new ArgumentException($"Configuration type must implement `{typeof(IIntegrationConfig).Name}'.",
                                    nameof(configType));
      }

      return (IIntegrationConfig) Activator.CreateInstance(configType);
    }

    #endregion
  }
}
