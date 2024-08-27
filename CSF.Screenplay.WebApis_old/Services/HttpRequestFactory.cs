//
// HttpRequestFactory.cs
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
using System.Net.Http;

namespace CSF.Screenplay.WebApis.Services
{
  /// <summary>
  /// Default implementation of <see cref="ICreatesHttpRequests"/>.
  /// </summary>
  public class HttpRequestFactory : ICreatesHttpRequests
  {
    /// <summary>
    /// Creates the request from a given endpoint and request body.
    /// </summary>
    /// <returns>The HTTP request.</returns>
    /// <param name="endpoint">Endpoint.</param>
    /// <param name="payload">The request payload.</param>
    public virtual HttpRequestMessage CreateRequest(IEndpoint endpoint, object payload)
    {
      if(payload == null) return CreateRequest(endpoint, (HttpContent) null);
      return CreateRequest(endpoint, new StringContent(payload.ToString()));
    }

    /// <summary>
    /// Creates the request from a given endpoint and request body.
    /// </summary>
    /// <returns>The HTTP request.</returns>
    /// <param name="endpoint">Endpoint.</param>
    /// <param name="requestBody">Request body.</param>
    public HttpRequestMessage CreateRequest(IEndpoint endpoint, HttpContent requestBody)
    {
      AssertEndpointIsValid(endpoint);
      
      var output = new HttpRequestMessage(endpoint.HttpMethod, endpoint.Uri) {
        Content = requestBody,
      };

      AddHeadersToRequest(output, endpoint);

      return output;
    }

    void AssertEndpointIsValid(IEndpoint endpoint)
    {
      if(endpoint == null)
        throw new ArgumentNullException(nameof(endpoint));
      if(endpoint.HttpMethod == null)
        throw new ArgumentException(Resources.ExceptionFormats.HttpMethodMustNotBeNull, nameof(endpoint));
      if(endpoint.Uri == null)
        throw new ArgumentException(Resources.ExceptionFormats.UriMustNotBeNull, nameof(endpoint));
    }

    void AddHeadersToRequest(HttpRequestMessage request, IEndpoint endpoint)
    {
      if(endpoint.Headers == null) return;

      foreach(var header in endpoint.Headers.Keys)
        request.Headers.Add(header, endpoint.Headers[header]);
    }
  }
}
