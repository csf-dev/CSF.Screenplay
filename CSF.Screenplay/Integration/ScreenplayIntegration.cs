using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  public abstract class ScreenplayIntegration : IScreenplayIntegration
  {
    static ScreenplayEnvironment environment;

    public void EnsureServicesAreRegistered()
    {
      environment.ConfigureServiceRegistryIfRequired(GetServiceRegistry);
    }

    public void BeforeExecutingFirstScenario()
    {
      BeforeExecutingFirstScenario(environment, CreateSingletonResolver());
      NotifySubscribersOfTestRunStart();
    }

    protected virtual void BeforeExecutingFirstScenario(IProvidesTestRunEvents testRunEvents,
                                                        IServiceResolver serviceResolver)
    {
      // Intentional no-op for subclasses to implement
    }

    public void BeforeScenario(ScreenplayScenario scenario)
    {
      CustomiseScenario(scenario);
      MarkAsBegun(scenario);
    }

    public void AfterScenario(ScreenplayScenario scenario, bool success)
    {
      AfterScenario(scenario);
      MarkAsEnded(scenario, success);
    }

    public void AfterExecutedLastScenario()
    {
      NotifySubscribersOfTestRunCompletion();
      AfterExecutedLastScenario(CreateSingletonResolver());
    }

    protected virtual void AfterExecutedLastScenario(IServiceResolver serviceResolver)
    {
      // Intentional no-op for subclasses to implement
    }

    protected abstract IServiceRegistrationProvider GetRegistrationProvider();

    protected virtual void CustomiseScenario(ScreenplayScenario scenario)
    {
      // Intentional no-op for subclasses to implement
    }

    protected virtual void AfterScenario(ScreenplayScenario scenario)
    {
      // Intentional no-op for subclasses to implement
    }

    public IScenarioFactory GetScenarioFactory() => environment.GetScenarioFactory();

    protected void MarkAsBegun(ScreenplayScenario scenario) => scenario.Begin();

    protected void MarkAsEnded(ScreenplayScenario scenario, bool success) => scenario.End(success);

    protected IServiceResolver CreateSingletonResolver() => environment.CreateSingletonResolver();

    protected void NotifySubscribersOfTestRunStart() => environment.NotifyBeginTestRun();

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
