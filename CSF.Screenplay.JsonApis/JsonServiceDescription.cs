using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace CSF.Screenplay.JsonApis
{
  public abstract class JsonServiceDescription : IProvidesInvocationDetails
  {
    static readonly ContentType JsonContentType = new ContentType("application/json");

    readonly TimeSpan? timeout;
    readonly object requestPayload;
    readonly JsonSerializer serializer;

    public HttpRequestMessage GetRequestMessage()
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

    public TimeSpan? GetTimeout() => timeout;

    protected abstract Uri GetUri();

    protected virtual HttpMethod GetHttpMethod() => HttpMethod.Post;

    protected virtual IDictionary<string,string> GetHeaders() => null;

    protected virtual Encoding GetEncoding() => Encoding.UTF8;

    void ConfigureRequestHeaders(HttpRequestMessage request)
    {
      var headers = GetHeaders();
      if(headers == null)
        return;
      
      foreach(var header in headers.Keys)
      {
        request.Headers.Add(header, headers[header]);
      }
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

    public override string ToString() => $"the {GetType().Name} service";

    public JsonServiceDescription(TimeSpan? timeout = null,
                                  object requestPayload = null)
    {
      this.requestPayload = requestPayload;
      this.timeout = timeout;

      serializer = JsonSerializer.CreateDefault();
    }
  }
}
