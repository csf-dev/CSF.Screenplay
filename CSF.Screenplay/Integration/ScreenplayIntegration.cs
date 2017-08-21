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

    /// <summary>
    /// Ensures that all of the screenplay services are registered.
    /// This method is safe to be called many times, however it will only perform the actual registration once.
    /// </summary>
    public void EnsureServicesAreRegistered()
    {
      environment.ConfigureServiceRegistryIfRequired(GetServiceRegistry);
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

    /// <summary>
    /// Hook for custom integrations to provide their own logic, which will be executed before the first
    /// scenario in a test run.
    /// </summary>
    /// <param name="testRunEvents">Test run events.</param>
    /// <param name="serviceResolver">Service resolver.</param>
    protected virtual void BeforeExecutingFirstScenario(IProvidesTestRunEvents testRunEvents,
                                                        IServiceResolver serviceResolver)
    {
      // Intentional no-op for subclasses to implement
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
      AfterScenario(scenario);
      MarkAsEnded(scenario, success);
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
    /// Hook for custom integrations to provide their own logic, which will be executed after the last
    /// scenario in a test run.
    /// </summary>
    /// <param name="serviceResolver">Service resolver.</param>
    protected virtual void AfterExecutedLastScenario(IServiceResolver serviceResolver)
    {
      // Intentional no-op for subclasses to implement
    }

    /// <summary>
    /// Gets a registration provider instance, which is responsible for registring all of the services which
    /// are required by Screenplay tests.
    /// </summary>
    /// <returns>The registration provider.</returns>
    protected abstract IServiceRegistrationProvider GetRegistrationProvider();

    /// <summary>
    /// Hook for custom implementations to customise a Scenario instance after it is created but before
    /// it is used for the test scenario.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    protected virtual void CustomiseScenario(ScreenplayScenario scenario)
    {
      // Intentional no-op for subclasses to implement
    }

    /// <summary>
    /// Hook for custom implementations to execute cleanup logic upon the Scenario instance after it has been
    /// used for a test scenario.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    protected virtual void AfterScenario(ScreenplayScenario scenario)
    {
      // Intentional no-op for subclasses to implement
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
    protected void MarkAsBegun(ScreenplayScenario scenario) => scenario.Begin();

    /// <summary>
    /// Marks the scenario instance as having ended (informing subscribers where applicable).
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    protected void MarkAsEnded(ScreenplayScenario scenario, bool success) => scenario.End(success);

    /// <summary>
    /// Creates a service resolver which resolves only singleton service instances.
    /// </summary>
    /// <returns>The singleton resolver.</returns>
    protected IServiceResolver CreateSingletonResolver() => environment.CreateSingletonResolver();

    /// <summary>
    /// Notifies the subscribers of test run start.
    /// </summary>
    protected void NotifySubscribersOfTestRunStart() => environment.NotifyBeginTestRun();

    /// <summary>
    /// Notifies the subscribers of test run completion.
    /// </summary>
    protected void NotifySubscribersOfTestRunCompletion() => environment.NotifyCompleteTestRun();

    ServiceRegistry GetServiceRegistry()
    {
      var provider = GetRegistrationProvider();
      return provider.GetServiceRegistry();
    }

    static ScreenplayIntegration()
    {
      environment = new ScreenplayEnvironment();
    }
  }
}
