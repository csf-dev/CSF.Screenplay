using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  public static class Read
  {
    public static IQuestion<TimeSpan> TheStopwatch() => new ReadTheStopwatch();
  }
}
