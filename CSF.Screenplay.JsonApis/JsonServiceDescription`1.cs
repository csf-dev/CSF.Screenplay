using System;
namespace CSF.Screenplay.JsonApis
{
  /// <summary>
  /// Generic specialisation of <see cref="JsonServiceDescription"/>.  This is used as a convenience to indicate the
  /// expected return type of the JSON web API, which in turn makes it easier to invoke via generic type inference.
  /// </summary>
  public abstract class JsonServiceDescription<TResult> : JsonServiceDescription
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.JsonServiceDescription`1"/> class.
    /// </summary>
    /// <param name="timeout">Timeout.</param>
    /// <param name="requestPayload">A payload object containing the parameters for the API call.</param>
    public JsonServiceDescription(TimeSpan? timeout = null,
                                  object requestPayload = null) : base(timeout, requestPayload) {}
  }
}
