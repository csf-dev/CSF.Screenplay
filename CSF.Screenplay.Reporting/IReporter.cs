using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

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
  }
}
