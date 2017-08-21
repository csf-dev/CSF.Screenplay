using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Concrete implementation of <see cref="IServiceResolver"/>.
  /// </summary>
  public class ServiceResolver : IServiceResolver
  {
    readonly IDictionary<ServiceMetadata,Lazy<object>> services;

    /// <summary>
    /// Gets a service of the indicated type and name.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The desired service type.</typeparam>
    public TService GetService<TService>(string name) where TService : class
    {
      var metadata = new ServiceMetadata(typeof(TService), name, ServiceLifetime.Any);
      return (TService) GetService(metadata);
    }

    /// <summary>
    /// Gets a service of the indicated type and name, returning a <c>null</c> reference if no service is found.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The desired service type.</typeparam>
    public TService GetOptionalService<TService>(string name) where TService : class
    {
      var metadata = new ServiceMetadata(typeof(TService), name, ServiceLifetime.Any);
      return (TService) GetOptionalService(metadata);
    }

    /// <summary>
    /// Gets a service which matches the given metadata.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="metadata">Metadata.</param>
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

    /// <summary>
    /// Gets a service which matches the given metadata, returning a <c>null</c> reference if no service is found.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="metadata">Metadata.</param>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ServiceResolver"/> class.
    /// </summary>
    /// <param name="registrations">Registrations.</param>
    public ServiceResolver(IReadOnlyCollection<IServiceRegistration> registrations)
    {
      if(registrations == null)
        throw new ArgumentNullException(nameof(registrations));

      services = registrations.ToDictionary(k => k.Metadata, v => v.GetService(this));
    }
  }
}
