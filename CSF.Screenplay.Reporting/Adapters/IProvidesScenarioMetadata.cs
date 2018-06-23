//
// IProvidesScenarioMetadata.cs
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
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Adapters
{
  /// <summary>
  /// An object which can provide detailled information about an <see cref="IScenario"/>.
  /// </summary>
  public interface IProvidesScenarioMetadata : IScenario
  {
    /// <summary>
    /// Gets a value indicating whether this scenario is a success.
    /// </summary>
    /// <value><c>true</c> if the scenario is a success; otherwise, <c>false</c>.</value>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether this scenario is a failure.
    /// </summary>
    /// <value><c>true</c> if the scenario is a failure; otherwise, <c>false</c>.</value>
    bool IsFailure { get; }

    /// <summary>
    /// Gets a value indicating whether this scenario is inconclusive.
    /// </summary>
    /// <value><c>true</c> if the scenario is inconclusive; otherwise, <c>false</c>.</value>
    bool IsInconclusive { get; }

    /// <summary>
    /// Gets the printable (human-readable) name of the scenario.
    /// </summary>
    /// <returns>The scenario name.</returns>
    string GetPrintableScenarioName();

    /// <summary>
    /// Gets the printable (human-readable) name of the feature.
    /// </summary>
    /// <returns>The feature name.</returns>
    string GetPrintableFeatureName();

    /// <summary>
    /// Gets the scenario ID if it is meaningful, otherwise returns a <c>null</c> reference.
    /// </summary>
    /// <returns>The scenario identifier if it is meaningful.</returns>
    string GetScenarioIdIfMeaningful();

    /// <summary>
    /// Gets the feature ID if it is meaningful, otherwise returns a <c>null</c> reference.
    /// </summary>
    /// <returns>The feature identifier if it is meaningful.</returns>
    string GetFeatureIdIfMeaningful();
  }
}
