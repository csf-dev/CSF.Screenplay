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
      var task = GetResponse(invocationDetails);

      var timeout = GetTimeout(invocationDetails);
      if(!task.Wait(timeout))
        throw new TimeoutException($"Timeout waiting for a response from a JSON API.  Waited for {timeout.ToString("g")}");
    }

    public virtual T GetResult<T>(IProvidesInvocationDetails invocationDetails)
    {
      var response = GetResponse(invocationDetails);
      var task = ConvertResponse<T>(response);

      var timeout = GetTimeout(invocationDetails);
      if(!task.Wait(timeout))
        throw new TimeoutException($"Timeout waiting for a response from a JSON API.  Waited for {timeout.ToString("g")}");

      return task.Result;
    }

    #endregion

    #region private methods

    async Task<HttpResponseMessage> GetResponse(IProvidesInvocationDetails invocationDetails)
    {
      if(invocationDetails == null)
        throw new ArgumentNullException(nameof(invocationDetails));

      var requestMessage = invocationDetails.GetRequestMessage();
      var timeout = GetTimeout(invocationDetails);

      var response = await httpClient.SendAsync(requestMessage);

      AssertThatResultIsSuccess(response, timeout);

      return response;
    }

    void AssertThatResultIsSuccess(HttpResponseMessage message, TimeSpan timeout)
    {
      try
      {
        message.EnsureSuccessStatusCode();
      }
      catch(HttpRequestException ex)
      {
        var content = message.Content.ReadAsStringAsync();
        content.Wait(timeout);
        var response = content.Result;

        throw new JsonApiException($@"The API request failed
{response}", ex);
      }
    }

    protected virtual async Task<T> ConvertResponse<T>(Task<HttpResponseMessage> response)
    {
      var buffer = new MemoryStream();
      await response.Result.Content.CopyToAsync(buffer);

      using(var reader = new StreamReader(buffer))
      {
        return (T) serializer.Deserialize(reader, typeof(T));
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

    public ConsumeJsonWebServices(Uri baseUri = null, TimeSpan? defaultTimeout = null)
    {
      httpClient = new HttpClient { BaseAddress = baseUri };
      this.defaultTimeout = defaultTimeout.GetValueOrDefault(SystemDefaultTimeout);
      serializer = JsonSerializer.CreateDefault();
    }

    #endregion
  }
}
