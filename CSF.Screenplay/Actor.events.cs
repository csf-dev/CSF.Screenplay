using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
    public partial class Actor : IHasPerformanceEvents
    {
        /// <inheritdoc/>
        public event EventHandler<PerformanceEventArgs> BeginPerformance;

        /// <summary>
        /// Invokes the <see cref="BeginPerformance"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        protected virtual void InvokeBeginPerformance(object performable, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformanceEventArgs(this, performable, phase);
            BeginPerformance?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformanceEventArgs> EndPerformance;

        /// <summary>
        /// Invokes the <see cref="EndPerformance"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        protected virtual void InvokeEndPerformance(object performable, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformanceEventArgs(this, performable, phase);
            EndPerformance?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformanceResultEventArgs> PerformanceResult;

        /// <summary>
        /// Invokes the <see cref="PerformanceResult"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="result">The result value from the performance</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        protected virtual void InvokePerformanceResult(object performable, object result, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformanceResultEventArgs(this, performable, result, phase);
            PerformanceResult?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<PerformanceFailureEventArgs> PerformanceFailed;

        /// <summary>
        /// Invokes the <see cref="PerformanceFailed"/> event.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="exception">The exception which halted the performance</param>
        /// <param name="phase">The performance phase to which this event relates</param>
        protected virtual void InvokePerformanceFailed(object performable, Exception exception, PerformancePhase phase = PerformancePhase.Unspecified)
        {
            var args = new PerformanceFailureEventArgs(this, performable, exception, phase);
            PerformanceFailed?.Invoke(this, args);
        }

        /// <inheritdoc/>
        public event EventHandler<GainAbilityEventArgs> GainedAbility;

        /// <summary>
        /// Invokes the <see cref="GainedAbility"/> event.
        /// </summary>
        /// <param name="ability">The ability which this actor gained</param>
        protected virtual void InvokeGainedAbility(object ability)
        {
            var args = new GainAbilityEventArgs(this, ability);
            GainedAbility?.Invoke(this, args);
        }
    }
}