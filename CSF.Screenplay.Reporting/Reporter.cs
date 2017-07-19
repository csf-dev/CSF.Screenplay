using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Base type for all reporter instances.
  /// </summary>
  public abstract class Reporter : IReporter
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

    /// <summary>
    /// Reports that an actor has gained an ability.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="ability">The ability.</param>
    public abstract void GainAbility(INamed actor, IAbility ability);

    /// <summary>
    /// Reports that a performable item has begun.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    public abstract void Begin(INamed actor, IPerformable performable);

    /// <summary>
    /// Reports that a performable item has completed successfully.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    public abstract void Success(INamed actor, IPerformable performable);

    /// <summary>
    /// Reports that a performable item has produced a result.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="result">The result produced.</param>
    public abstract void Result(INamed actor, IPerformable performable, object result);

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    public abstract void Failure(INamed actor, IPerformable performable);

    /// <summary>
    /// Reports that a performable item has failed and possible terminated early.
    /// </summary>
    /// <param name="actor">The actor.</param>
    /// <param name="performable">The performable item.</param>
    /// <param name="exception">An exception encountered whilst attempting to perform the item.</param>
    public abstract void Failure(INamed actor, IPerformable performable, Exception exception);
  }
}
