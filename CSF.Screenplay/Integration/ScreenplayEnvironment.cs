using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  class ScreenplayEnvironment : IProvidesTestRunEvents
  {
    internal ServiceRegistry ResolverFactory { get; set; }

    internal IServiceResolver CreateResolver()
    {
      if(ResolverFactory == null)
        throw new InvalidOperationException("Cannot create a resolver instance until the registrations have been provided.");

      return ((IServiceResolverFactory) ResolverFactory).GetResolver();
    }

    internal IServiceResolver CreateSingletonResolver()
    {
      if(ResolverFactory == null)
        throw new InvalidOperationException("Cannot create a resolver instance until the registrations have been provided.");

      return ((ISingletonServiceResolverFactory) ResolverFactory).GetResolver();
    }

    public event EventHandler BeginTestRun;

    public event EventHandler CompleteTestRun;

    internal void NotifyBeginTestRun()
    {
      BeginTestRun?.Invoke(this, EventArgs.Empty);
    }

    internal void NotifyCompleteTestRun()
    {
      CompleteTestRun?.Invoke(this, EventArgs.Empty);
    }

    internal IScenarioFactory GetScenarioFactory()
    {
      var resolver = CreateResolver();
      return new ScenarioFactory(resolver);
    }
  }
}
