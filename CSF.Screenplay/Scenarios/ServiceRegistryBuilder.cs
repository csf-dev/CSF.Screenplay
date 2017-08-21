using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Concrete implementation of <see cref="IServiceRegistryBuilder"/>.
  /// </summary>
  public class ServiceRegistryBuilder : IServiceRegistryBuilder
  {
    readonly ICollection<IServiceRegistration> registrations;

    /// <summary>
    /// Registers a service which will be used across all scenarios within a test run.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The 1st type parameter.</typeparam>
    public void RegisterSingleton<TService>(TService instance, string name = null) where TService : class
    {
      var meta = GetMetadata<TService>(name, ServiceLifetime.Singleton);
      var reg = new SingletonRegistration(meta, instance);
      registrations.Add(reg);
    }

    /// <summary>
    /// Registers a service which will be used across all scenarios within a test run.
    /// </summary>
    /// <param name="initialiser">Initialiser.</param>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The 1st type parameter.</typeparam>
    public void RegisterSingleton<TService>(Func<TService> initialiser, string name = null) where TService : class
    {
      var meta = GetMetadata<TService>(name, ServiceLifetime.Singleton);
      var fac = GetNonGenericFactory(initialiser);
      var reg = new LazySingletonRegistration(meta, fac);
      registrations.Add(reg);
    }

    /// <summary>
    /// Registers a service which will be constructed afresh (using the given factory function) for
    /// each scenario within a test run.
    /// </summary>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The 1st type parameter.</typeparam>
    public void RegisterPerScenario<TService>(Func<IServiceResolver,TService> factory, string name = null) where TService : class
    {
      var meta = GetMetadata<TService>(name, ServiceLifetime.PerScenario);
      var fac = GetNonGenericFactory(factory);
      var reg = new PerScenarioRegistration(meta, fac);
      registrations.Add(reg);
    }

    /// <summary>
    /// Builds and returns a service registry.
    /// </summary>
    /// <returns>The registry.</returns>
    public ServiceRegistry BuildRegistry()
    {
      var builtRegistrations = new ReadOnlyCollection<IServiceRegistration>(registrations.ToList());
      return new ServiceRegistry(builtRegistrations);
    }

    ServiceMetadata GetMetadata<TService>(string name, ServiceLifetime lifetime)
    {
      return new ServiceMetadata(typeof(TService), name, lifetime);
    }

    Func<IServiceResolver,object> GetNonGenericFactory<TService>(Func<IServiceResolver,TService> genericFactory)
    {
      return scenario => genericFactory(scenario);
    }

    Func<object> GetNonGenericFactory<TService>(Func<TService> genericFactory)
    {
      return () => genericFactory();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ServiceRegistryBuilder"/> class.
    /// </summary>
    public ServiceRegistryBuilder()
    {
      registrations = new List<IServiceRegistration>();
    }
  }
}
