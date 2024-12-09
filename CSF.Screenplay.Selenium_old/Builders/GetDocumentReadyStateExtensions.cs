//
// GetDocumentReadyStateExtensions.cs
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
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.ScriptResources;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Extension methods for a <see cref="ExecuteJavaScriptBuilder"/>, related to building performables which use the
  /// <see cref="GetDocumentReadyState"/> JavaScript.
  /// </summary>
  public static class GetDocumentReadyStateExtensions
  {
    /// <summary>
    /// Gets a performable which represents an invocation of the <see cref="GetDocumentReadyState"/> JavaScript.
    /// </summary>
    /// <returns>The JavaScript question performable.</returns>
    /// <param name="builder">Builder.</param>
    public static IPerformableJavaScriptWithResult WhichGetsTheDocumentReadyState(this ExecuteJavaScriptBuilder builder)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));

      return builder.AsPerformableBuilder.BuildQuestion<GetDocumentReadyState>();
    }
  }
}
