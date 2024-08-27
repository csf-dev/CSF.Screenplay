using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A specialisation of <see cref="PerformableEventArgs"/> which describe the situation where
    /// an exception halted the execution of the performable item.
    /// </summary>
    public class PerformableFailureEventArgs : PerformableEventArgs
    {
        /// <summary>
        /// Gets the exception which halted the performable item.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformableFailureEventArgs"/>.
        /// </summary>
        /// <param name="actorName">The actor's name</param>
        /// <param name="performanceIdentity">The actor's performance identity</param>
        /// <param name="performable">The performable item which raised the exception</param>
        /// <param name="exception">The exception which occurred</param>
        /// <param name="phase">The phase of performance which was underway when the exception occurred</param>
        public PerformableFailureEventArgs(string actorName,
                                           Guid performanceIdentity,
                                           object performable,
                                           Exception exception,
                                           PerformancePhase phase = PerformancePhase.Unspecified) : base(actorName, performanceIdentity, performable, phase)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }
    }
}
