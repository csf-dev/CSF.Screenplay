using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

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
    /// Subscribe to the specified test run.
    /// </summary>
    /// <param name="testRun">Test run.</param>
    void Subscribe(IProvidesTestRunEvents testRun);

    /// <summary>
    /// Unsubscribe to the specified test run.
    /// </summary>
    /// <param name="testRun">Test run.</param>
    void Unsubscribe(IProvidesTestRunEvents testRun);

    /// <summary>
    /// Subscribe to the specified scenario.
    /// </summary>
    /// <param name="scenario">Test run.</param>
    void Subscribe(IScreenplayScenario scenario);

    /// <summary>
    /// Unsubscribe to the specified scenario.
    /// </summary>
    /// <param name="scenario">Test run.</param>
    void Unsubscribe(IScreenplayScenario scenario);
  }
}
