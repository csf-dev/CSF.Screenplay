using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Represents a performable thing; a task, a question or an action.
  /// Items of this type return a response/result value.
  /// </summary>
  public interface IPerformableWithResult : IPerformable
  {
    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    new object PerformAs(IPerformer actor);
  }
}
