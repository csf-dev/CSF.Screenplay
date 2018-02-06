using System;
using System.Threading;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Stopwatch;

namespace CSF.Screenplay.Selenium.Waits
{
  /// <summary>
  /// A simple wait which has no resume condition except for the duration passing.
  /// </summary>
  public class GeneralWait : Performable
  {
    readonly TimeSpan timespan;
    readonly IDurationFormatter durationFormatter;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      var timeoutString = durationFormatter.GetDuration(timespan);
      return $"{actor.Name} waits for {timeoutString}.";
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      Thread.Sleep(timespan);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Waits.GeneralWait"/> class.
    /// </summary>
    /// <param name="timespan">Timespan.</param>
    public GeneralWait(TimeSpan timespan)
    {
      this.timespan = timespan;
      durationFormatter = new TimeSpanFormatter();
    }
  }
}
