using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Represents a performable thing; a task, a question or an action.
  /// </summary>
  public interface IPerformable : IReportable
  {
    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    void PerformAs(IPerformer actor);
  }
}
