using System;
using System.Collections.Generic;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  class ScreenplayEnvironment : IProvidesTestRunEvents
  {
    object syncRoot;
    IServiceRegistry serviceRegistry;

    ISingletonServiceResolverFactory SingletonResolverFactory => serviceRegistry;

    IReadOnlyCollection<IServiceRegistration> GetRegistrations()
      => serviceRegistry.Registrations;

    internal IServiceResolver CreateSingletonResolver()
    {
      if(SingletonResolverFactory == null)
        throw new InvalidOperationException("Cannot create a resolver instance until the registrations have been configured.");

      return SingletonResolverFactory.GetResolver();
    }

    internal void ConfigureServiceRegistryIfRequired(Func<IServiceRegistry> factory)
    {
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));
      
      lock(syncRoot)
      {
        if(serviceRegistry == null)
        {
          var registry = factory();
          if(registry == null)
            throw new ArgumentException("The factory parameter must not produce a null instance.", nameof(factory));
          serviceRegistry = registry;
        }
      }
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
      return new ScenarioFactory(GetRegistrations());
    }

    internal ScreenplayEnvironment()
    {
      syncRoot = new object();
    }
  }
}
