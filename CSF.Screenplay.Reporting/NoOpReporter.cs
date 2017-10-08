using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// A reporter which does nothing.  This is a suitable base type for reporter implementations.
  /// </summary>
  public class NoOpReporter : IReporter
  {
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
      GainAbility(ev.Actor, ev.Ability, ev.Actor.ScenarioIdentity);
    }

    void OnBegin(object sender, BeginPerformanceEventArgs ev)
    {
      Begin(ev.Actor, ev.Performable, ev.Actor.ScenarioIdentity);
    }

    void OnSuccess(object sender, EndSuccessfulPerformanceEventArgs ev)
    {
      Success(ev.Actor, ev.Performable, ev.Actor.ScenarioIdentity);
    }

    void OnResult(object sender, PerformanceResultEventArgs ev)
    {
      Result(ev.Actor, ev.Performable, ev.Result, ev.Actor.ScenarioIdentity);
    }

    void OnFailure(object sender, PerformanceFailureEventArgs ev)
    {
      Failure(ev.Actor, ev.Performable, ev.Exception, ev.Actor.ScenarioIdentity);
    }

    void OnBeginGiven(object sender, ActorEventArgs ev)
    {
      BeginGiven(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnEndGiven(object sender, ActorEventArgs ev)
    {
      EndGiven(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnBeginWhen(object sender, ActorEventArgs ev)
    {
      BeginWhen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnEndWhen(object sender, ActorEventArgs ev)
    {
      EndWhen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnBeginThen(object sender, ActorEventArgs ev)
    {
      BeginThen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    void OnEndThen(object sender, ActorEventArgs ev)
    {
      EndThen(ev.Actor, ev.Actor.ScenarioIdentity);
    }

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void GainAbility(INamed actor, IAbility ability, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void Begin(INamed actor, IPerformable performable, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void Success(INamed actor, IPerformable performable, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void Result(INamed actor, IPerformable performable, object result, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void Failure(INamed actor, IPerformable performable, Exception exception, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void BeginGiven(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void EndGiven(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void BeginWhen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void EndWhen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void BeginThen(INamed actor, Guid scenarioIdentity) {}

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void EndThen(INamed actor, Guid scenarioIdentity) {}

    #endregion

    #region Scenario-related handlers

    /// <summary>
    /// Subscribe to the specified scenario.
    /// </summary>
    /// <param name="scenario">Test run.</param>
    public void Subscribe(IScreenplayScenario scenario)
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
    public void Unsubscribe(IScreenplayScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));

      scenario.BeginScenario -= OnNewScenario;
      scenario.EndScenario -= OnEndScenario;
    }

    void OnNewScenario(object sender, BeginScenarioEventArgs ev)
    {
      BeginNewScenario(ev.ScenarioId.Identity,
                       ev.ScenarioId.Name,
                       ev.FeatureId.Name,
                       ev.FeatureId.Identity,
                       ev.ScenarioIdentity);
    }

    void OnEndScenario(object sender, EndScenarioEventArgs ev)
    {
      CompleteScenario(ev.ScenarioOutcome, ev.ScenarioIdentity);
    }

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    /// <param name="featureId">The uniquely identifying name for the feature.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void BeginNewScenario(string idName,
                                            string friendlyName,
                                            string featureName,
                                            string featureId,
                                            Guid scenarioIdentity) {}

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="outcome"><c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    /// <param name="scenarioIdentity">The screenplay scenario identity.</param>
    protected virtual void CompleteScenario(bool? outcome, Guid scenarioIdentity) {}

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
      BeginNewTestRun();
    }

    void OnEndTestRun(object sender, EventArgs ev)
    {
      CompleteTestRun();
    }

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    protected virtual void BeginNewTestRun() {}

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    protected virtual void CompleteTestRun() {}

    #endregion
  }
}
