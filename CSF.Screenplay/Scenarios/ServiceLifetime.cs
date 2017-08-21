using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Enuermates the available lifetimes of registered services.
  /// </summary>
  public enum ServiceLifetime
  {
    /// <summary>
    /// Indicates that any lifetime is acceptable.  Services are not registered with this lifetime, but they may
    /// be queried in this manner.
    /// </summary>
    Any = 0,

    /// <summary>
    /// A service instance which exists throughout the entire test run.
    /// </summary>
    Singleton,

    /// <summary>
    /// A service which is created (from a factory/initialisation method) once for each scenario.
    /// </summary>
    PerScenario
  }
}
