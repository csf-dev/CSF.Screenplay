using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  public abstract class ServiceRegistrationProvider : IServiceRegistrationProvider
  {
    public virtual ServiceRegistry GetServiceRegistry()
    {
      var builder = new ServiceRegistryBuilder();
      RegisterServices(builder);
      return builder.BuildRegistry();
    }

    protected abstract void RegisterServices(IServiceRegistryBuilder builder);
  }
}
