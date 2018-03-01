//
// JsonHttpContentReaderWriter.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace CSF.Screenplay.WebApis.Services
{
  /// <summary>
  /// Implementation of both <see cref="ICreatesRequestBodies"/> and <see cref="IReadsResponseBodies"/> which
  /// serializes/deserializes request/response bodies to/from JSON.
  /// </summary>
  public class JsonHttpContentReaderWriter : ICreatesRequestBodies, IReadsResponseBodies
  {
    static readonly TimeSpan ReadResponseTimeout = TimeSpan.FromSeconds(5);

    public static readonly ContentType JsonContentType = new ContentType("application/json");

    readonly JsonSerializer serializer;
    readonly Encoding encoding;

    /// <summary>
    /// Creates an <c>HttpContent</c> representing the request body.
    /// </summary>
    /// <returns>The request body.</returns>
    /// <param name="payload">The data payload to form the request body.</param>
    public HttpContent CreateRequestBody(object payload)
    {
      var serializedContent = SerializeJson(payload);
      return new StringContent(serializedContent, encoding, JsonContentType.MediaType);
    }

    /// <summary>
    /// Reads the response body and gets an object which represents that response.
    /// </summary>
    /// <returns>The response body.</returns>
    /// <param name="response">Response.</param>
    /// <typeparam name="T">The expected response type.</typeparam>
    public T ReadResponseBody<T>(HttpResponseMessage response)
    {
      return DeserializeJson<T>(response);
    }

    string SerializeJson(object content)
    {
      var sb = new StringBuilder();

      using(var writer = new StringWriter(sb))
      {
        serializer.Serialize(writer, content);
        writer.Flush();
      }

      return sb.ToString();
    }

    T DeserializeJson<T>(HttpResponseMessage response)
    {
      using(var buffer = new MemoryStream())
      {
        PrepareBufferForReading(buffer, response);

        using(var reader = new StreamReader(buffer))
          return (T) serializer.Deserialize(reader, typeof(T));
      }
    }

    void PrepareBufferForReading(Stream buffer, HttpResponseMessage response)
    {
      var copyTask = response.Content.CopyToAsync(buffer);

      var copySuccess = copyTask.Wait(ReadResponseTimeout);
      if(!copySuccess)
        throw new WebApiException(Resources.ExceptionFormats.ReadResponseTimeout);

      // Reset the buffer to the start
      buffer.Position = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.WebApis.Services.JsonHttpContentReaderWriter"/> class.
    /// </summary>
    /// <param name="encoding">Encoding.</param>
    public JsonHttpContentReaderWriter(Encoding encoding = null)
    {
      serializer = new JsonSerializer();

      this.encoding = encoding ?? Encoding.UTF8;
    }
  }
}
