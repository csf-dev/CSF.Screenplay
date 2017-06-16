using System;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
  /// <summary>
  /// An actor which relays reports about its performances via an <see cref="IReporter"/> implementation.
  /// </summary>
  public class ReportingActor : Actor
  {
    readonly IReporter reporter;

    void WireUpReporting()
    {
      BeginPerformance += (sender, e) => reporter.Begin(e.Actor, e.Performable);
      EndPerformance += (sender, e) => reporter.Success(e.Actor, e.Performable);
      PerformanceResult += (sender, e) => reporter.Result(e.Actor, e.Performable, e.Result);
      PerformanceFailed += (sender, e) => reporter.Failure(e.Actor, e.Performable, e.Exception);
      GainedAbility += (sender, e) => reporter.GainAbility(e.Actor, e.Ability);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportingActor"/> class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the <paramref name="reporter"/> is null, then a new instance of <see cref="TraceReporter"/> will be used.
    /// </para>
    /// </remarks>
    /// <param name="name">The actor's name.</param>
    /// <param name="reporter">The reporter instance to use.</param>
    public ReportingActor(string name, IReporter reporter = null) : base(name)
    {
      this.reporter = reporter?? new TraceReporter();
      WireUpReporting();
    }
  }
}
