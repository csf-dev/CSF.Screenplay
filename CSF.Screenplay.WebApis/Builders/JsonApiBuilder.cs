//
// JsonApiBuilder.cs
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
using CSF.Screenplay.Builders;
using CSF.Screenplay.Performables;
using CSF.Screenplay.WebApis.Actions;

namespace CSF.Screenplay.WebApis.Builders
{
  public class JsonApiBuilder
  {
    readonly IEndpoint endpoint;
    object payload;
    IProvidesTimespan timeoutProvider;

    public JsonApiBuilder WithTheData(object payload)
    {
      this.payload = payload;
      return this;
    }

    public TimespanBuilder<JsonApiBuilder> WithATimeoutOf(int value)
    {
      if(timeoutProvider != null)
        throw new InvalidOperationException(Resources.ExceptionFormats.CannotSetTimeoutTwice);

      var timeoutBuilder = TimespanBuilder.Create(value, this);
      timeoutProvider = timeoutBuilder;
      return timeoutBuilder;
    }

    public IPerformable AndVerifyItSucceeds()
    {
      return new InvokeJsonApi(endpoint, payload, timeoutProvider?.GetTimespan());
    }

    public IPerformable<T> AndReadTheResultAs<T>()
    {
      return new GetJsonApiResult<T>(endpoint, payload, timeoutProvider?.GetTimespan());
    }

    public JsonApiBuilder(IEndpoint endpoint)
    {
      if(endpoint == null)
        throw new ArgumentNullException(nameof(endpoint));

      this.endpoint = endpoint;
      this.timeoutProvider = null;
      payload = null;
    }
  }
}
