using System;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.JsonApis.ObjectFormatters
{
  /// <summary>
  /// A Screenplay object formatter which transforms a JSON API exception into a human-readable report string.
  /// </summary>
  public class JsonApiExceptionFormatter : ObjectFormatter<JsonApiException>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string Format(JsonApiException obj) => obj.Message;
  }
}
