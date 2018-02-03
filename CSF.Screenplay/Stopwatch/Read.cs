using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Stopwatch
{
  /// <summary>
  /// Static helper class, facilitating reading of the stopwatch.
  /// </summary>
  public static class Read
  {
    /// <summary>
    /// Gets a performable/question which represents reading the current value from the stopwatch.
    /// </summary>
    /// <returns>A performable/question.</returns>
    public static IQuestion<TimeSpan> TheStopwatch() => new ReadTheStopwatch();
  }
}
