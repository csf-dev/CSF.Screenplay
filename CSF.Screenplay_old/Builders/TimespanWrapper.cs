using System;
namespace CSF.Screenplay.Builders
{
  /// <summary>
  /// Basic timespan provider which wraps a given value.
  /// </summary>
  public class TimespanWrapper : IProvidesTimespan
  {
    TimeSpan timespan;

    TimeSpan IProvidesTimespan.GetTimespan() => timespan;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimespanWrapper"/> class.
    /// </summary>
    /// <param name="timespan">Timespan.</param>
    public TimespanWrapper(TimeSpan timespan)
    {
      this.timespan = timespan;
    }
  }
}
