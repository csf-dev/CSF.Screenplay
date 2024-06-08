namespace CSF.Screenplay.Performances
{
    /// <summary>An object which controls the beginning and ending of a performance</summary>
    public interface IBeginsAndEndsPerformance
    {
        /// <summary>Begins the performance</summary>
        void BeginPerformance();

        /// <summary>Completes the performance with a value indicating whether or not it was a success</summary>
        /// <param name="success">If <see langword="true" /> then the performance is to be considered a success; if
        /// <see langword="false" /> then a failure. A value of <see langword="null" /> indicates that the performance did not
        /// succeed but should not be considered a failure either.</param>
        void CompletePerformance(bool? success);
    }
}