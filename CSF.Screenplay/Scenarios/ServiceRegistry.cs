using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Registry type which contains all of the Screenplay service registrations.
  /// </summary>
  public class ServiceRegistry : IServiceResolverFactory, ISingletonServiceResolverFactory
  {
    readonly IReadOnlyCollection<IServiceRegistration> registrations;

    IServiceResolver IServiceResolverFactory.GetResolver()
    {
      return new ServiceResolver(registrations);
    }

    IServiceResolver ISingletonServiceResolverFactory.GetResolver()
    {
      var singletons = registrations.Where(x => x.Metadata.Lifetime == ServiceLifetime.Singleton).ToArray();
      return new ServiceResolver(singletons);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Scenarios.ServiceRegistry"/> class.
    /// </summary>
    /// <param name="registrations">Registrations.</param>
    public ServiceRegistry(IReadOnlyCollection<IServiceRegistration> registrations)
    {
      if(registrations == null)
        throw new ArgumentNullException(nameof(registrations));

      this.registrations = registrations;
    }
  }
}
