using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Abstract implementation of <see cref="IServiceRegistryFactory"/>, suitable for subclassing
  /// in custom integrations.
  /// </summary>
  public class ServiceRegistryFactory : IServiceRegistryFactory
  {
    readonly IIntegrationConfigBuilder integrationHelper;

    /// <summary>
    /// Gets the service registry, containing all of the required registrations.
    /// </summary>
    /// <returns>The service registry.</returns>
    public virtual IServiceRegistry GetServiceRegistry()
    {
      var builder = new ServiceRegistryBuilder();
      RegisterServices(builder);
      return builder.BuildRegistry();
    }

    void RegisterServices(IServiceRegistryBuilder builder)
    {
      foreach(var registrationCallback in integrationHelper.RegisterServices)
        registrationCallback(builder);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Integration.ServiceRegistrationProvider"/> class.
    /// </summary>
    /// <param name="integrationHelper">Integration helper.</param>
    public ServiceRegistryFactory(IIntegrationConfigBuilder integrationHelper)
    {
      if(integrationHelper == null)
        throw new ArgumentNullException(nameof(integrationHelper));

      this.integrationHelper = integrationHelper;
    }
  }
}
