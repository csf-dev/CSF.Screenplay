﻿//
// GetJsonApiResult.cs
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
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.WebApis.Abilities;
using CSF.Screenplay.WebApis.Services;

namespace CSF.Screenplay.WebApis.Actions
{
  /// <summary>
  /// A Screenplay action type for invoking an HTTP web API.  This action executes the API call, verifies that the
  /// response was a success and then reads the response, converting it to a given type.
  /// </summary>
  public class GetApiResult<T> : Question<T>
  {
    readonly ICreatesHttpRequests requestFactory;
    readonly IVerifiesSuccessfulResponse responseVerifier;
    readonly IReadsResponseBodies responseReader;
    readonly IEndpoint endpoint;
    readonly object payload;
    readonly TimeSpan? timeout;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} invokes {endpoint.Name} and reads the result.";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override T PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<ConsumeWebServices>();
      var request = requestFactory.CreateRequest(endpoint, payload);
      var response = ability.SynchronousHttpClient.Send(request, timeout ?? endpoint.Timeout);
      responseVerifier.AssertThatResponseIsSuccessful(response);
      return responseReader.ReadResponseBody<T>(response);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:GetApiResult{T}"/> class.
    /// </summary>
    /// <param name="requestFactory">A factory for HTTP requests.</param>
    /// <param name="responseReader">An HTTP response reader.</param>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="responseVerifier">An optional response verifier.</param>
    /// <param name="payload">An optional request payload.</param>
    /// <param name="timeout">An optional timeout.</param>
    public GetApiResult(ICreatesHttpRequests requestFactory,
                        IReadsResponseBodies responseReader,
                        IEndpoint endpoint,
                        IVerifiesSuccessfulResponse responseVerifier = null,
                        object payload = null,
                        TimeSpan? timeout = null)
    {
      if(responseReader == null)
        throw new ArgumentNullException(nameof(responseReader));
      if(requestFactory == null)
        throw new ArgumentNullException(nameof(requestFactory));
      if(endpoint == null)
        throw new ArgumentNullException(nameof(endpoint));

      this.requestFactory = requestFactory;
      this.responseReader = responseReader;
      this.responseVerifier = responseVerifier ?? new HttpResponseSuccessVerifier();
      this.endpoint = endpoint;
      this.payload = payload;
      this.timeout = timeout;
    }
  }
}
