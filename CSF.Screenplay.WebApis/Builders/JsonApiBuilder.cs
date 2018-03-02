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
  /// <summary>
  /// A builder type for creating actions which relate to invoking JSON web APIs.
  /// </summary>
  public class JsonApiBuilder
  {
    readonly IEndpoint endpoint;
    object payload;
    IProvidesTimespan timeoutProvider;

    /// <summary>
    /// Provides a 'data payload' which will be serialized to JSON and included in the HTTP request body.
    /// </summary>
    /// <returns>The builder instance.</returns>
    /// <param name="payload">The request payload.</param>
    public JsonApiBuilder WithTheData(object payload)
    {
      this.payload = payload;
      return this;
    }

    /// <summary>
    /// Provides a custom/non-default timeout for the API call.  This uses a builder of its own to create the timeout.
    /// </summary>
    /// <returns>A timeout builder.</returns>
    /// <param name="value">The numeric number of milliseconds/seconds/minutes for which the timeout will last.</param>
    public TimespanBuilder<JsonApiBuilder> WithATimeoutOf(int value)
    {
      if(timeoutProvider != null)
        throw new InvalidOperationException(Resources.ExceptionFormats.CannotSetTimeoutTwice);

      var timeoutBuilder = TimespanBuilder.Create(value, this);
      timeoutProvider = timeoutBuilder;
      return timeoutBuilder;
    }

    /// <summary>
    /// Creates and returns an action which invokes the API and verifies that the response is a success, but does not
    /// read the response body.
    /// </summary>
    /// <returns>A Screenplay Action instance.</returns>
    public IPerformable AndVerifyItSucceeds()
    {
      return new InvokeJsonApi(endpoint, payload, timeoutProvider?.GetTimespan());
    }

    /// <summary>
    /// Creates and returns an action which invokes the API, verifies that the response is a success and then reads the
    /// response body and converts it from JSON to the specified type.
    /// </summary>
    /// <returns>A Screenplay Question instance.</returns>
    /// <typeparam name="T">The type into which the response body is to be converted.</typeparam>
    public IPerformable<T> AndReadTheResultAs<T>()
    {
      return new GetJsonApiResult<T>(endpoint, payload, timeoutProvider?.GetTimespan());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.WebApis.Builders.JsonApiBuilder"/> class.
    /// </summary>
    /// <param name="endpoint">Endpoint.</param>
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
