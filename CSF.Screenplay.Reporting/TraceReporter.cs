using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IReporter"/> which writes report data to the <c>System.Diagnostics.TraceSource</c>
  /// held within the <see cref="Trace"/> class.
  /// </summary>
  public class TraceReporter : Reporter
  {
    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    public override void GainAbility(INamed actor, IAbility ability)
    {
      var report = new AbilityReport(actor, ability);
      Trace.Source.TraceData(Trace.AbilityType, Trace.AbilityId, report);
    }

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    public override void Begin(INamed actor, IPerformable performable)
    {
      var report = new BeginReport(actor, performable);
      Trace.Source.TraceData(Trace.BeginType, Trace.BeginId, report);
    }

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    public override void Failure(INamed actor, IPerformable performable)
    {
      Failure(actor, performable, null);
    }

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    public override void Failure(INamed actor, IPerformable performable, Exception exception)
    {
      var report = new FailureReport(actor, performable, exception);
      Trace.Source.TraceData(Trace.FailureType, Trace.FailureId, report);
    }

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    public override void Success(INamed actor, IPerformable performable)
    {
      var report = new SuccessReport(actor, performable);
      Trace.Source.TraceData(Trace.SuccessType, Trace.SuccessId, report);
    }

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    public override void Result(INamed actor, IPerformable performable, object result)
    {
      var report = new ResultReport(actor, performable, result);
      Trace.Source.TraceData(Trace.ResultType, Trace.ResultId, report);
    }
  }
}
