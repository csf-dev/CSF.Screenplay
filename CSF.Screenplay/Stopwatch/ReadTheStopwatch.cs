using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  public class ReadTheStopwatch : Question<TimeSpan>
  {
    protected override string GetReport(INamed actor) => $"{actor.Name} reads the elapsed time from the stopwatch";

    protected override TimeSpan PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<UseAStopwatch>();
      return ability.Watch.Elapsed;
    }
  }
}
