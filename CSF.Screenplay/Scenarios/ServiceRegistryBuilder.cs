using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  public class ServiceRegistryBuilder : IServiceRegistryBuilder
  {
    readonly ICollection<IServiceRegistration> registrations;

    public void RegisterSingleton<TService>(TService instance, string name = null) where TService : class
    {
      var meta = GetMetadata<TService>(name);
      var reg = new SingletonRegistration(meta, instance);
      registrations.Add(reg);
    }

    public void RegisterPerScenario<TService>(Func<IServiceResolver,TService> factory, string name = null) where TService : class
    {
      var meta = GetMetadata<TService>(name);
      var fac = GetNonGenericFactory(factory);
      var reg = new PerScenarioRegistration(meta, fac);
      registrations.Add(reg);
    }

    public ServiceRegistry BuildRegistry()
    {
      var builtRegistrations = new ReadOnlyCollection<IServiceRegistration>(registrations.ToList());
      return new ServiceRegistry(builtRegistrations);
    }

    ServiceMetadata GetMetadata<TService>(string name)
    {
      return new ServiceMetadata(typeof(TService), name);
    }

    Func<IServiceResolver,object> GetNonGenericFactory<TService>(Func<IServiceResolver,TService> genericFactory)
    {
      return scenario => genericFactory(scenario);
    }

    public ServiceRegistryBuilder()
    {
      registrations = new List<IServiceRegistration>();
    }
  }
}
