//
// IMakesSynchronousHttpRequests.cs
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
  /// An adapter for an <c>HttpClient</c> which sends requests synchronously and returns only when the response is ready.
  /// </summary>
  public interface IMakesSynchronousHttpRequests
  {
    /// <summary>
    /// Sends the request and gets its response.
    /// </summary>
    /// <param name="request">Request.</param>
    HttpResponseMessage Send(HttpRequestMessage request);

    /// <summary>
    /// Sends the request and gets its response.
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="timeout">Timeout.</param>
    HttpResponseMessage Send(HttpRequestMessage request, TimeSpan? timeout);
  }
}
