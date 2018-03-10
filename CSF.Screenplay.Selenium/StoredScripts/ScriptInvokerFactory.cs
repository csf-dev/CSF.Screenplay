//
// ArgumentsArrayConverter.cs
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
namespace CSF.Screenplay.Selenium.StoredScripts
{
  /// <summary>
  /// A special stored JavaScript which is used to invoke other JavaScripts.
  /// </summary>
  public class ScriptInvokerFactory : ICreatesInvocationScript
  {
    readonly ScriptResourceLoader loader;

    /// <summary>
    /// Gets an invocation script for the given entry point name.
    /// </summary>
    /// <returns>The invocation script.</returns>
    /// <param name="entryPoint">The name of the entry point which should be invoked.</param>
    public string GetScript(string entryPoint)
    {
      var invokerService = loader.GetScriptFor<ScriptInvokerFactory>();
      var invocationLine = GetInvocationLine(entryPoint);

      return String.Concat(invokerService, Environment.NewLine, invocationLine);
    }

    string GetInvocationLine(string entryPoint)
      => $"return invoker.invoke({entryPoint}, arguments);";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.StoredScripts.ScriptInvoker"/> class.
    /// </summary>
    public ScriptInvokerFactory()
    {
      loader = new ScriptResourceLoader();
    }
  }
}
