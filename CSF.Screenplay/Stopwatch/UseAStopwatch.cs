using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Stopwatch
{
  public class UseAStopwatch : Ability
  {
    readonly System.Diagnostics.Stopwatch watch;

    public System.Diagnostics.Stopwatch Watch => watch;

    protected override string GetReport(Actors.INamed actor) => $"{actor.Name} is able to use a stopwatch";

    public UseAStopwatch()
    {
      watch = new System.Diagnostics.Stopwatch();
    }
  }
}
