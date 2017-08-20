using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  public abstract class ScreenplayIntegration
  {
    static ScreenplayEnvironment environment;

    public virtual void BeforeBuildingFirstScenario()
    {
      RegisterServices();
    }

    public virtual void BeforeExecutingFirstScenario()
    {
      BeforeExecutingFirstScenario(environment, CreateSingletonResolver());
      NotifySubscribersOfTestRunStart();
    }

    protected virtual void BeforeExecutingFirstScenario(IProvidesTestRunEvents testRunEvents,
                                                        IServiceResolver serviceResolver)
    {
      // Intentional no-op for subclasses to implement
    }

    public virtual void BeforeScenario(ScreenplayScenario scenario)
    {
      CustomiseScenario(scenario);
      MarkAsBegun(scenario);
    }

    public virtual void AfterScenario(ScreenplayScenario scenario, bool success)
    {
      MarkAsEnded(scenario, success);
    }

    public virtual void AfterExecutedLastScenario()
    {
      NotifySubscribersOfTestRunCompletion();
      AfterExecutedLastScenario(CreateSingletonResolver());
    }

    protected virtual void AfterExecutedLastScenario(IServiceResolver serviceResolver)
    {
      // Intentional no-op for subclasses to implement
    }

    protected abstract IServiceRegistrationProvider GetRegistrationProvider();

    protected virtual void CustomiseScenario(IScreenplayScenario scenario)
    {
      // Intentional no-op for subclasses to implement
    }

    public IScenarioFactory GetScenarioFactory() => environment.GetScenarioFactory();

    protected void MarkAsBegun(ScreenplayScenario scenario) => scenario.Begin();

    protected void MarkAsEnded(ScreenplayScenario scenario, bool success) => scenario.End(success);

    protected IServiceResolver CreateSingletonResolver() => environment.CreateSingletonResolver();

    protected void NotifySubscribersOfTestRunStart() => environment.NotifyBeginTestRun();

    protected void NotifySubscribersOfTestRunCompletion() => environment.NotifyCompleteTestRun();

    protected void RegisterServices()
    {
      var provider = GetRegistrationProvider();
      var registry = provider.GetServiceRegistry();
      RegisterServices(registry);
    }

    void RegisterServices(ServiceRegistry registry)
    {
      if(registry == null)
        throw new ArgumentNullException(nameof(registry));

      environment.ResolverFactory = registry;
    }

    public ScreenplayIntegration()
    {
      environment = new ScreenplayEnvironment();
    }
  }
}
