using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    public sealed partial class Actor : IHasPerformableEvents
    {
        /// <inheritdoc/>
        public event EventHandler<PerformableEventArgs> BeginPerformable;

        /// <summary>
        /// Invokes the <see cref="BeginPerformable"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        void InvokeBeginPerformable(object performable, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformableEventArgs(Name, PerformanceIdentity, performable, phase);
            BeginPerformable?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformableEventArgs> EndPerformable;

        /// <summary>
        /// Invokes the <see cref="EndPerformable"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        void InvokeEndPerformable(object performable, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformableEventArgs(Name, PerformanceIdentity, performable, phase);
            EndPerformable?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformableResultEventArgs> PerformableResult;

        /// <summary>
        /// Invokes the <see cref="PerformableResult"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="result">The result value from the performable</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        void InvokePerformableResult(object performable, object result, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformableResultEventArgs(Name, PerformanceIdentity, performable, result, phase);
            PerformableResult?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformableFailureEventArgs> PerformableFailed;

        /// <summary>
        /// Invokes the <see cref="PerformableFailed"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="exception">The exception which halted the performable</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        void InvokePerformableFailed(object performable, Exception exception, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformableFailureEventArgs(Name, PerformanceIdentity, performable, exception, phase);
            PerformableFailed?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<GainAbilityEventArgs> GainedAbility;

        /// <summary>
        /// Invokes the <see cref="GainedAbility"/> event.
        /// </summary>
        /// <param name="ability">The ability which this actor gained</param>
        void InvokeGainedAbility(object ability)
        {
            var args = new GainAbilityEventArgs(Name, PerformanceIdentity, ability);
            GainedAbility?.Invoke(this, args);
        }
    }
}