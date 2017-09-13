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
    /// Gets the services managed by the current resolver instance.
    /// </summary>
    /// <value>The services.</value>
    protected IDictionary<ServiceMetadata,Lazy<object>> Services => services;

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

    /// <summary>
    /// Releases all of the services which registered per-scenario.
    /// </summary>
    public void ReleasePerScenarioServices()
    {
      var perScenarioServices = services
        .Where(x => x.Key.Lifetime == ServiceLifetime.PerScenario)
        .Select(x => x.Value)
        .ToArray();

      DisposeAllInitialised(perScenarioServices);
    }

    /// <summary>
    /// Releases all of the lazily-initialised services are which registered per-scenario.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This will release all singleton services which were registered lazily with the service resolver.
    /// That is - where the resolver was itself responsible for instantiating/initialising the service.
    /// </para>
    /// <para>
    /// Services which were initialised outside of the resolver and were passed simply as an already-constructed
    /// and initialised object reference are NOT released/disposed by calling this method.  Other code must
    /// be responsible for releasing/disposing these.
    /// </para>
    /// <para>
    /// In short, this method only cleans up objects which the resolver created itself.
    /// </para>
    /// </remarks>
    public void ReleaseLazySingletonServices()
    {
      var lazySingletonServices = services
        .Where(x => x.Key.Lifetime == ServiceLifetime.Singleton && x.Key.IsResolverOwned)
        .Select(x => x.Value)
        .ToArray();

      DisposeAllInitialised(lazySingletonServices);
    }

    void DisposeAllInitialised(IEnumerable<Lazy<object>> toDispose)
    {
      foreach(var service in toDispose)
      {
        if(service.IsValueCreated && service.Value is IDisposable)
        {
          ((IDisposable) service.Value).Dispose();
        }
      }
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
