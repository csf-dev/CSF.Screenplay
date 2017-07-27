using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// A reporter which does nothing.  This is a suitable base type for reporter implementations.
  /// </summary>
  public class NoOpReporter : IReporter
  {
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
      GainAbility(ev.Actor, ev.Ability);
    }

    void OnBegin(object sender, BeginPerformanceEventArgs ev)
    {
      Begin(ev.Actor, ev.Performable);
    }

    void OnSuccess(object sender, EndSuccessfulPerformanceEventArgs ev)
    {
      Success(ev.Actor, ev.Performable);
    }

    void OnResult(object sender, PerformanceResultEventArgs ev)
    {
      Result(ev.Actor, ev.Performable, ev.Result);
    }

    void OnFailure(object sender, PerformanceFailureEventArgs ev)
    {
      Failure(ev.Actor, ev.Performable, ev.Exception);
    }

    void OnBeginGiven(object sender, ActorEventArgs ev)
    {
      BeginGiven(ev.Actor);
    }

    void OnEndGiven(object sender, ActorEventArgs ev)
    {
      EndGiven(ev.Actor);
    }

    void OnBeginWhen(object sender, ActorEventArgs ev)
    {
      BeginWhen(ev.Actor);
    }

    void OnEndWhen(object sender, ActorEventArgs ev)
    {
      EndWhen(ev.Actor);
    }

    void OnBeginThen(object sender, ActorEventArgs ev)
    {
      BeginThen(ev.Actor);
    }

    void OnEndThen(object sender, ActorEventArgs ev)
    {
      EndThen(ev.Actor);
    }

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    protected virtual void GainAbility(INamed actor, IAbility ability) {}

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected virtual void Begin(INamed actor, IPerformable performable) {}

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    protected virtual void Success(INamed actor, IPerformable performable) {}

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    protected virtual void Result(INamed actor, IPerformable performable, object result) {}

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    protected virtual void Failure(INamed actor, IPerformable performable, Exception exception) {}

    /// <summary>
    /// Reports that an actor has begun a 'given' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected virtual void BeginGiven(INamed actor) {}

    /// <summary>
    /// Reports that an actor has ended the 'given' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected virtual void EndGiven(INamed actor) {}

    /// <summary>
    /// Reports that an actor has begun a 'when' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected virtual void BeginWhen(INamed actor) {}

    /// <summary>
    /// Reports that an actor has ended the 'when' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected virtual void EndWhen(INamed actor) {}

    /// <summary>
    /// Reports that an actor has begun a 'then' part of their performance and that subsequent performables occur
    /// in this context.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected virtual void BeginThen(INamed actor) {}

    /// <summary>
    /// Reports that an actor has ended the 'then' part of their performance.
    /// </summary>
    /// <param name="actor">The actor.</param>
    protected virtual void EndThen(INamed actor) {}

    /// <summary>
    /// Indicates to the reporter that a new test-run has begun.
    /// </summary>
    public virtual void BeginNewTestRun() {}

    /// <summary>
    /// Indicates to the reporter that the test run has completed and that the report should be finalised.
    /// </summary>
    public virtual void CompleteTestRun() {}

    /// <summary>
    /// Indicates to the reporter that a new scenario has begun.
    /// </summary>
    /// <param name="friendlyName">The friendly scenario name.</param>
    /// <param name="featureName">The feature name.</param>
    /// <param name="idName">The uniquely identifying name for the test.</param>
    public virtual void BeginNewScenario(string idName, string friendlyName, string featureName) {}

    /// <summary>
    /// Indicates to the reporter that a scenario has finished.
    /// </summary>
    /// <param name="success"><c>true</c> if the scenario was a success; <c>false</c> otherwise.</param>
    public virtual void CompleteScenario(bool success) {}
  }
}
