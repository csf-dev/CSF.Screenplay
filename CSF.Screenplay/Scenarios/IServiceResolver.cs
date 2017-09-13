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
    void ReleaseLazySingletonServices();
  }
}
