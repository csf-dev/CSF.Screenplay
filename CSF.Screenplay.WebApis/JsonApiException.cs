using System;
namespace CSF.Screenplay.JsonApis
{
  /// <summary>
  /// Exception raised when there is a problem executing a JSON web API call.
  /// </summary>
  [System.Serializable]
  public class JsonApiException : Exception
  {
    /// <summary>
    /// Gets or sets the HTTP response body related to a request which raised an error.
    /// </summary>
    /// <value>The error response body.</value>
    public string ErrorResponseBody
    {
      get {
        return this.Data.Contains("ErrorResponseBody") ? (string) this.Data["ErrorResponseBody"] : default(string);
      }
      set {
        this.Data["ErrorResponseBody"] = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:JsonApiException"/> class
    /// </summary>
    public JsonApiException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:JsonApiException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public JsonApiException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:JsonApiException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public JsonApiException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:JsonApiException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected JsonApiException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
