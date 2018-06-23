//
// ScenarioModel.cs
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
using CSF.Screenplay.Reporting.Adapters;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Model for HTML report scenarios (wraps a <see cref="Scenario"/> instance).
  /// </summary>
  public class ScenarioModel
  {
    readonly IProvidesScenarioMetadata scenario;
    readonly IObjectFormattingService formattingService;

    /// <summary>
    /// Gets the unique identifier name of the scenario.
    /// </summary>
    /// <value>The identifier.</value>
    public virtual string Id => scenario.Name.Id;

    /// <summary>
    /// Gets a value indicating whether the <see cref="Id"/> should be displayed.
    /// </summary>
    /// <value><c>true</c> if the scenario identifier should be displayed; otherwise, <c>false</c>.</value>
    public bool ShouldDisplayId => scenario.GetScenarioIdIfMeaningful() != null;

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The scenario name.</value>
    public virtual string FriendlyName => scenario.GetPrintableScenarioName();

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    /// <value>The feature name.</value>
    public virtual string FeatureName => scenario.GetPrintableFeatureName();

    /// <summary>
    /// Gets the identifier for the feature.
    /// </summary>
    /// <value>The feature identifier.</value>
    public virtual string FeatureId => scenario.Feature.Name.Id;

    /// <summary>
    /// Gets the contained reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public virtual IReadOnlyList<ReportableModel> Reportables
      => scenario.Reportables.Select(x => new ReportableModel(x, formattingService)).ToArray();

    /// <summary>
    /// Gets or sets the outcome for the test scenario.
    /// </summary>
    /// <value>The outcome.</value>
    public virtual bool? Outcome => scenario.Outcome;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/>
    /// is inconclusive (neither success nor failure).
    /// </summary>
    /// <value><c>true</c> if this scenario is inconclusive; otherwise, <c>false</c>.</value>
    public virtual bool IsInconclusive => scenario.IsInconclusive;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/>
    /// is a failure.
    /// </summary>
    /// <value><c>true</c> if this scenario is a failure; otherwise, <c>false</c>.</value>
    public virtual bool IsFailure => scenario.IsFailure;

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:CSF.Screenplay.Reporting.Models.Scenario"/> is a success.
    /// </summary>
    /// <value><c>true</c> if is success; otherwise, <c>false</c>.</value>
    public virtual bool IsSuccess => scenario.IsSuccess;

    /// <summary>
    /// Gets the HTML class attribute value indicating the outcome of a given scenario.
    /// </summary>
    /// <returns>The scenario outcome class.</returns>
    public string GetOutcomeClass()
    {
      if(scenario == null) return String.Empty;
      var outcome = GetOutcomeClass(scenario.IsSuccess, scenario.IsFailure);
      return $"scenario {outcome}";
    }

    string GetOutcomeClass(bool success, bool failure)
    {
      if(success) return ReportConstants.SuccessClass;
      if(failure) return ReportConstants.FailureClass;
      return ReportConstants.InconclusiveClass;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.ScenarioModel"/> class.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="formattingService">Formatting service.</param>
    public ScenarioModel(IScenario scenario, IObjectFormattingService formattingService)
    {
      if(formattingService == null)
        throw new ArgumentNullException(nameof(formattingService));
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      this.scenario = new ScenarioMetadataAdapter(scenario);
      this.formattingService = formattingService;
    }
  }
}
