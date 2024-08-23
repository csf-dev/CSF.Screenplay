using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A model for event arguments which relate to an actor's use of a performable.
    /// </summary>
    public class PerformableEventArgs : ActorEventArgs
    {
        /// <summary>
        /// Gets the performable item to which these event arguments relate.
        /// </summary>
        public object Performable { get; }

        /// <summary>
        /// Gets the performance phase to which these event arguments relate.
        /// </summary>
        public PerformancePhase Phase { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformableEventArgs"/>.
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <param name="performable">The performable item</param>
        /// <param name="phase">The phase of performance</param>
        public PerformableEventArgs(ICanPerform actor, object performable, PerformancePhase phase = PerformancePhase.Unspecified) : base(actor)
        {
            Performable = performable ?? throw new ArgumentNullException(nameof(performable));
            Phase = Enum.IsDefined(typeof(PerformancePhase), phase)
                ? phase
                : throw new ArgumentException($"The performance phase must be a valid member of {nameof(PerformancePhase)}", nameof(phase));
        }
    }
}
