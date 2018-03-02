//
// IEndpoint.cs
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
using System.Collections.Generic;
using System.Net.Http;

namespace CSF.Screenplay.WebApis
{
  /// <summary>
  /// Describes a Web API endpoint.
  /// </summary>
  public interface IEndpoint
  {
    /// <summary>
    /// Gets an optional (non-default) timeout which has been specified for this endpoint.
    /// </summary>
    /// <value>The timeout.</value>
    TimeSpan? Timeout { get; }

    /// <summary>
    /// Gets this endpoint's URI.
    /// </summary>
    /// <value>The URI.</value>
    Uri Uri { get; }

    /// <summary>
    /// Gets the HTTP method (sometimes referred to as "verb") for this endpoint, for example 'GET' or 'POST'.
    /// </summary>
    /// <value>The HTTP method.</value>
    HttpMethod HttpMethod { get; }

    /// <summary>
    /// Gets an optional collection of headers which are to be set upon any HTTP request which uses this endpoint.
    /// </summary>
    /// <value>The headers.</value>
    IDictionary<string,string> Headers { get; }

    /// <summary>
    /// Gets the name of the current instance.
    /// </summary>
    string Name { get; }
  }
}
