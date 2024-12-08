//
// ExecuteJavaScriptProviderAndGetResult.cs
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
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.StoredScripts;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// A question class which executes a named/provided script in the web browser and returns the result.
  /// </summary>
  public class ExecuteJavaScriptProviderAndGetResult : Question<object>, IPerformableJavaScriptWithResult
  {
    readonly IProvidesScript provider;
    readonly object[] parameters;
    readonly IRunsScripts scriptRunner;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} executes {provider.Name} in their browser and gets the result";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override object PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var ability = actor.GetAbility<BrowseTheWeb>();
      return scriptRunner.ExecuteScript(provider, ability.WebDriver, parameters);
    }

    /// <summary>
    /// Performs the current action using a given <c>IWebDriver</c>.
    /// </summary>
    /// <returns>The script result.</returns>
    /// <param name="driver">The web driver.</param>
    public object PerformWith(IWebDriver driver)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      return scriptRunner.ExecuteScript(provider, driver, parameters);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Actions.ExecuteJavaScriptAndGetResult"/> class.
    /// </summary>
    /// <param name="provider">The script provider.</param>
    /// <param name="parameters">Parameters.</param>
    public ExecuteJavaScriptProviderAndGetResult(IProvidesScript provider, params object[] parameters)
    {
      if(provider == null)
        throw new ArgumentNullException(nameof(provider));

      this.provider = provider;
      this.parameters = parameters;
      scriptRunner = new ScriptRunner();
    }
  }
}
