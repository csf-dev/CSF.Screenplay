using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Observes reportable events and hands them off to a handler implementation for actual processing.
  /// </summary>
  public class DelegatingReportableEventObserver : IObservesReportableEvents
  {
    #region fields

    readonly IHandlesReportableEvents handler;

    #endregion

    #region Actor-related handlers

    /// <summary>
    /// Subscribe to the given actor and report upon their actions.
    /// </summary>
    /// <param name="actor">Actor.</param>
    public virtual void Subscribe(IActor actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      actor.GainedAbility += OnGainAbility;
      actor.BeginPerformance += OnBegin;
      actor.EndPerformance += OnSuccess;
      actor.PerformanceResult += OnResult;
      actor.PerformanceFailed += OnFailure;
      actor.BeginGiven += OnBeginGiven;
      actor.EndGiven += OnEndGiven;
      actor.BeginWhen += OnBeginWhen;
      actor.EndWhen += OnEndWhen;
      actor.BeginThen += OnBeginThen;
      actor.EndThen += OnEndThen;
    }

    /// <summary>
    /// Unsubscribe from the given actor; cease reporting upon their actions.
    /// </summary>
    /// <param name="actor">Actor.</param>
    public virtual void Unsubscribe(IActor actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      actor.GainedAbility -= OnGainAbility;
      actor.BeginPerformance -= OnBegin;
      actor.EndPerformance -= OnSuccess;
      actor.PerformanceResult -= OnResult;
      actor.PerformanceFailed -= OnFailure;
      actor.BeginGiven -= OnBeginGiven;
      actor.EndGiven -= OnEndGiven;
      actor.BeginWhen -= OnBeginWhen;
      actor.EndWhen -= OnEndWhen;
      actor.BeginThen -= OnBeginThen;
      actor.EndThen -= OnEndThen;
    }

    void OnGainAbility(object sender, GainAbilityEventArgs ev)
    {
      handler.GainAbility(ev.Actor, ev.Ability, ev.Actor.ScenarioIdentity);
    }

    void OnBegin(object sender, BeginPerformanceEventArgs ev)
    {
      handler.Begin(ev.Actor, ev.Performable, ev.Actor.ScenarioIdentity);
    }

    void OnSuccess(object sender, EndSuccessfulPerformanceEventArgs ev)
    {
      handler.Success(ev.Actor, ev.Performable, ev.Actor.ScenarioIdentity);
    }

    void OnResult(object sender, PerformanceResultEventArgs ev)
    {
      handler.Result(ev.Actor, ev.Performable, ev.Result, ev.Actor.ScenarioIdentity);
    }

    void OnFailure(object sender, PerformanceFailureEventArgs ev)
    {
      handler.Failure(ev.Actor, ev.Performable, ev.Exception, ev.Actor.ScenarioIdentity);
    }

    void OnBeginGiven(object sender, ActorEventArgs ev)
    {
      handler.BeginGiven(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnEndGiven(object sender, ActorEventArgs ev)
    {
      handler.EndGiven(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnBeginWhen(object sender, ActorEventArgs ev)
    {
      handler.BeginWhen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnEndWhen(object sender, ActorEventArgs ev)
    {
      handler.EndWhen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnBeginThen(object sender, ActorEventArgs ev)
    {
      handler.BeginThen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnEndThen(object sender, ActorEventArgs ev)
    {
      handler.EndThen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    #endregion

    #region Scenario-related handlers

    /// <summary>
    /// Subscribe to the specified scenario.
    /// </summary>
    /// <param name="scenario">Test run.</param>
    public void Subscribe(IScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      scenario.BeginScenario += OnNewScenario;
      scenario.EndScenario += OnEndScenario;
    }

    /// <summary>
    /// Unsubscribe to the specified scenario.
    /// </summary>
    /// <param name="scenario">Test run.</param>
    public void Unsubscribe(IScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      scenario.BeginScenario -= OnNewScenario;
      scenario.EndScenario -= OnEndScenario;
    }

    void OnNewScenario(object sender, BeginScenarioEventArgs ev)
    {
      handler.BeginNewScenario(ev.ScenarioId, ev.FeatureId, ev.ScenarioIdentity);
    }

    void OnEndScenario(object sender, EndScenarioEventArgs ev)
    {
      handler.CompleteScenario(ev.ScenarioOutcome, ev.ScenarioIdentity);
    }

    #endregion

    #region Test-run-related handlers

    /// <summary>
    /// Subscribe to the specified test run.
    /// </summary>
    /// <param name="testRun">Test run.</param>
    public void Subscribe(IProvidesTestRunEvents testRun)
    {
      if(testRun == null)
        throw new ArgumentNullException(nameof(testRun));

      testRun.BeginTestRun += OnNewTestRun;
      testRun.CompleteTestRun += OnEndTestRun;
    }

    /// <summary>
    /// Unsubscribe to the specified test run.
    /// </summary>
    /// <param name="testRun">Test run.</param>
    public void Unsubscribe(IProvidesTestRunEvents testRun)
    {
      if(testRun == null)
        throw new ArgumentNullException(nameof(testRun));

      testRun.BeginTestRun -= OnNewTestRun;
      testRun.CompleteTestRun -= OnEndTestRun;
    }

    void OnNewTestRun(object sender, EventArgs ev)
    {
      handler.BeginNewTestRun();
    }

    void OnEndTestRun(object sender, EventArgs ev)
    {
      handler.CompleteTestRun();
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.DelegatingReportableEventObserver"/>
    /// class.
    /// </summary>
    /// <param name="handler">Handler.</param>
    public DelegatingReportableEventObserver(IHandlesReportableEvents handler)
    {
      if(handler == null)
        throw new ArgumentNullException(nameof(handler));

      this.handler = handler;
    }

    #endregion
  }
}
