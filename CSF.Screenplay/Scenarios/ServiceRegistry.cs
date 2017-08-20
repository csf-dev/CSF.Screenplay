using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Registry type which contains all of the Screenplay service registrations.
  /// </summary>
  public class ServiceRegistry
  {
    readonly IReadOnlyCollection<IServiceRegistration> registrations;

    /// <summary>
    /// Creates and returns a service resolver instance.
    /// </summary>
    /// <returns>The resolver.</returns>
    public ServiceResolver GetResolver()
    {
      return new ServiceResolver(registrations);
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
