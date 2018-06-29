//
// FeatureModel.cs
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
using CSF.Screenplay.ReportModel.Adapters;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Model for HTML report features (wraps an <see cref="IHierarchicalFeature"/> instance).
  /// </summary>
  public class FeatureModel
  {
    readonly IHierarchicalFeature feature;

    /// <summary>
    /// Gets the identity of this feature.
    /// </summary>
    public string Id => feature.Name.Id;

    /// <summary>
    /// Gets a value indicating whether the <see cref="Id"/> should be displayed.
    /// </summary>
    /// <value><c>true</c> if the feature identifier should be displayed; otherwise, <c>false</c>.</value>
    public bool ShouldDisplayId => !String.IsNullOrEmpty(Id) && !feature.Name.IsIdGenerated;

    /// <summary>
    /// Gets the human-readable friendly-name of this feature.
    /// </summary>
    public string FriendlyName => feature.Name.Name;

    /// <summary>
    /// Gets a collection of the scenarios in the current feature.
    /// </summary>
    public IReadOnlyCollection<ScenarioModel> Scenarios
      => feature.Scenarios.Select(x => new ScenarioModel(x)).ToArray();

    /// <summary>
    /// Gets a value indicating whether this <see cref="IHierarchicalFeature"/> contains any scenarios
    /// which are themselves failures.
    /// </summary>
    /// <value><c>true</c> if this feature contains any failures; otherwise, <c>false</c>.</value>
    public bool HasFailures => feature.IsFailure;

    /// <summary>
    /// Gets a value indicating whether every scenario within this <see cref="IHierarchicalFeature"/> is a success.
    /// </summary>
    /// <value><c>true</c> if the feature contains only successful scenarios; otherwise, <c>false</c>.</value>
    public bool IsSuccess => feature.IsSuccess;

    /// <summary>
    /// Gets the HTML class attribute value indicating the outcome of a given feature.
    /// </summary>
    /// <returns>The feature outcome class.</returns>
    public string GetOutcomeClass()
    {
      if(feature == null) return String.Empty;
      var outcome = GetOutcomeClass(IsSuccess, HasFailures);
      return $"feature {outcome}";
    }

    string GetOutcomeClass(bool success, bool failure)
    {
      if(success) return ReportConstants.SuccessClass;
      if(failure) return ReportConstants.FailureClass;
      return ReportConstants.InconclusiveClass;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.FeatureModel"/> class.
    /// </summary>
    /// <param name="feature">Feature.</param>
    public FeatureModel(IHierarchicalFeature feature)
    {
      if(feature == null)
        throw new ArgumentNullException(nameof(feature));

      this.feature = feature;
    }
  }
}
