using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Extends <see cref="ServiceRegistryFactory"/> by adding a function cache in front of
  /// <see cref="GetServiceRegistry"/> method.  The registry will be created at most once
  /// per insyance of this type.
  /// </summary>
  public class CachingServiceRegistryFactory : ServiceRegistryFactory
  {
    readonly object syncRoot;
    IServiceRegistry registry;

    /// <summary>
    /// Gets the service registry, containing all of the required registrations.
    /// </summary>
    /// <returns>The service registry.</returns>
    public override IServiceRegistry GetServiceRegistry()
    {
      lock(syncRoot)
      {
        if(registry == null)
          registry = base.GetServiceRegistry();

        return registry;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Integration.CachingServiceRegistryFactory"/> class.
    /// </summary>
    /// <param name="builder">Builder.</param>
    public CachingServiceRegistryFactory(IIntegrationConfigBuilder builder) : base(builder)
    {
      syncRoot = new object();
    }
  }
}
