using System;
namespace CSF.Screenplay.Scenarios
{
  public interface IServiceRegistryBuilder
  {
    void RegisterSingleton<TService>(TService instance, string name = null) where TService : class;

    void RegisterPerScenario<TService>(Func<IServiceResolver,TService> factory, string name = null) where TService : class;
  }
}
