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
  }
}
