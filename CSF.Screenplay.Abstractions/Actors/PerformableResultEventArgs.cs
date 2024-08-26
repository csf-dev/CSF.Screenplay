using System;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A specialisation of <see cref="PerformableEventArgs"/> which describe a scenario in which the performable
    /// completed and has returned a result value.
    /// </summary>
    public class PerformableResultEventArgs : PerformableEventArgs
    {
        /// <summary>
        /// Gets the result value which was returned by the performable
        /// </summary>
        public object Result { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformableResultEventArgs"/>.
        /// </summary>
        /// <param name="actorName">The actor's name</param>
        /// <param name="performanceIdentity">The actor's performance identity</param>
        /// <param name="performable">The performable item</param>
        /// <param name="result">The result from the performable</param>
        /// <param name="phase">The phase of performance</param>
        public PerformableResultEventArgs(string actorName,
                                          Guid performanceIdentity,
                                          object performable,
                                          object result,
                                          PerformancePhase phase = PerformancePhase.Unspecified) : base(actorName, performanceIdentity, performable, phase)
        {
            Result = result;
        }
    }
}
