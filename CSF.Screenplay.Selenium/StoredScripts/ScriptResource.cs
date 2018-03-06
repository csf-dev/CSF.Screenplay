//
// ScriptResource.cs
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
using System.Reflection;
using CSF.Reflection;

namespace CSF.Screenplay.Selenium.StoredScripts
{
  /// <summary>
  /// Abstract implementation of <see cref="IProvidesScript"/> for a JavaScript which is stored as an embedded resource.
  /// </summary>
  public abstract class ScriptResource : IProvidesScript
  {
    const string
      DefaultEntryPointName = "executeScript";

    /// <summary>
    /// Gets the name of this script.
    /// </summary>
    /// <value>The name.</value>
    public virtual string Name => GetType().Name;

    /// <summary>
    /// Gets the name of the entry point to the script - this is the function exposed by
    /// <see cref="M:CSF.Screenplay.Selenium.StoredScripts.IProvidesScript.GetScript" />.
    /// </summary>
    /// <returns>The name of the entry point function.</returns>
    public virtual string GetEntryPointName() => DefaultEntryPointName;

    /// <summary>
    /// Gets the script fragment as a named function.  The name of that function is exposed via
    /// <see cref="M:CSF.Screenplay.Selenium.StoredScripts.IProvidesScript.GetEntryPointName" />.
    /// </summary>
    /// <returns>The script.</returns>
    public string GetScript()
    {
      var thisType = GetType();
      var scriptAssembly = thisType.Assembly;

      return scriptAssembly.GetManifestResourceText(thisType, $"{thisType.Name}.js");
    }
  }
}
