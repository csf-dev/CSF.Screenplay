using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Base class for performable types.
  /// </summary>
  public abstract class Performable : IPerformable
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    public virtual string GetReport(INamed actor)
    {
      return $"{actor.Name} performs {GetType().Name}.";
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    public abstract void PerformAs(IPerformer actor);
  }
}
