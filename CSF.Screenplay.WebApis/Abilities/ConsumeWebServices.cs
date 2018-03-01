//
// ConsumeWebServices.cs
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
using CSF.Screenplay.Abilities;
using CSF.Screenplay.WebApis.Services;

namespace CSF.Screenplay.WebApis.Abilities
{
  /// <summary>
  /// A Screenplay Ability indicating that an actor may execute HTTP web APIs and read their responses.
  /// </summary>
  public class ConsumeWebServices : Ability
  {
    readonly HttpClient httpClient;
    readonly IMakesSynchronousHttpRequests synchronousClientAdapter;

    /// <summary>
    /// Gets an HTTP client.
    /// </summary>
    /// <value>The HTTP client.</value>
    public HttpClient HttpClient => httpClient;

    /// <summary>
    /// Gets an adapter which allows the reading of synchronous HTTP requests.
    /// </summary>
    /// <value>The synchronous HTTP client.</value>
    public IMakesSynchronousHttpRequests SynchronousHttpClient => synchronousClientAdapter;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(Actors.INamed actor)
      => $"{actor.Name} can consume HTTP web services.";

    /// <summary>
    /// Performs disposal of the current instance.
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then we are explicitly disposing.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      if(disposing)
        httpClient.Dispose();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.WebApis.Abilities.ConsumeWebServices"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    /// <param name="synchronousClientAdapter">Synchronous client adapter.</param>
    /// <param name="defaultTimeout">Default timeout.</param>
    public ConsumeWebServices(HttpClient httpClient,
                              IMakesSynchronousHttpRequests synchronousClientAdapter = null,
                              TimeSpan? defaultTimeout = null)
    {
      if(httpClient == null)
        throw new ArgumentNullException(nameof(httpClient));

      this.httpClient = httpClient;
      this.synchronousClientAdapter = synchronousClientAdapter ?? new SynchronousHttpClientAdapter(httpClient, defaultTimeout);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.WebApis.Abilities.ConsumeWebServices"/> class.
    /// </summary>
    /// <param name="baseUri">The base URI for endpoints which use relative URIs.</param>
    /// <param name="synchronousClientAdapter">Synchronous client adapter.</param>
    /// <param name="defaultTimeout">Default timeout.</param>
    public ConsumeWebServices(Uri baseUri = null,
                              IMakesSynchronousHttpRequests synchronousClientAdapter = null,
                              TimeSpan? defaultTimeout = null)
    {
      if(httpClient == null)
        throw new ArgumentNullException(nameof(httpClient));

      this.httpClient = new HttpClient() {
        BaseAddress = baseUri
      };
      this.synchronousClientAdapter = synchronousClientAdapter ?? new SynchronousHttpClientAdapter(httpClient, defaultTimeout);
    }
  }
}
