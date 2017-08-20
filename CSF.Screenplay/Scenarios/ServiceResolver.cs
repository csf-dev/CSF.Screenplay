using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  public class ServiceResolver : IServiceResolver
  {
    readonly IDictionary<ServiceMetadata,Lazy<object>> services;

    public TService GetService<TService>(string name) where TService : class
    {
      var metadata = new ServiceMetadata(typeof(TService), name);
      return (TService) GetService(metadata);
    }

    public TService GetOptionalService<TService>(string name) where TService : class
    {
      var metadata = new ServiceMetadata(typeof(TService), name);
      return (TService) GetOptionalService(metadata);
    }

    public object GetService(ServiceMetadata metadata)
    {
      var service = GetOptionalService(metadata);
      if(ReferenceEquals(service, null))
      {
        var message = GetNotRegisteredExceptionMessage(metadata);
        throw new ServiceNotRegisteredException(message);
      }

      return service;
    }

    public object GetOptionalService(ServiceMetadata metadata)
    {
      if(!services.ContainsKey(metadata))
        return null;

      return services[metadata].Value;
    }

    string GetNotRegisteredExceptionMessage(ServiceMetadata registration)
    {
      var message = $@"Failed to resolve a Screenplay service; the service is not registered.
Use {nameof(GetOptionalService)} if the service is expected to be null.
Service type:{registration.Type.FullName}";

      if(registration.Name != null)
      {
        message += $@"
Service name:{registration.Name}";
      }

      return message;
    }

    public ServiceResolver(IReadOnlyCollection<IServiceRegistration> registrations)
    {
      if(registrations == null)
        throw new ArgumentNullException(nameof(registrations));

      services = registrations.ToDictionary(k => k.Metadata, v => v.GetService(this));
    }
  }
}
