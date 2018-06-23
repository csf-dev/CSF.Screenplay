//
// HierarchicalFeature.cs
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

namespace CSF.Screenplay.ReportModel.Adapters
{
  /// <summary>
  /// A wrapper around a <see cref="IReport"/> for a given feature.  This provides a mechanism by which to filter
  /// that report's scenarios for only those within the current feature.
  /// </summary>
  public class HierarchicalFeature : IHierarchicalFeature
  {
    readonly IProvidesIdAndName feature;
    readonly IReport report;

    /// <summary>
    /// Gets the identity and name information.
    /// </summary>
    /// <value>The name.</value>
    public IdAndName Name => feature.Name;

    /// <summary>
    /// Gets the scenarios.
    /// </summary>
    /// <value>The scenarios.</value>
    public IReadOnlyCollection<IScenario> GetScenarios()
      => report.Scenarios.Where(ScenarioIsInThisFeature).ToArray();

    /// <summary>
    /// Gets a value indicating whether this feature is a success.
    /// </summary>
    /// <value>
    /// <c>true</c> if the feature is a success; otherwise, <c>false</c>.</value>
    public bool IsSuccess
      => GetScenarios().Select(x => new ScenarioMetadataAdapter(x)).All(x => x.IsSuccess);

    /// <summary>
    /// Gets a value indicating whether this feature is a failure.
    /// </summary>
    /// <value>
    /// <c>true</c> if the feature is a failure; otherwise, <c>false</c>.</value>
    public bool IsFailure
      => GetScenarios().Select(x => new ScenarioMetadataAdapter(x)).Any(x => x.IsFailure);

    bool ScenarioIsInThisFeature(IScenario scenario)
    {
      return (scenario.Feature?.Name?.Id != null
              && scenario.Feature.Name.Id == feature.Name.Id);
    }

    IEnumerable<IScenario> IProvidesScenarios.Scenarios => GetScenarios();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Adapters.HierarchicalFeature"/> class.
    /// </summary>
    /// <param name="feature">Feature.</param>
    /// <param name="report">Report.</param>
    public HierarchicalFeature(IProvidesIdAndName feature, IReport report)
    {
      if(report == null)
        throw new ArgumentNullException(nameof(report));
      if(feature == null)
        throw new ArgumentNullException(nameof(feature));
      this.feature = feature;
      this.report = report;
    }
  }
}
