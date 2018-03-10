//
// IExecuteJavaScriptBuilder.cs
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
  /// A service which can build performable instances related to the execution of JavaScript.
  /// </summary>
  public interface IBuildsJavaScriptPerformable
  {
    /// <summary>
    /// Builds a performable action from the given script provider and parameters.
    /// </summary>
    /// <returns>The performable action.</returns>
    /// <param name="scriptProvider">A script provider.</param>
    /// <param name="parameters">The script parameters.</param>
    IPerformableJavaScript BuildAction(IProvidesScript scriptProvider, params object[] parameters);

    /// <summary>
    /// Builds a performable action from the given script and parameters.
    /// </summary>
    /// <returns>The performable action.</returns>
    /// <param name="script">A script.</param>
    /// <param name="parameters">The script parameters.</param>
    IPerformableJavaScript BuildAction(string script, params object[] parameters);

    /// <summary>
    /// Builds a performable action from the given script provider type and parameters.
    /// </summary>
    /// <returns>The performable action.</returns>
    /// <param name="parameters">The script parameters.</param>
    /// <typeparam name="TProvider">The script provider type</typeparam>
    IPerformableJavaScript BuildAction<TProvider>(params object[] parameters) where TProvider : IProvidesScript,new();

    /// <summary>
    /// Builds a performable question from the given script provider and parameters.
    /// </summary>
    /// <returns>The performable question.</returns>
    /// <param name="scriptProvider">A script provider.</param>
    /// <param name="parameters">The script parameters.</param>
    IPerformableJavaScriptWithResult BuildQuestion(IProvidesScript scriptProvider, params object[] parameters);

    /// <summary>
    /// Builds a performable question from the given script and parameters.
    /// </summary>
    /// <returns>The performable question.</returns>
    /// <param name="script">A script.</param>
    /// <param name="parameters">The script parameters.</param>
    IPerformableJavaScriptWithResult BuildQuestion(string script, params object[] parameters);

    /// <summary>
    /// Builds a performable question from the given script provider type and parameters.
    /// </summary>
    /// <returns>The performable question.</returns>
    /// <param name="parameters">The script parameters.</param>
    /// <typeparam name="TProvider">The script provider type</typeparam>
    IPerformableJavaScriptWithResult BuildQuestion<TProvider>(params object[] parameters) where TProvider : IProvidesScript,new();
  }
}
