//
// RelativeEndpoint.cs
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
namespace CSF.Screenplay.WebApis
{
  /// <summary>
  /// Abstract implementation of <see cref="Endpoint"/> which provides a relative URI.
  /// </summary>
  public abstract class RelativeEndpoint : Endpoint
  {
    /// <summary>
    /// Gets this endpoint's URI.
    /// </summary>
    /// <value>The URI.</value>
    public override Uri Uri
      => new Uri(GetRelativeUriString(), UriKind.Relative);

    /// <summary>
    /// Gets a <c>System.String</c> which provides a relative URI for this endpoint.  The URI should be relative to
    /// a well-known base URI, such as "the root of the application's URI space".
    /// </summary>
    /// <returns>The relative URI.</returns>
    protected abstract string GetRelativeUriString();
  }
}
