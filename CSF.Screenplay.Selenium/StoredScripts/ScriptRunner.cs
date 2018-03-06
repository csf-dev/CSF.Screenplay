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
    /// <summary>
    /// Executes the script and returns the result.
    /// </summary>
    /// <returns>The script result.</returns>
    /// <param name="script">A JavaScript.</param>
    /// <param name="webDriver">A web driver that runs scripts.</param>
    /// <param name="arguments">The script arguments.</param>
    public object ExecuteScript(IProvidesScript script, IJavaScriptExecutor webDriver, params object[] arguments)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      var runnableScript = GetRunnableScript(script);
      return webDriver.ExecuteScript(runnableScript, arguments);
    }

    string GetRunnableScript(IProvidesScript script)
      => String.Concat(script.GetScript(), Environment.NewLine, $"{script.GetEntryPointName()}(arguments);");
  }
}
