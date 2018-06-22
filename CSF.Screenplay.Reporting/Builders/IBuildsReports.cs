//
// IBuildsReports.cs
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
  /// An object which builds a <see cref="Report"/> instance.
  /// </summary>
  public interface IBuildsReports
  {
    /// <summary>
    /// Begins a new scenario.
    /// </summary>
    /// <param name="idName">Identifier name.</param>
    /// <param name="friendlyName">Friendly name.</param>
    /// <param name="featureName">Feature name.</param>
    /// <param name="featureId">Feature identifier.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    /// <param name="scenarioIdIsGenerated">If set to <c>true</c> scenario identifier is generated.</param>
    /// <param name="featureIdIsGenerated">If set to <c>true</c> feature identifier is generated.</param>
    void BeginNewScenario(string idName,
                          string friendlyName,
                          string featureName,
                          string featureId,
                          Guid scenarioId,
                          bool scenarioIdIsGenerated,
                          bool featureIdIsGenerated);

    /// <summary>
    /// Ends the identified scenario.
    /// </summary>
    /// <param name="outcome">Outcome.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void EndScenario(bool? outcome,
                     Guid scenarioId);

    /// <summary>
    /// Begins a performance for the identified scenario.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="performable">Performable.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void BeginPerformance(INamed actor,
                          IPerformable performable,
                          Guid scenarioId);

    /// <summary>
    /// Begins performance category for the identified scenario.
    /// </summary>
    /// <param name="performanceType">Performance type.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void BeginPerformanceCategory(ReportableCategory performanceType,
                                  Guid scenarioId);

    /// <summary>
    /// Records a result for the identified scenario.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="result">Result.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void RecordResult(IPerformable performable,
                      object result,
                      Guid scenarioId);

    /// <summary>
    /// Records a failure for the identified scenario.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="exception">Exception.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void RecordFailure(IPerformable performable,
                       Exception exception,
                       Guid scenarioId);

    /// <summary>
    /// Records the success for the identified scenario.
    /// </summary>
    /// <param name="performable">Performable.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void RecordSuccess(IPerformable performable,
                       Guid scenarioId);

    /// <summary>
    /// Ends the current performance category for the identified scenario.
    /// </summary>
    /// <param name="scenarioId">Scenario identifier.</param>
    void EndPerformanceCategory(Guid scenarioId);

    /// <summary>
    /// Gains the ability for the identified scenario.
    /// </summary>
    /// <param name="actor">Actor.</param>
    /// <param name="ability">Ability.</param>
    /// <param name="scenarioId">Scenario identifier.</param>
    void GainAbility(INamed actor,
                     IAbility ability,
                     Guid scenarioId);

    /// <summary>
    /// Gets the report.
    /// </summary>
    /// <returns>The report.</returns>
    Report GetReport();
  }
}
