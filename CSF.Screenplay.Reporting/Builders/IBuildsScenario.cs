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
  public interface IBuildsScenario
  {
    string ScenarioIdName { get; set; }

    string ScenarioFriendlyName { get; set; }

    string FeatureFriendlyName { get; set; }

    string FeatureIdName { get; set; }

    bool ScenarioIdIsGenerated { get; set; }

    bool FeatureIdIsGenerated { get; set; }

    void Finalise(bool? outcome);

    Scenario GetScenario();

    void BeginPerformance(INamed actor, IPerformable performable);

    void BeginPerformanceType(ReportableCategory performanceType);

    void RecordResult(IPerformable performable, object result);

    void RecordFailure(IPerformable performable, Exception exception);

    void RecordSuccess(IPerformable performable);

    void EndPerformanceType();

    void GainAbility(INamed actor, IAbility ability);
  }
}
