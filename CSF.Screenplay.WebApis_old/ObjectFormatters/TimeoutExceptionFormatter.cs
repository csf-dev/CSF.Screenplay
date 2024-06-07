using System;
using CSF.Screenplay.ReportFormatting;

namespace CSF.Screenplay.WebApis.ObjectFormatters
{
  /// <summary>
  /// Object formatter for a <c>System.TimeoutException</c>, which just emits the exception message with no other details.
  /// </summary>
  public class TimeoutExceptionFormatter : ObjectFormattingStrategy<TimeoutException>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string FormatForReport(TimeoutException obj) => obj.Message;
  }
}
