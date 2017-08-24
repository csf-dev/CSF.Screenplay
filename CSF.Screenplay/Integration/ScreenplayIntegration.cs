using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Base type for custom screenplay integrations.  This is suitable for subclassing in custom integrations.
  /// </summary>
  public abstract class ScreenplayIntegration : IScreenplayIntegration
  {
    static ScreenplayEnvironment environment;
    readonly IScreenplayIntegrationHelper integrationHelper;
    bool loaded;

    /// <summary>
    /// Loads the integration customisations and configurations.
    /// </summary>
    public void LoadIntegration()
    {
      if(loaded)
        return;

      CustomiseIntegration(integrationHelper);
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
      foreach(var callback in integrationHelper.BeforeFirstScenario)
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

    void AfterExecutedLastScenario(IServiceResolver serviceResolver)
    {
      foreach(var callback in integrationHelper.AfterLastScenario)
        callback(serviceResolver);
    }

    IServiceRegistrationProvider GetRegistrationProvider()
      => new ServiceRegistrationProvider(integrationHelper);

    void CustomiseScenario(ScreenplayScenario scenario)
    {
      foreach(var callback in integrationHelper.BeforeScenario)
        callback(scenario);
    }

    void AfterScenario(ScreenplayScenario scenario)
    {
      foreach(var callback in integrationHelper.AfterScenario)
        callback(scenario);
    }

    /// <summary>
    /// Gets the scenario factory.
    /// </summary>
    /// <returns>The scenario factory.</returns>
    public IScenarioFactory GetScenarioFactory() => environment.GetScenarioFactory();

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

    /// <summary>
    /// Customises this Screenplay integration.  Override this method to register
    /// and configure your screenplay-related services.
    /// </summary>
    /// <param name="integrationHelper">Screenplay integration customisation helper.</param>
    protected abstract void CustomiseIntegration(IScreenplayIntegrationHelper integrationHelper);

    ServiceRegistry GetServiceRegistry()
    {
      var provider = GetRegistrationProvider();
      return provider.GetServiceRegistry();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Integration.ScreenplayIntegration"/> class.
    /// </summary>
    public ScreenplayIntegration()
    {
      integrationHelper = new IntegrationHelper();
    }

    static ScreenplayIntegration()
    {
      environment = new ScreenplayEnvironment();
    }
  }
}
