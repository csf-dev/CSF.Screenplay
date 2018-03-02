using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Stopwatch;
using Newtonsoft.Json;

namespace CSF.Screenplay.JsonApis.Abilities
{
  /// <summary>
  /// An adapter type which wraps a <c>System.Net.Http.HttpClient</c>.  This adapter makes that HTTP client's
  /// usually-asynchronous methods synchronous (as is the requirement for Screenplay).
  /// </summary>
  public class SynchronousJsonAdapter : IDisposable
  {
    #region methods

    HttpResponseMessage GetResponseMessage(HttpRequestMessage request, TimeSpan timeout)
    {
      var response = GetResponseRespectingTimeout(request, timeout);
      AssertThatResultIsSuccess(response);
      return response;
    }

    /// <summary>
    /// Converts a JSON object found within an HTTP response body into a response object type.
    /// </summary>
    /// <returns>The response object.</returns>
    /// <param name="response">An HTTP response body.</param>
    /// <typeparam name="T">The expected type of the response object.</typeparam>
    protected virtual T ConvertResponse<T>(HttpResponseMessage response)
    {
      using(var buffer = new MemoryStream())
      {
        var copyTask = response.Content.CopyToAsync(buffer);
        var waitSuccess = copyTask.Wait(TimeSpan.FromSeconds(1));
        if(!waitSuccess)
          throw new WebApiException(Resources.ExceptionFormats.TimeoutConvertingResponseBody);

        buffer.Position = 0;

        using(var reader = new StreamReader(buffer))
        {
          return (T) serializer.Deserialize(reader, typeof(T));
        }
      }
    }

    #endregion

  }
}
