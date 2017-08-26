using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// A service resolver (in essence a lightweight service locator) for Screenplay components.
  /// </summary>
  public interface IServiceResolver
  {
    /// <summary>
    /// Gets a service of the indicated type and name.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The desired service type.</typeparam>
    TService GetService<TService>(string name = null) where TService : class;

    /// <summary>
    /// Gets a service which matches the given metadata.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="metadata">Metadata.</param>
    object GetService(ServiceMetadata metadata);

    /// <summary>
    /// Gets a service of the indicated type and name, returning a <c>null</c> reference if no service is found.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The desired service type.</typeparam>
    TService GetOptionalService<TService>(string name = null) where TService : class;

    /// <summary>
    /// Gets a service which matches the given metadata, returning a <c>null</c> reference if no service is found.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="metadata">Metadata.</param>
    object GetOptionalService(ServiceMetadata metadata);

    /// <summary>
    /// Releases all of the services are which registered per-scenario.
    /// </summary>
    void ReleasePerScenarioServices();
  }
}
