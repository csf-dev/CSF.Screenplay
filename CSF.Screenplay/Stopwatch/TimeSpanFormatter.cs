using System;
using System.Text;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Stopwatch
{
  /// <summary>
  /// Simple duration formatter which supports (at most) minutes, seconds and milliseconds.
  /// </summary>
  public class TimeSpanFormatter : ObjectFormatter<TimeSpan>, IDurationFormatter
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
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string Format(TimeSpan obj)
    {
      StringBuilder output = new StringBuilder();

      if(obj.TotalMinutes >= 1)
        Append(output, obj.Minutes, Minute, Minutes);

      if(obj.Seconds >= 1)
        Append(output, obj.Seconds, Second, Seconds);

      if(obj.Milliseconds >= 1)
        Append(output, obj.Milliseconds, Millisecond, Milliseconds);

      return output.ToString();
    }

    string IDurationFormatter.GetDuration(TimeSpan timespan) => Format(timespan);

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
