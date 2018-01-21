using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using Newtonsoft.Json;

namespace CSF.Screenplay.JsonApis.Abilities
{
  public class ConsumeJsonWebServices : Ability
  {
    #region fields

    static readonly TimeSpan SystemDefaultTimeout = new TimeSpan(0, 0, 30);

    readonly TimeSpan defaultTimeout;
    readonly HttpClient httpClient;
    readonly JsonSerializer serializer;

    #endregion

    #region public API

    public virtual void Execute(IProvidesInvocationDetails invocationDetails)
    {
      GetResponse(invocationDetails);
    }

    public virtual T GetResult<T>(IProvidesInvocationDetails invocationDetails)
    {
      var response = GetResponse(invocationDetails);
      return ConvertResponse<T>(response);
    }

    #endregion

    #region private methods

    HttpResponseMessage GetResponse(IProvidesInvocationDetails invocationDetails)
    {
      if(invocationDetails == null)
        throw new ArgumentNullException(nameof(invocationDetails));

      var requestMessage = invocationDetails.GetRequestMessage();
      var timeout = GetTimeout(invocationDetails);

      var response = httpClient.SendAsync(requestMessage);

      var waitSuccess = response.Wait(timeout);
      if(!waitSuccess)
        throw new TimeoutException($"Timeout waiting for a response from a JSON API.  Waited for {timeout.ToString("g")}");

      AssertThatResultIsSuccess(response.Result, timeout);

      return response.Result;
    }

    void AssertThatResultIsSuccess(HttpResponseMessage result, TimeSpan timeout)
    {
      try
      {
        result.EnsureSuccessStatusCode();
      }
      catch(HttpRequestException ex)
      {
        var response = GetResultString(result, timeout);
        throw new JsonApiException($@"The API request failed: HTTP {result.StatusCode.ToString()}
{response}", ex);
      }
    }

    string GetResultString(HttpResponseMessage result, TimeSpan timeout)
    {
      Task<string> contentTask = null;

      try
      {
        contentTask = result.Content.ReadAsStringAsync();
      }
      catch(Exception ex)
      {
        throw new JsonApiException($"The API request failed with no content: HTTP {result.StatusCode.ToString()}", ex);
      }

      contentTask.Wait(timeout);
      return contentTask.Result;
    }

    protected virtual T ConvertResponse<T>(HttpResponseMessage response)
    {
      using(var buffer = new MemoryStream())
      {
        var copyTask = response.Content.CopyToAsync(buffer);
        copyTask.Wait(TimeSpan.FromSeconds(5));

        buffer.Position = 0;

        using(var reader = new StreamReader(buffer))
        {
          return (T) serializer.Deserialize(reader, typeof(T));
        }
      }
    }

    TimeSpan GetTimeout(IProvidesInvocationDetails invocationDetails)
      => invocationDetails.GetTimeout().GetValueOrDefault(defaultTimeout);

    #endregion

    #region boilerplate Ability overrides

    protected override string GetReport(Actors.INamed actor)
    => $"{actor.Name} can consume JSON web services.";

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      if(disposing)
        httpClient.Dispose();
    }

    #endregion

    #region constructor

    public ConsumeJsonWebServices(string baseUriString, TimeSpan? defaultTimeout = null)
      : this(new Uri(baseUriString, UriKind.Absolute), defaultTimeout) {}

    public ConsumeJsonWebServices(Uri baseUri = null, TimeSpan? defaultTimeout = null)
    {
      httpClient = new HttpClient { BaseAddress = baseUri };
      this.defaultTimeout = defaultTimeout.GetValueOrDefault(SystemDefaultTimeout);
      serializer = JsonSerializer.CreateDefault();
    }

    #endregion
  }
}
