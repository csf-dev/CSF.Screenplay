using System;
using System.IO;
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
      var tokenSource = new CancellationTokenSource(timeout);
      var response = httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, tokenSource.Token);

      var result = response.Result;
      AssertThatResultIsSuccess(result);

      return result;
    }

    void AssertThatResultIsSuccess(HttpResponseMessage result)
    {
      if(result.IsSuccessStatusCode) return;

      var responseBody = result.Content.ReadAsStringAsync().Result;
      throw new JsonApiException($@"The API request failed: HTTP {result.StatusCode.ToString()}
{responseBody}");
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
