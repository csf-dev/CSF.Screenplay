//
// ExecuteJavaScriptBuilder.cs
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
using CSF.Screenplay.Selenium.StoredScripts;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// A builder type which assists in the creation of JavaScript execution actions.
  /// </summary>
  public class ExecuteJavaScriptBuilder : IBuildsJavaScriptPerformable
  {
    IPerformableJavaScript IBuildsJavaScriptPerformable.BuildAction(string script, object[] parameters)
      => new ExecuteJavaScript(script, parameters);

    IPerformableJavaScript IBuildsJavaScriptPerformable.BuildAction(IProvidesScript scriptProvider, object[] parameters)
      => new ExecuteJavaScriptProvider(scriptProvider, parameters);
    
    IPerformableJavaScript IBuildsJavaScriptPerformable.BuildAction<TProvider>(object[] parameters)
      => new ExecuteJavaScriptProvider(new TProvider(), parameters);

    IPerformableJavaScriptWithResult IBuildsJavaScriptPerformable.BuildQuestion(string script, object[] parameters)
      => new ExecuteJavaScriptAndGetResult(script, parameters);

    IPerformableJavaScriptWithResult IBuildsJavaScriptPerformable.BuildQuestion(IProvidesScript scriptProvider, object[] parameters)
      => new ExecuteJavaScriptProviderAndGetResult(scriptProvider, parameters);

    IPerformableJavaScriptWithResult IBuildsJavaScriptPerformable.BuildQuestion<TProvider>(object[] parameters)
      => new ExecuteJavaScriptProviderAndGetResult(new TProvider(), parameters);

    /// <summary>
    /// Gets the current instance, 'downcast' to <see cref="IBuildsJavaScriptPerformable"/>, in order to bring the interface
    /// methods into scope.
    /// </summary>
    /// <value>The current <see cref="ExecuteJavaScriptBuilder"/>.</value>
    public IBuildsJavaScriptPerformable AsPerformableBuilder => this;

    internal ExecuteJavaScriptBuilder() {}
  }
}
