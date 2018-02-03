using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSF.Screenplay.JsonApis.Abilities
{
  public class SynchronousJsonGateway : IDisposable
  {
    #region fields

    readonly HttpClient httpClient;
    readonly JsonSerializer serializer;

    #endregion

    #region public API

    public void GetResponse(HttpRequestMessage request, TimeSpan timeout)
    {
      GetResponseMessage(request, timeout);
    }

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
        throw new TimeoutException($"The JSON API request timed out after {timeout.ToString("g")}.", ex);
      }
      catch(HttpRequestException ex)
      {
        throw new JsonApiException("The JSON API request failed.", ex);
      }
      catch(AggregateException ex)
      {
        if(ex.InnerExceptions.OfType<TaskCanceledException>().Any())
          throw new TimeoutException($"The JSON API request timed out after {timeout.ToString("g")}.", ex);
        else if(ex.InnerExceptions.OfType<HttpRequestException>().Any())
          throw new JsonApiException("The JSON API request failed.", ex);

        throw;
      }
    }

    void AssertThatResultIsSuccess(HttpResponseMessage result)
    {
      if(result.IsSuccessStatusCode) return;

      var responseBody = result.Content.ReadAsStringAsync().Result;

      var message = $"The JSON API request failed: HTTP {(int) result.StatusCode}";
      throw new JsonApiException(message) {
        ErrorResponseBody = responseBody
      };
    }

    protected virtual T ConvertResponse<T>(HttpResponseMessage response)
    {
      using(var buffer = new MemoryStream())
      {
        var copyTask = response.Content.CopyToAsync(buffer);
        var waitSuccess = copyTask.Wait(TimeSpan.FromSeconds(1));
        if(!waitSuccess)
          throw new JsonApiException("Timeout whilst converting JSON response to string. This should probably never happen?!");

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

    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    #region constructor

    public SynchronousJsonGateway(Uri httpClientBaseUri)
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
