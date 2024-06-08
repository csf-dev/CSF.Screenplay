using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A specialisation of <see cref="PerformanceEventArgs"/> which describe a failed performance,
    /// where an exception halted the performable item.
    /// </summary>
    public class PerformanceFailureEventArgs : PerformanceEventArgs
    {
        /// <summary>
        /// Gets the exception which halted the performable item.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformanceFailureEventArgs"/>.
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <param name="performable">The performable item which raised the exception</param>
        /// <param name="exception">The exception which occurred</param>
        /// <param name="phase">The phase of performance which was underway when the exception occurred</param>
        public PerformanceFailureEventArgs(ICanPerform actor,
                                           object performable,
                                           Exception exception,
                                           PerformancePhase phase = PerformancePhase.Unspecified) : base(actor, performable, phase)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }
    }
}
