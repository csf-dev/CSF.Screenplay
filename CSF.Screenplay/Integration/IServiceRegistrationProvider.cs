using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// A type which provides the registrations of Screenplay services.
  /// </summary>
  public interface IServiceRegistrationProvider
  {
    /// <summary>
    /// Gets the service registry, containing all of the required registrations.
    /// </summary>
    /// <returns>The service registry.</returns>
    ServiceRegistry GetServiceRegistry();
  }
}
