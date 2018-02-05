using System;
using System.Text;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Simple duration formatter which supports (at most) minutes, seconds and milliseconds.
  /// </summary>
  public class DurationFormatter : IDurationFormatter
  {
    const string
      Milliseconds  = "milliseconds",
      Millisecond   = "millisecond",
      Seconds       = "seconds",
      Second        = "second",
      Minutes       = "minutes",
      Minute        = "minute",
      Separator     = ", ";

    /// <summary>
    /// Gets the human-readable representation of the given timespan.
    /// </summary>
    /// <returns>The duration.</returns>
    /// <param name="timespan">Timespan.</param>
    public string GetDuration(TimeSpan timespan)
    {
      StringBuilder output = new StringBuilder();

      if(timespan.TotalMinutes >= 1)
        Append(output, timespan.Minutes, Minute, Minutes);

      if(timespan.Seconds >= 1)
        Append(output, timespan.Seconds, Second, Seconds);

      if(timespan.Milliseconds >= 1)
        Append(output, timespan.Milliseconds, Millisecond, Milliseconds);

      return output.ToString();
    }

    void Append(StringBuilder builder, int value, string singular, string plural)
    {
      if(builder.Length > 0)
        builder.Append(Separator);

      builder.Append(GetComponent(value, singular, plural));
    }

    string GetComponent(int value, string singular, string plural)
      => (value == 1)? $"{value} {singular}" : $"{value} {plural}";
  }
}
