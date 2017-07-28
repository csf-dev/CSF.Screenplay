using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting.Trace
{
  /// <summary>
  /// Implementation of <see cref="IReporter"/> which writes report data to the <c>System.Diagnostics.TraceSource</c>
  /// held within the <see cref="TraceConstants"/> class.
  /// </summary>
  public class TraceReporter : NoOpReporter
  {
    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    protected override void GainAbility(INamed actor, IAbility ability)
    {
      var report = new AbilityReport(actor, ability);
      TraceConstants.Source.TraceData(TraceConstants.AbilityType, TraceConstants.AbilityId, report);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected override void Begin(INamed actor, IPerformable performable)
    {
      var report = new BeginReport(actor, performable);
      TraceConstants.Source.TraceData(TraceConstants.BeginType, TraceConstants.BeginId, report);
    }

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    protected override void Failure(INamed actor, IPerformable performable, Exception exception)
    {
      var report = new FailureReport(actor, performable, exception);
      TraceConstants.Source.TraceData(TraceConstants.FailureType, TraceConstants.FailureId, report);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected override void Success(INamed actor, IPerformable performable)
    {
      var report = new SuccessReport(actor, performable);
      TraceConstants.Source.TraceData(TraceConstants.SuccessType, TraceConstants.SuccessId, report);
    }

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    protected override void Result(INamed actor, IPerformable performable, object result)
    {
      var report = new ResultReport(actor, performable, result);
      TraceConstants.Source.TraceData(TraceConstants.ResultType, TraceConstants.ResultId, report);
    }
  }
}
