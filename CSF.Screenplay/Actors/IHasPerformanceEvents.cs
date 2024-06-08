using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>An actor which may emit events as they participate in a performance</summary>
    public interface IHasPerformanceEvents
    {
        /// <summary>
        /// Occurs when the actor begins a performance.
        /// </summary>
        event EventHandler<PerformanceEventArgs> BeginPerformance;

        /// <summary>
        /// Occurs when an actor ends a performance.
        /// </summary>
        event EventHandler<PerformanceEventArgs> EndPerformance;

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