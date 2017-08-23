using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Abstract implementation of <see cref="IServiceRegistrationProvider"/>, suitable for subclassing
  /// in custom integrations.
  /// </summary>
  public abstract class ServiceRegistrationProvider : IServiceRegistrationProvider
  {
    /// <summary>
    /// Gets the service registry, containing all of the required registrations.
    /// </summary>
    /// <returns>The service registry.</returns>
    public virtual ServiceRegistry GetServiceRegistry()
    {
      var builder = new ServiceRegistryBuilder();
      RegisterServices(builder);
      return builder.BuildRegistry();
    }

    /// <summary>
    /// Registers all of the required Screenplay services using a builder instance.
    /// </summary>
    /// <param name="builder">Builder.</param>
    protected abstract void RegisterServices(IServiceRegistryBuilder builder);
  }
}
