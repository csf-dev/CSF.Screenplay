using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Factory type for instances of <see cref="ServiceRegistry"/>
  /// </summary>
  public interface IServiceRegistryFactory
  {
    /// <summary>
    /// Gets the service registry.
    /// </summary>
    /// <returns>The service registry.</returns>
    IServiceRegistry GetServiceRegistry();
  }
}
