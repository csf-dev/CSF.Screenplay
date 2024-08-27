using System;
using System.Collections.Generic;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// An object which has events that 
    /// </summary>
    public interface IHasPerformanceEvents
    {
        /// <summary>
        /// Occurs when an <see cref="Actor"/> begins the execution of a performable object.
        /// </summary>
        event EventHandler<PerformableEventArgs> BeginPerformable;

        /// <summary>
        /// Occurs when an <see cref="Actor"/> ends the execution of a performable object.
        /// </summary>
        event EventHandler<PerformableEventArgs> EndPerformable;

        /// <summary>
        /// Occurs when an <see cref="Actor"/> receives a result from a perfperformable objectrmance.
        /// </summary>
        event EventHandler<PerformableResultEventArgs> PerformableResult;

        /// <summary>
        /// Occurs when a performable object fails with an exception.
        /// </summary>
        event EventHandler<PerformableFailureEventArgs> PerformableFailed;

        /// <summary>
        /// Occurs when an <see cref="Actor"/> gains a new ability.
        /// </summary>
        event EventHandler<GainAbilityEventArgs> GainedAbility;

        /// <summary>
        /// Occurs when a <see cref="IPerformance"/> begins executing.
        /// </summary>
        event EventHandler<PerformanceEventArgs> PerformanceBegun;

        /// <summary>
        /// Occurs when a <see cref="IPerformance"/> has finished executing.
        /// </summary>
        event EventHandler<PerformanceFinishedEventArgs> PerformanceFinished;
    }
}