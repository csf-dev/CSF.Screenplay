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
  public interface IBuildsReports
  {
    void BeginNewScenario(string idName,
                          string friendlyName,
                          string featureName,
                          string featureId,
                          Guid scenarioId,
                          bool scenarioIdIsGenerated,
                          bool featureIdIsGenerated);
    
    void EndScenario(bool? outcome,
                     Guid scenarioId);

    void BeginPerformance(INamed actor,
                          IPerformable performable,
                          Guid scenarioId);

    void BeginPerformanceType(ReportableCategory performanceType,
                              Guid scenarioId);

    void RecordResult(IPerformable performable,
                      object result,
                      Guid scenarioId);

    void RecordFailure(IPerformable performable,
                       Exception exception,
                       Guid scenarioId);


    void RecordSuccess(IPerformable performable,
                       Guid scenarioId);

    void EndPerformanceType(Guid scenarioId);

    void GainAbility(INamed actor,
                     IAbility ability,
                     Guid scenarioId);

    Report GetReport();
  }
}
