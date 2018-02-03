using System;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.JsonApis.ObjectFormatters
{
  /// <summary>
  /// Object formatter for a <c>System.TimeoutException</c>, which just emits the exception message with no other details.
  /// </summary>
  public class TimeoutExceptionFormatter : ObjectFormatter<TimeoutException>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string Format(TimeoutException obj) => obj.Message;
  }
}
