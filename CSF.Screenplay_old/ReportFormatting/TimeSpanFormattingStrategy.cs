using System;
using System.Text;

namespace CSF.Screenplay.ReportFormatting
{
  /// <summary>
  /// An object formatting strategy which supports (at most) minutes, seconds and milliseconds.
  /// </summary>
  public class TimeSpanFormattingStrategy : ObjectFormattingStrategy<TimeSpan>, Stopwatch.IFormatsDurations
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
    public override string FormatForReport(TimeSpan obj)
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

    string Stopwatch.IFormatsDurations.GetDuration(TimeSpan timespan) => FormatForReport(timespan);

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
