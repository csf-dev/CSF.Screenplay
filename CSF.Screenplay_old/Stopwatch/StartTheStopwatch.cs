using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  /// <summary>
  /// A performable action representing the actor starting their stopwatch (beginning timing).
  /// </summary>
  public class StartTheStopwatch : Performable
  {
    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} starts the stopwatch";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<UseAStopwatch>();
      ability.Watch.Start();
    }
  }
}
