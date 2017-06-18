using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Performables
{
  /// <summary>
  /// Base class for performable types which return a result.
  /// </summary>
  public abstract class Performable<TResult> : IPerformable<TResult>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected virtual string GetReport(INamed actor)
    {
      return $"{actor.Name} performs {GetType().Name}.";
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected abstract TResult PerformAs(IPerformer actor);

    void IPerformable.PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      PerformAs(actor);
    }

    object IPerformableWithResult.PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      return PerformAs(actor);
    }

    TResult IPerformable<TResult>.PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      return PerformAs(actor);
    }

    string IReportable.GetReport(INamed actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      return GetReport(actor);
    }
  }
}
