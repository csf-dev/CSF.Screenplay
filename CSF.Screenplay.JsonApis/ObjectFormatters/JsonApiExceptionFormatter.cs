using System;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.JsonApis.ObjectFormatters
{
  public class JsonApiExceptionFormatter : ObjectFormatter<JsonApiException>
  {
    public override string Format(JsonApiException obj) => obj.Message;
  }
}
