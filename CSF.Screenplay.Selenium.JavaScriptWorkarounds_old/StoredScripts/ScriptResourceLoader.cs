//
// ScriptResourceLoader.cs
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
using CSF.Reflection;

namespace CSF.Screenplay.Selenium.StoredScripts
{
  /// <summary>
  /// Loads a <c>string</c> manifest resource representing a JavaScript which matches the name of a <c>System.Type</c>.
  /// </summary>
  public class ScriptResourceLoader
  {
    /// <summary>
    /// Gets the JavaScript for the given type.
    /// </summary>
    /// <returns>The JavaScript resource.</returns>
    /// <typeparam name="T">The type for which to load script.</typeparam>
    public string GetScriptFor<T>()
      => GetScriptFor(typeof(T));

    /// <summary>
    /// Gets the JavaScript for the given type.
    /// </summary>
    /// <returns>The JavaScript resource.</returns>
    /// <param name="type">The type for which to load script.</param>
    public string GetScriptFor(Type type)
    {
      if(type == null)
        throw new ArgumentNullException(nameof(type));
      
      var scriptAssembly = type.Assembly;

      return scriptAssembly.GetManifestResourceText(type, $"{type.Name}.js");
    }
  }
}
