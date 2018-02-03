using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  /// <summary>
  /// A question which represents reading the current value from a stopwatch.
  /// </summary>
  public class ReadTheStopwatch : Question<TimeSpan>
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} reads the elapsed time from the stopwatch";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override TimeSpan PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<UseAStopwatch>();
      return ability.Watch.Elapsed;
    }
  }
}
