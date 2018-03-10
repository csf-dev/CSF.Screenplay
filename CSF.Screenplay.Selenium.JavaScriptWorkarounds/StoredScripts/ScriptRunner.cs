//
// ScriptRunner.cs
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
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.StoredScripts
{
  /// <summary>
  /// Default implementation of <see cref="IRunsScripts"/>
  /// </summary>
  public class ScriptRunner : IRunsScripts
  {
    readonly ICreatesInvocationScript invoker;
    
    /// <summary>
    /// Executes the script and returns the result.
    /// </summary>
    /// <returns>The script result.</returns>
    /// <param name="script">A JavaScript.</param>
    /// <param name="webDriver">A web driver.</param>
    /// <param name="arguments">The script arguments.</param>
    public object ExecuteScript(string script, IWebDriver webDriver, params object[] arguments)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      var javaScriptRunner = GetJavaScriptExecutor(webDriver);
      return javaScriptRunner.ExecuteScript(script, arguments);
    }

    /// <summary>
    /// Executes the script exposed by the given script provider and returns the result.
    /// </summary>
    /// <returns>The script result.</returns>
    /// <param name="script">A JavaScript provider.</param>
    /// <param name="webDriver">A web driver.</param>
    /// <param name="arguments">The script arguments.</param>
    public object ExecuteScript(IProvidesScript script, IWebDriver webDriver, params object[] arguments)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      var scriptBody = GetScriptBodyWithInvoker(script);
      return ExecuteScript(scriptBody, webDriver, arguments);
    }

    string GetScriptBodyWithInvoker(IProvidesScript script)
    {
      var scriptBody = script.GetScript();
      var invokerBody = invoker.GetScript(script.GetEntryPointName());

      return String.Concat(scriptBody, Environment.NewLine, invokerBody);
    }

    IJavaScriptExecutor GetJavaScriptExecutor(IWebDriver driver)
    {
      var jsDriver = driver as IJavaScriptExecutor;

      if(jsDriver == null)
        throw new ArgumentException($"The {nameof(IWebDriver)} must support the execution of JavaScript.", nameof(driver));

      return jsDriver;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.StoredScripts.ScriptRunner"/> class.
    /// </summary>
    public ScriptRunner() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.StoredScripts.ScriptRunner"/> class.
    /// </summary>
    /// <param name="invoker">A JavaScript which invokes other scripts via their named entry points.</param>
    public ScriptRunner(ICreatesInvocationScript invoker)
    {
      this.invoker = invoker ?? new ScriptInvokerFactory();
    }
  }
}
