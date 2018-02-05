using System;
namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Basic timespan provider which wraps a given value.
  /// </summary>
  public class TimespanWrapper : IProvidesTimespan
  {
    TimeSpan timespan;

    TimeSpan IProvidesTimespan.GetTimespan() => timespan;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Models.TimespanWrapper"/> class.
    /// </summary>
    /// <param name="timespan">Timespan.</param>
    public TimespanWrapper(TimeSpan timespan)
    {
      this.timespan = timespan;
    }
  }
}
