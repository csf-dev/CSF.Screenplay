using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Builder service which assists in the creation of service registrations.
  /// </summary>
  public interface IServiceRegistryBuilder
  {
    /// <summary>
    /// Registers a service which will be used across all scenarios within a test run.
    /// </summary>
    /// <param name="instance">Instance.</param>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The 1st type parameter.</typeparam>
    void RegisterSingleton<TService>(TService instance, string name = null) where TService : class;

    /// <summary>
    /// Registers a service which will be used across all scenarios within a test run.
    /// </summary>
    /// <param name="initialiser">Initialiser.</param>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The 1st type parameter.</typeparam>
    void RegisterSingleton<TService>(Func<TService> initialiser, string name = null) where TService : class;

    /// <summary>
    /// Registers a service which will be constructed afresh (using the given factory function) for
    /// each scenario within a test run.
    /// </summary>
    /// <param name="factory">Factory.</param>
    /// <param name="name">Name.</param>
    /// <typeparam name="TService">The 1st type parameter.</typeparam>
    void RegisterPerScenario<TService>(Func<IScreenplayScenario,TService> factory, string name = null) where TService : class;
  }
}
