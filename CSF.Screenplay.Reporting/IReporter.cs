using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// A service which is informed when actors gain abilities or perform any performable items.  The reporter
  /// is then responsible for publishing that information in some manner, such as to a
  /// <c>System.Diagnostics.TraceSource</c>.
  /// </summary>
  public interface IReporter
  {
    /// <summary>
    /// Subscribe to the given actor and report upon their actions.
    /// </summary>
    /// <param name="actor">Actor.</param>
    void Subscribe(IActor actor);

    /// <summary>
    /// Unsubscribe from the given actor; cease reporting upon their actions.
    /// </summary>
    /// <param name="actor">Actor.</param>
    void Unsubscribe(IActor actor);

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    void BeginNewTestRun();

    /// <summary>
    /// Indicates to the reporter that a test run has completed; it may now prepare its report.
    /// </summary>
    void CompleteTestRun();

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    /// <param name="featureId">The uniquely identifying name for the feature.</param>
    void BeginNewScenario(string idName, string friendlyName, string featureName, string featureId);

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="success"><c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    void CompleteScenario(bool success);
  }
}
