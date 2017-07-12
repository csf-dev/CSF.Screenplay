using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// An actor is a persona who may participate in a testing scenario.
  /// Actors are given <see cref="Abilities.IAbility"/> abilities.  These abilities allow them to make use of
  /// <see cref="Performables.IPerformable"/> items, which may be tasks, actions or questions.
  /// </summary>
  public interface IActor : INamed, IPerformer, IGivenActor, IThenActor, IWhenActor, ICanReceiveAbilities, IDisposable
  {
    /// <summary>
    /// Occurs when the actor begins a performance.
    /// </summary>
    event EventHandler<BeginPerformanceEventArgs> BeginPerformance;

    /// <summary>
    /// Occurs when an actor ends a performance.
    /// </summary>
    event EventHandler<EndSuccessfulPerformanceEventArgs> EndPerformance;

    /// <summary>
    /// Occurs when an actor receives a result from a performance.
    /// </summary>
    event EventHandler<PerformanceResultEventArgs> PerformanceResult;

    /// <summary>
    /// Occurs when a performance fails with an exception.
    /// </summary>
    event EventHandler<PerformanceFailureEventArgs> PerformanceFailed;

    /// <summary>
    /// Occurs when an actor gains a new ability.
    /// </summary>
    event EventHandler<GainAbilityEventArgs> GainedAbility;

    /// <summary>
    /// Occurs when the actor begins a 'Given' task, action or question.
    /// </summary>
    event EventHandler<ActorEventArgs> BeginGiven;

    /// <summary>
    /// Occurs when the actor finishes a 'Given' task, action or question.
    /// </summary>
    event EventHandler<ActorEventArgs> EndGiven;

    /// <summary>
    /// Occurs when the actor begins a 'When' task, action or question.
    /// </summary>
    event EventHandler<ActorEventArgs> BeginWhen;

    /// <summary>
    /// Occurs when the actor finishes a 'When' task, action or question.
    /// </summary>
    event EventHandler<ActorEventArgs> EndWhen;

    /// <summary>
    /// Occurs when the actor begins a 'Then' task, action or question (usually a question).
    /// </summary>
    event EventHandler<ActorEventArgs> BeginThen;

    /// <summary>
    /// Occurs when the actor finishes a 'Then' task, action or question (usually a question).
    /// </summary>
    event EventHandler<ActorEventArgs> EndThen;
  }
}
