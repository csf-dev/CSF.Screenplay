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
    #region fields

    readonly HttpClient httpClient;
    readonly JsonSerializer serializer;

    #endregion

    #region public API

    /// <summary>
    /// Gets the response from an HTTP request, using the specified timeout.  This overload discards that response
    /// and simply returns if the request was successful.
    /// </summary>
    /// <param name="request">The HTTP API request.</param>
    /// <param name="timeout">Timeout.</param>
    public void GetResponse(HttpRequestMessage request, TimeSpan timeout)
    {
      GetResponseMessage(request, timeout);
    }

    /// <summary>
    /// Gets the response from an HTTP request, using the specified timeout.  This overload converts the response
    /// from a JSON object into an object type.
    /// </summary>
    /// <returns>The deserialized response object.</returns>
    /// <param name="request">The HTTP API request.</param>
    /// <param name="timeout">Timeout.</param>
    /// <typeparam name="T">The expected type of the response object.</typeparam>
    public T GetResponse<T>(HttpRequestMessage request, TimeSpan timeout)
    {
      var response = GetResponseMessage(request, timeout);
      return ConvertResponse<T>(response);
    }

    #endregion

    #region methods

    HttpResponseMessage GetResponseMessage(HttpRequestMessage request, TimeSpan timeout)
    {
      var response = GetResponseRespectingTimeout(request, timeout);
      AssertThatResultIsSuccess(response);
      return response;
    }

    HttpResponseMessage GetResponseRespectingTimeout(HttpRequestMessage request, TimeSpan timeout)
    {
      var tokenSource = new CancellationTokenSource(timeout);
      var token = tokenSource.Token;

      try
      {
        return httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).Result;
      }
      catch(TaskCanceledException ex)
      {
        var timeFormatter = new TimeSpanFormatter();
        var message = String.Format(Resources.ExceptionFormats.ApiRequestTimedOut, timeFormatter.Format(timeout));
        throw new TimeoutException(message, ex);
      }
      catch(HttpRequestException ex)
      {
        throw new JsonApiException(Resources.ExceptionFormats.UnexpectedApiRequestFailure, ex);
      }
      catch(AggregateException ex)
      {
        if(ex.InnerExceptions.OfType<TaskCanceledException>().Any())
        {
          var timeFormatter = new TimeSpanFormatter();
          var message = String.Format(Resources.ExceptionFormats.ApiRequestTimedOut, timeFormatter.Format(timeout));
          throw new TimeoutException(message, ex);
        }
        else if(ex.InnerExceptions.OfType<HttpRequestException>().Any())
          throw new JsonApiException(Resources.ExceptionFormats.UnexpectedApiRequestFailure, ex);

        throw;
      }
    }

    void AssertThatResultIsSuccess(HttpResponseMessage result)
    {
      if(result.IsSuccessStatusCode) return;

      var responseBody = result.Content.ReadAsStringAsync().Result;

      var message = String.Format(Resources.ExceptionFormats.ApiRequestFailedWithStatusCode, (int) result.StatusCode);
      throw new JsonApiException(message) {
        ErrorResponseBody = responseBody
      };
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
          throw new JsonApiException(Resources.ExceptionFormats.TimeoutConvertingResponseBody);

        buffer.Position = 0;

        using(var reader = new StreamReader(buffer))
        {
          return (T) serializer.Deserialize(reader, typeof(T));
        }
      }
    }

    #endregion

    #region IDisposable Support
    bool disposedValue;

    /// <summary>
    /// Performs disposal of the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then we are explicitly disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposedValue)
      {
        if(disposing)
        {
          httpClient.Dispose();
        }

        disposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.JsonApis.Abilities.SynchronousJsonAdapter"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.JsonApis.Abilities.SynchronousJsonAdapter"/>. The <see cref="Dispose()"/> method
    /// leaves the <see cref="T:CSF.Screenplay.JsonApis.Abilities.SynchronousJsonAdapter"/> in an unusable state. After
    /// calling <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.JsonApis.Abilities.SynchronousJsonAdapter"/> so the garbage collector can reclaim
    /// the memory that the <see cref="T:CSF.Screenplay.JsonApis.Abilities.SynchronousJsonAdapter"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.Abilities.SynchronousJsonAdapter"/> class.
    /// </summary>
    /// <param name="httpClientBaseUri">Http client base URI.</param>
    public SynchronousJsonAdapter(Uri httpClientBaseUri)
    {
      disposedValue = false;

      httpClient = new HttpClient {
        BaseAddress = httpClientBaseUri,
        Timeout = Timeout.InfiniteTimeSpan,
      };
      serializer = JsonSerializer.CreateDefault();
    }

    #endregion
  }
}
