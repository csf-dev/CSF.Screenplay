using System;
namespace CSF.Screenplay.Web.Models
{
  /// <summary>
  /// Basic timespan provider which wraps a given value.
  /// </summary>
  public class TimespanWrapper : IProvidesTimespan
  {
    TimeSpan timespan;

    TimeSpan IProvidesTimespan.GetTimespan() => timespan;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Models.TimespanWrapper"/> class.
    /// </summary>
    /// <param name="timespan">Timespan.</param>
    public TimespanWrapper(TimeSpan timespan)
    {
      this.timespan = timespan;
    }
  }
}
