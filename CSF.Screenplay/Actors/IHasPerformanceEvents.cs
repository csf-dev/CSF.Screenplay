using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>An actor which may emit events as they participate in a <see cref="Performance"/></summary>
    public interface IHasPerformanceEvents
    {
        /// <summary>
        /// Occurs when the actor begins the execution of a performable object.
        /// </summary>
        event EventHandler<PerformableEventArgs> BeginPerformable;

        /// <summary>
        /// Occurs when an actor ends the execution of a performable object.
        /// </summary>
        event EventHandler<PerformableEventArgs> EndPerformable;

        /// <summary>
        /// Occurs when an actor receives a result from a perfperformable objectrmance.
        /// </summary>
        event EventHandler<PerformableResultEventArgs> PerformableResult;

        /// <summary>
        /// Occurs when a performable object fails with an exception.
        /// </summary>
        event EventHandler<PerformableFailureEventArgs> PerformableFailed;

        /// <summary>
        /// Occurs when an actor gains a new ability.
        /// </summary>
        event EventHandler<GainAbilityEventArgs> GainedAbility;
    }
}