using System;
namespace CSF.Screenplay.Context
{
  /// <summary>
  /// Enumerates the possible lifetimes of a service in the <see cref="IScreenplayContext"/>.
  /// </summary>
  public enum ServiceLifetime
  {
    /// <summary>
    /// The service lives for the entire test run, essentially making it a singleton.
    /// </summary>
    PerTestRun = 0,

    /// <summary>
    /// The service is renewed for every individual test (AKA a "scenario" in cucumber-style terminology)
    /// </summary>
    PerScenario,
  }
}
