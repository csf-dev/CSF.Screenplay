//
// ResourceBundler.cs
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
using System.Linq;
using System.Reflection;
using CSF.Reflection;

namespace CSF.Screenplay.Reporting.Views
{
  /// <summary>
  /// A very crude class which 'bundles' a collection of resources into a single string.  This does no minification,
  /// obfuscation or the like.  It simply concatenates the resources in the order listed.
  /// </summary>
  class ResourceBundler
  {
    Assembly ThisAssembly => Assembly.GetExecutingAssembly();

    /// <summary>
    /// Creates and returns a bundled resource, using the given resource names.
    /// </summary>
    /// <returns>The bundled resource.</returns>
    /// <param name="resourceNames">Resource names.</param>
    internal string GetBundle(params string[] resourceNames)
    {
      if(resourceNames == null) return String.Empty;

      return String.Join(Environment.NewLine, resourceNames.Select(GetResourceString));
    }

    string GetResourceString(string resourceName)
      => ThisAssembly.GetManifestResourceText(resourceName);
  }
}
