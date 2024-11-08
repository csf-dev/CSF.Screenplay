//
// CustomJavaScriptBuilder.cs
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

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// A builder type for the creation of custom JavaScripts.  Instantiate this via <see cref="Execute.TheJavaScript"/>.
  /// </summary>
  public class CustomJavaScriptBuilder
  {
    readonly IBuildsJavaScriptPerformable builder;
    readonly string script;
    object[] parameters;

    /// <summary>
    /// Indicates that the given parameters are to be passed to the script, via the <c>arguments</c> biult-in
    /// JavaScript keyword.
    /// </summary>
    /// <returns>The parameters.</returns>
    /// <param name="parameters">Parameters.</param>
    public CustomJavaScriptBuilder WithTheParameters(params object[] parameters)
    {
      this.parameters = parameters;
      return this;
    }

    /// <summary>
    /// Gets the action; a result from the script will be returned.
    /// </summary>
    /// <returns>The action.</returns>
    public IPerformableJavaScriptWithResult AndGetTheResult()
    {
      return builder.BuildQuestion(script, parameters);
    }

    /// <summary>
    /// Gets the action; any result from the script will be discarded.
    /// </summary>
    /// <returns>The action.</returns>
    public IPerformableJavaScript AndIgnoreTheResult()
    {
      return builder.BuildAction(script, parameters);
    }

    internal CustomJavaScriptBuilder(string script)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));

      this.script = script;
      builder = new ExecuteJavaScriptBuilder();
    }
  }
}
