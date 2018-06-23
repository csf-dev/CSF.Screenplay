//
// ScenarioMetadataAdapter.cs
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

namespace CSF.Screenplay.ReportModel.Adapters
{
  /// <summary>
  /// A wrapper around an <see cref="IScenario"/> which extends its API with new/convenience functionality.
  /// </summary>
  public class ScenarioMetadataAdapter : IProvidesScenarioMetadata
  {
    readonly IScenario scenario;

    /// <summary>
    /// Gets the feature with which the current scenario is associated.
    /// </summary>
    /// <value>The feature.</value>
    public IFeature Feature => scenario.Feature;

    /// <summary>
    /// Gets the scenario outcome.
    /// </summary>
    /// <value>The outcome.</value>
    public ScenarioOutcome Outcome => scenario.Outcome;

    /// <summary>
    /// Gets the identity and name information.
    /// </summary>
    /// <value>The name.</value>
    public IdAndName Name => scenario.Name;

    /// <summary>
    /// Gets the reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public IReadOnlyList<IReportable> Reportables => scenario.Reportables;

    /// <summary>
    /// Gets a value indicating whether this scenario is a success.
    /// </summary>
    /// <value>
    /// <c>true</c> if the scenario is a success; otherwise, <c>false</c>.</value>
    public bool IsSuccess => Outcome == ScenarioOutcome.Success;

    /// <summary>
    /// Gets a value indicating whether this scenario is a failure.
    /// </summary>
    /// <value>
    /// <c>true</c> if the scenario is a failure; otherwise, <c>false</c>.</value>
    public bool IsFailure => Outcome == ScenarioOutcome.Failure;

    /// <summary>
    /// Gets a value indicating whether this scenario is inconclusive.
    /// </summary>
    /// <value>
    /// <c>true</c> if the scenario is inconclusive; otherwise, <c>false</c>.</value>
    public bool IsInconclusive => Outcome == ScenarioOutcome.Inconclusive;

    /// <summary>
    /// Gets the feature ID if it is meaningful, otherwise returns a <c>null</c> reference.
    /// </summary>
    /// <returns>The feature identifier if it is meaningful.</returns>
    public string GetFeatureIdIfMeaningful()
    {
      if((scenario.Feature?.Name?.IsIdGenerated).GetValueOrDefault(true))
        return null;

      var id = scenario.Feature?.Name?.Id;
      if(String.IsNullOrEmpty(id) || id == GetPrintableFeatureName())
        return null;

      return id;
    }

    /// <summary>
    /// Gets the printable (human-readable) name of the feature.
    /// </summary>
    /// <returns>The feature name.</returns>
    public string GetPrintableFeatureName()
    {
      var name = scenario.Feature?.Name?.Name;
      if(!String.IsNullOrEmpty(name)) return name;

      return scenario.Feature?.Name?.Id;
    }

    /// <summary>
    /// Gets the scenario ID if it is meaningful, otherwise returns a <c>null</c> reference.
    /// </summary>
    /// <returns>The scenario identifier if it is meaningful.</returns>
    public string GetScenarioIdIfMeaningful()
    {
      if((scenario.Name?.IsIdGenerated).GetValueOrDefault(true))
        return null;

      var id = scenario.Name?.Id;
      if(String.IsNullOrEmpty(id) || id == GetPrintableScenarioName())
        return null;

      return id;
    }

    /// <summary>
    /// Gets the printable (human-readable) name of the scenario.
    /// </summary>
    /// <returns>The scenario name.</returns>
    public string GetPrintableScenarioName()
    {
      var name = scenario.Name?.Name;
      if(!String.IsNullOrEmpty(name)) return name;

      return scenario.Name?.Id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Adapters.ScenarioMetadataAdapter"/> class.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    public ScenarioMetadataAdapter(IScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));
      this.scenario = scenario;
    }
  }
}
