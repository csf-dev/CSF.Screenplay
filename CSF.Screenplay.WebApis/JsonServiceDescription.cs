using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace CSF.Screenplay.JsonApis
{
  /// <summary>
  /// A base class for an object which describes a JSON web API service.  This provides all of the information
  /// required to call that web service.
  /// </summary>
  public abstract class JsonServiceDescription : IProvidesInvocationDetails
  {
    static readonly ContentType JsonContentType = new ContentType("application/json");

    readonly TimeSpan? timeout;
    readonly object requestPayload;
    readonly JsonSerializer serializer;

    /// <summary>
    /// Gets an HTTP request message for an invocation of the service.
    /// </summary>
    /// <returns>The HTTP request.</returns>
    public virtual HttpRequestMessage GetRequestMessage()
    {
      var output = new HttpRequestMessage
      {
        RequestUri = GetUri(),
        Method = GetHttpMethod(),
        Content = GetContent(),
      };

      ConfigureRequestHeaders(output);

      return output;
    }

    /// <summary>
    /// Gets an optional explicit timeout for an API request.  If this value is null then a default timeout will
    /// be used instead.
    /// </summary>
    /// <returns>The timeout.</returns>
    public TimeSpan? GetTimeout() => timeout;

    /// <summary>
    /// Gets the <c>System.Uri</c> to which the web API request will be sent.  It is recommended to use a
    /// relative URI here and a base URI in your <see cref="Abilities.ConsumeJsonWebServices"/> ability.
    /// </summary>
    /// <returns>The web API URI.</returns>
    protected virtual Uri GetUri() => new Uri(GetRelativeUriString(), UriKind.Relative);

    /// <summary>
    /// Gets the relative URI for the web API.  This should be relative to the base URI provided to your
    /// <see cref="Abilities.ConsumeJsonWebServices"/> ability.
    /// </summary>
    /// <returns>The relative web API URI.</returns>
    protected abstract string GetRelativeUriString();

    /// <summary>
    /// Gets the HTTP method (AKA 'verb') which shall be used for this web service call.  By default this will be 'POST'.
    /// </summary>
    /// <returns>The http method.</returns>
    protected virtual HttpMethod GetHttpMethod() => HttpMethod.Post;

    /// <summary>
    /// Gets an optional collection of HTTP headers to be added to the request.  This should be provided as a
    /// collection of key/value pairs.  If unset then no extra headers shall be added to the request except for
    /// an <c>Accept</c> header for <c>application/json</c>.
    /// </summary>
    /// <returns>The headers.</returns>
    protected virtual IDictionary<string,string> GetHeaders() => null;

    /// <summary>
    /// Gets the encoding to use for the HTTP request (including the serialized request payload).  This defaults
    /// to UTF-8.
    /// </summary>
    /// <returns>The encoding.</returns>
    protected virtual Encoding GetEncoding() => Encoding.UTF8;

    void ConfigureRequestHeaders(HttpRequestMessage request)
    {
      AddAcceptsJsonHeader(request);

      var headers = GetHeaders();
      if(headers == null)
        return;

      foreach(var header in headers.Keys)
      {
        request.Headers.Add(header, headers[header]);
      }
    }

    /// <summary>
    /// Adds the request header: <c>Accept: application/json</c>.
    /// </summary>
    /// <param name="request">The request message.</param>
    protected virtual void AddAcceptsJsonHeader(HttpRequestMessage request)
    {
      request.Headers.Add("Accept", JsonContentType.MediaType);
    }

    HttpContent GetContent()
    {
      if(ReferenceEquals(requestPayload, null))
        return null;

      var serializedContent = SerializeContentToJson(requestPayload);
      return new StringContent(serializedContent, GetEncoding(), JsonContentType.MediaType);
    }

    string SerializeContentToJson(object content)
    {
      var sb = new StringBuilder();

      using(var writer = new StringWriter(sb))
      {
        serializer.Serialize(writer, content);
        writer.Flush();
      }

      return sb.ToString();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the
    /// current <see cref="T:CSF.Screenplay.JsonApis.JsonServiceDescription"/>.  Override to provide
    /// a more useful/meaningful name for this service.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CSF.Screenplay.JsonApis.JsonServiceDescription"/>.</returns>
    public override string ToString() => $"the {GetType().Name} service";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.JsonServiceDescription"/> class.
    /// </summary>
    /// <param name="timeout">Timeout.</param>
    /// <param name="requestPayload">A payload object containing the parameters for the API call.</param>
    public JsonServiceDescription(TimeSpan? timeout = null,
                                  object requestPayload = null)
    {
      this.requestPayload = requestPayload;
      this.timeout = timeout;

      serializer = JsonSerializer.CreateDefault();
    }
  }
}
