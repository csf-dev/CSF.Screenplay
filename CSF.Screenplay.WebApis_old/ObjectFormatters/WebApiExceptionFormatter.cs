using System;
using CSF.Screenplay.ReportFormatting;

namespace CSF.Screenplay.WebApis.ObjectFormatters
{
  /// <summary>
  /// A Screenplay object formatter which transforms a Web API exception into a human-readable report string.
  /// </summary>
  public class WebApiExceptionFormatter : ObjectFormattingStrategy<WebApiException>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string FormatForReport(WebApiException obj) => obj.Message;
  }
}
