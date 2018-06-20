//
// ReportModel.cs
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
  /// Model for HTML reports (wraps a <see cref="Report"/> instance).
  /// </summary>
  public class ReportModel
  {
    readonly IProvidesHierarchicalFeatures report;
    readonly IObjectFormattingService formattingService;

    /// <summary>
    /// Gets a collection of the scenarios in this report.
    /// </summary>
    /// <value>The scenarios.</value>
    public virtual IReadOnlyList<FeatureModel> Features
    => report.GetFeatures().Select(x => new FeatureModel(x, formattingService)).ToArray();

    /// <summary>
    /// Gets the count of features (total) in this report.
    /// </summary>
    /// <value>The feature count.</value>
    public virtual int TotalFeatureCount => report.GetFeatures().Count;

    /// <summary>
    /// Gets the count of successful features in this report.
    /// </summary>
    /// <value>The successful feature count.</value>
    public virtual int SuccessfulFeatureCount => report.GetFeatures().Count(x => x.IsSuccess);

    /// <summary>
    /// Gets the count of failing features in this report.
    /// </summary>
    /// <value>The failing feature count.</value>
    public virtual int FailingFeatureCount => report.GetFeatures().Count(x => x.IsFailure);

    /// <summary>
    /// Gets the scenarios in the current report instance.
    /// </summary>
    /// <value>The scenarios.</value>
    public virtual IReadOnlyList<ScenarioModel> Scenarios
      => report.GetFeatures().SelectMany(x => x.Scenarios).Select(x => new ScenarioModel(x, formattingService)).ToArray();

    /// <summary>
    /// Gets the count of scenarios (total) in this report.
    /// </summary>
    /// <value>The scenario count.</value>
    public virtual int TotalScenarioCount => report.Scenarios.Count();

    /// <summary>
    /// Gets the count of successful scenarios in this report.
    /// </summary>
    /// <value>The successful scenario count.</value>
    public virtual int SuccessfulScenarioCount => report.Scenarios.Count(x => x.Outcome == true);

    /// <summary>
    /// Gets the count of failing scenarios in this report.
    /// </summary>
    /// <value>The failing scenario count.</value>
    public virtual int FailingScenarioCount => report.Scenarios.Count(x => x.Outcome == false);

    /// <summary>
    /// Gets the timestamp for the creation of this report.
    /// </summary>
    /// <value>The timestamp.</value>
    public virtual DateTime Timestamp => report.Timestamp;

    /// <summary>
    /// Gets the formatted time.
    /// </summary>
    /// <value>The formatted time.</value>
    public string FormattedTime => Timestamp.ToString("T");

    /// <summary>
    /// Gets the formatted date.
    /// </summary>
    /// <value>The formatted date.</value>
    public string FormattedDate => Timestamp.ToString("D");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.ReportModel"/> class.
    /// </summary>
    /// <param name="report">Report.</param>
    /// <param name="formattingService">Formatting service.</param>
    public ReportModel(IReport report, IObjectFormattingService formattingService)
    {
      if(formattingService == null)
        throw new ArgumentNullException(nameof(formattingService));
      if(report == null)
        throw new ArgumentNullException(nameof(report));

      this.report = new HierarchicalReportAdapter(report);
      this.formattingService = formattingService;
    }
  }
}
