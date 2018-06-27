//
// NoOpReportableEventHandler.cs
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
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An implementation of <see cref="IHandlesReportableEvents"/> which does nothing.  This essentially disables
  /// reporting.
  /// </summary>
  public class NoOpReportableEventHandler : IHandlesReportableEvents
  {
    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Begin(INamed actor, IPerformable performable, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginGiven(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="scenarioName">The scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginNewScenario(IdAndName scenarioName, IdAndName featureName, Guid scenarioIdentity) {}

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    public void BeginNewTestRun() {}

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginThen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void BeginWhen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="outcome">
    /// <c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void CompleteScenario(bool? outcome, Guid scenarioIdentity) {}

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    public void CompleteTestRun() {}

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndGiven(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndThen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void EndWhen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Failure(INamed actor, IPerformable performable, Exception exception, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void GainAbility(INamed actor, IAbility ability, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Result(INamed actor, IPerformable performable, object result, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    public void Success(INamed actor, IPerformable performable, Guid scenarioIdentity) {}
  }
}
