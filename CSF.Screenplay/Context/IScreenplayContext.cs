using System;
namespace CSF.Screenplay.Context
{
  /// <summary>
  /// Represents a screenplay context, which acts as a service locator for screenplay-related services.
  /// </summary>
  public interface IScreenplayContext
  {
    /// <summary>
    /// Gets the service, optionally specifying a service name.
    /// </summary>
    /// <returns>The service.</returns>
    /// <param name="name">The optional name of the registration.</param>
    /// <typeparam name="TService">The service type.</typeparam>
    TService GetService<TService>(string name = null) where TService : class;

    /// <summary>
    /// Registers a singleton service (the same instance will be used for all features/scenarios in the test run).
    /// </summary>
    /// <param name="instance">The service instance.</param>
    /// <param name="name">The optional name of the registration.</param>
    /// <typeparam name="TService">The service type.</typeparam>
    void RegisterSingleton<TService>(TService instance, string name = null) where TService : class;

    /// <summary>
    /// Registers a service which will have a per-scenario lifetime (it will be created afresh upon each new scenario).
    /// </summary>
    /// <param name="factory">A factory which will create service instances on-demand.</param>
    /// <param name="name">The optional name of the registration.</param>
    /// <typeparam name="TService">The service type.</typeparam>
    void RegisterPerScenario<TService>(Func<TService> factory, string name = null) where TService : class;

    /// <summary>
    /// Registers a service which will have a per-scenario lifetime (it will be created afresh upon each new scenario).
    /// </summary>
    /// <param name="name">The optional name of the registration.</param>
    /// <typeparam name="TService">The service type.</typeparam>
    void RegisterPerScenario<TService>(string name = null) where TService : class,new();
  }
}
