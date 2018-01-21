using System;
namespace CSF.Screenplay.JsonApis
{
  public abstract class JsonServiceDescription<TResult> : JsonServiceDescription
  {
    public JsonServiceDescription(TimeSpan? timeout = null,
                                  object requestPayload = null) : base(timeout, requestPayload) {}
  }
}
