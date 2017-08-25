using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// A container for all of the service registrations which have been made.
  /// </summary>
  public interface IServiceRegistry : ISingletonServiceResolverFactory
  {
    /// <summary>
    /// Gets the registrations.
    /// </summary>
    /// <value>The registrations.</value>
    IReadOnlyCollection<IServiceRegistration> Registrations { get; }
  }
}
