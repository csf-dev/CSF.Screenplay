namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// A specialisation of <see cref="PerformanceEventArgs"/> which describe a successful performance,
    /// which has returned a result value.
    /// </summary>
    public class PerformanceResultEventArgs : PerformanceEventArgs
    {
        /// <summary>
        /// Gets the result value which was returned by the performable
        /// </summary>
        public object Result { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PerformanceResultEventArgs"/>.
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <param name="performable">The performable item</param>
        /// <param name="result">The result from the performable</param>
        /// <param name="phase">The phase of performance</param>
        public PerformanceResultEventArgs(ICanPerform actor,
                                          object performable,
                                          object result,
                                          PerformancePhase phase = PerformancePhase.Unspecified) : base(actor, performable, phase)
        {
            Result = result;
        }
    }
}
