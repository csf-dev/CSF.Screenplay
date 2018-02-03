using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  public class ResetTheStopwatch : Performable
  {
    protected override string GetReport(INamed actor) => $"{actor.Name} resets the stopwatch to zero";

    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<UseAStopwatch>();
      ability.Watch.Reset();
    }
  }
}
