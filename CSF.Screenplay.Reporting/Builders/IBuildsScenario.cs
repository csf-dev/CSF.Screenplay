//
// IBuildsScenario.cs
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
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Builders
{
  /// <summary>
  /// An object which builds up a <see cref="Scenario"/> instance.
  /// </summary>
  public interface IBuildsScenario
  {
    /// <summary>
    /// Gets or sets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    string ScenarioIdName { get; set; }

    /// <summary>
    /// Gets or sets the scenario friendly name.
    /// </summary>
    /// <value>The scenario friendly name.</value>
    string ScenarioFriendlyName { get; set; }

    /// <summary>
    /// Gets or sets the feature friendly name.
    /// </summary>
    /// <value>The feature friendly name.</value>
    string FeatureFriendlyName { get; set; }

    /// <summary>
    /// Gets or sets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    string FeatureIdName { get; set; }

    /// <summary>
    /// Gets a value which indicates whether or not the <see cref="ScenarioIdName"/> was auto-generated (and therefore
    /// meaningless).
    /// </summary>
    /// <value><c>true</c> if the scenario identifier is generated; otherwise, <c>false</c>.</value>
    bool ScenarioIdIsGenerated { get; set; }

    /// <summary>
    /// Gets a value which indicates whether or not the <see cref="FeatureIdName"/> was auto-generated (and therefore
    /// meaningless).
    /// </summary>
    /// <value><c>true</c> if the feature identifier is generated; otherwise, <c>false</c>.</value>
    bool FeatureIdIsGenerated { get; set; }

    /// <summary>
    /// Gets a value which indicates if the scenario is finalised or not.
    /// </summary>
    /// <returns><c>true</c>, if the scenario is finalised, <c>false</c> otherwise.</returns>
    bool IsFinalised();

    /// <summary>
    /// Finalise the current scenario using the specified outcome.
    /// </summary>
    /// <param name="outcome">Outcome.</param>
    void Finalise(bool? outcome);

    /// <summary>
    /// Gets the built scenario.
    /// </summary>
    /// <returns>The scenario.</returns>
    Scenario GetScenario();

    /// <summary>
    /// Begins the a reportable performance.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="performable">Performable.</param>
    void BeginPerformance(INamed actor, IPerformable performable);

    /// <summary>
    /// Begins a category of performance.
    /// </summary>
    /// <param name="performanceCategory">Performance category.</param>
    void BeginPerformanceCategory(ReportableCategory performanceCategory);

    /// <summary>
    /// Records a result for the current performance.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="result">Result.</param>
    void RecordResult(IPerformable performable, object result);

    /// <summary>
    /// Records that the current performance failed.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="exception">Exception.</param>
    void RecordFailure(IPerformable performable, Exception exception);

    /// <summary>
    /// Records that the current performance was a success.
    /// </summary>
    /// <param name="performable">Performable.</param>
    void RecordSuccess(IPerformable performable);

    /// <summary>
    /// Ends the current performance category.
    /// </summary>
    void EndPerformanceCategory();

    /// <summary>
    /// Records the gaining of an ability.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    void GainAbility(INamed actor, IAbility ability);
  }
}
