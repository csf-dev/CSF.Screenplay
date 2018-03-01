//
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
  public class GetJsonApiResult<T> : Question<T>
  {
    readonly JsonHttpRequestFactory requestFactory;
    readonly IVerifiesSuccessfulResponse responseVerifier;
    readonly IReadsResponseBodies responseReader;
    readonly IEndpoint endpoint;
    readonly object payload;
    readonly TimeSpan? timeout;

    protected override string GetReport(INamed actor) => $"{actor.Name} invokes {endpoint.Name} and reads the result.";

    protected override T PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<ConsumeWebServices>();
      var request = requestFactory.GetRequest(endpoint, payload);
      var response = ability.SynchronousHttpClient.Send(request, timeout ?? endpoint.Timeout);
      responseVerifier.AssertThatResponseIsSuccessful(response);
      return responseReader.ReadResponseBody<T>(response);
    }

    public GetJsonApiResult(IEndpoint endpoint, object payload = null, TimeSpan? timeout = null)
    {
      if(endpoint == null)
        throw new ArgumentNullException(nameof(endpoint));

      this.endpoint = endpoint;
      this.payload = payload;
      this.timeout = timeout;

      requestFactory = new JsonHttpRequestFactory();
      responseVerifier = new HttpResponseSuccessVerifier();
      responseReader = new JsonHttpContentReaderWriter();
    }
  }
}
