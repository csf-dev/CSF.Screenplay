//
// SynchronousHttpClientAdapter.cs
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
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Stopwatch;

namespace CSF.Screenplay.WebApis.Services
{
  /// <summary>
  /// Default implementation of <see cref="IMakesSynchronousHttpRequests"/>.
  /// </summary>
  public class SynchronousHttpClientAdapter : IMakesSynchronousHttpRequests
  {
    readonly HttpClient httpClient;
    readonly TimeSpan defaultTimeout;

    /// <summary>
    /// Sends the request and gets its response.
    /// </summary>
    /// <param name="request">Request.</param>
    public HttpResponseMessage Send(HttpRequestMessage request) => Send(request, null);

    /// <summary>
    /// Sends the request and gets its response.
    /// </summary>
    /// <param name="request">Request.</param>
    /// <param name="timeout">Timeout.</param>
    public HttpResponseMessage Send(HttpRequestMessage request, TimeSpan? timeout)
    {
      var actualTimeout = timeout.GetValueOrDefault(defaultTimeout);
      var tokenSource = new CancellationTokenSource(actualTimeout);
      var token = tokenSource.Token;

      try
      {
        return httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, token).Result;
      }
      catch(TaskCanceledException ex)
      {
        var timeFormatter = new TimeSpanFormatter();
        var message = String.Format(Resources.ExceptionFormats.ApiRequestTimedOut, timeFormatter.Format(actualTimeout));
        throw new TimeoutException(message, ex);
      }
      catch(HttpRequestException ex)
      {
        throw new WebApiException(Resources.ExceptionFormats.UnexpectedApiRequestFailure, ex);
      }
      catch(AggregateException ex)
      {
        if(ex.InnerExceptions.OfType<TaskCanceledException>().Any())
        {
          var timeFormatter = new TimeSpanFormatter();
          var message = String.Format(Resources.ExceptionFormats.ApiRequestTimedOut, timeFormatter.Format(actualTimeout));
          throw new TimeoutException(message, ex);
        }
        else if(ex.InnerExceptions.OfType<HttpRequestException>().Any())
          throw new WebApiException(Resources.ExceptionFormats.UnexpectedApiRequestFailure, ex);

        throw;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.WebApis.Services.SynchronousHttpClientAdapter"/> class.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    /// <param name="defaultTimeout">Default timeout.</param>
    public SynchronousHttpClientAdapter(HttpClient httpClient, TimeSpan? defaultTimeout = null)
    {
      if(httpClient == null)
        throw new ArgumentNullException(nameof(httpClient));

      this.httpClient = httpClient;
      this.defaultTimeout = defaultTimeout ?? TimeSpan.FromSeconds(30);
    }
  }
}
