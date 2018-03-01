using System;
using System.Net.Http;

namespace CSF.Screenplay.JsonApis
{
  /// <summary>
  /// An object instance which provides all of the information required to execute a JSON web API.
  /// </summary>
  public interface IProvidesInvocationDetails
  {
    /// <summary>
    /// Gets an HTTP request message for an invocation of the service.
    /// </summary>
    /// <returns>The HTTP request.</returns>
    HttpRequestMessage GetRequestMessage();

    /// <summary>
    /// Gets an optional explicit timeout for an API request.  If this value is null then a default timeout will
    /// be used instead.
    /// </summary>
    /// <returns>The timeout.</returns>
    TimeSpan? GetTimeout();
  }
}
