//
// ExecuteJavaScriptProvider.cs
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
using CSF.Screenplay.Selenium.StoredScripts;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// An action class which executes a named/provided script in the web browser and ignores the result.
  /// </summary>
  public class ExecuteJavaScriptProvider : Performable, IPerformableJavaScript
  {
    readonly ExecuteJavaScriptProviderAndGetResult innerScriptAction;
    readonly IProvidesScript provider;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} executes {provider.Name} in their browser";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
      => ((IPerformable<object>) innerScriptAction).PerformAs(actor);

    /// <summary>
    /// Performs the current action using a given <c>IWebDriver</c>.
    /// </summary>
    /// <param name="driver">The web driver.</param>
    public void PerformWith(IWebDriver driver) => innerScriptAction.PerformWith(driver);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Actions.ExecuteJavaScript"/> class.
    /// </summary>
    /// <param name="script">Script.</param>
    /// <param name="parameters">Parameters.</param>
    public ExecuteJavaScriptProvider(IProvidesScript script, params object[] parameters)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));
      
      innerScriptAction = new ExecuteJavaScriptProviderAndGetResult(script, parameters);
      provider = script;
    }
  }
}
