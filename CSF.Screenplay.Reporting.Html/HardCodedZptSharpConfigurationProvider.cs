//
// HardCodedZptSharpConfigurationProvider.cs
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
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting
{
  class HardCodedZptSharpConfigurationProvider : Zpt.IPluginConfiguration
  {
    static readonly Type[] pluginTypes = {
      typeof(Zpt.ExpressionEvaluators.LoadExpressions.LoadExpressionEvaluator),
      typeof(Zpt.ExpressionEvaluators.CSharpExpressions.CSharpExpressionEvaluator),
      typeof(Zpt.ExpressionEvaluators.NotExpressions.NotExpressionEvaluator),
      typeof(Zpt.ExpressionEvaluators.PathExpressions.PathExpressionEvaluator),
      typeof(Zpt.ExpressionEvaluators.StringExpressions.StringExpressionEvaluator),
      typeof(Zpt.DocumentProviders.XmlLinqZptDocumentProvider),
      typeof(Zpt.DocumentProviders.HtmlZptDocumentProvider),
    };

    /// <summary>
    /// Gets all plugin assembly paths.
    /// </summary>
    /// <returns>The all plugin assembly paths.</returns>
    public IEnumerable<string> GetAllPluginAssemblyPaths()
      => pluginTypes.Select(x => System.IO.Path.GetFileNameWithoutExtension(x.Assembly.Location)).ToArray();

    /// <summary>
    /// Gets the name of the default expression evaluator type.
    /// </summary>
    /// <returns>The default expression evaluator type name.</returns>
    public string GetDefaultExpressionEvaluatorTypeName()
      => typeof(Zpt.ExpressionEvaluators.PathExpressions.PathExpressionEvaluator).FullName;

    /// <summary>
    /// Gets the default name of the html document provider type.
    /// </summary>
    /// <returns>The default html document provider type name.</returns>
    public string GetDefaultHtmlDocumentProviderTypeName()
      => typeof(Zpt.DocumentProviders.HtmlZptDocumentProvider).FullName;

    /// <summary>
    /// Gets the default name of the xml document provider type.
    /// </summary>
    /// <returns>The default xml document provider type name.</returns>
    public string GetDefaultXmlDocumentProviderTypeName()
      => typeof(Zpt.DocumentProviders.XmlLinqZptDocumentProvider).FullName;
  }
}
