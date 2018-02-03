using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  public class StopTheStopwatch : Performable
  {
    protected override string GetReport(INamed actor) => $"{actor.Name} stops the stopwatch";

    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<UseAStopwatch>();
      ability.Watch.Stop();
    }
  }
}
