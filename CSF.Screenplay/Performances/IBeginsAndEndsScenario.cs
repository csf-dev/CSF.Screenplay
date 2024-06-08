namespace CSF.Screenplay.Performances
{
    /// <summary>An object which controls the beginning and ending of a scenario</summary>
    public interface IBeginsAndEndsScenario
    {
        /// <summary>Begins the scenario</summary>
        void BeginScenario();

        /// <summary>Completes the scenario with a value indicating whether or not the scenario was a success</summary>
        /// <param name="success">If <see langword="true" /> then the scenario is to be considered a success; if
        /// <see langword="false" /> then a failure. A value of <see langword="null" /> indicates that the scenario did not
        /// succeed but should not be considered a failure either.</param>
        void CompleteScenario(bool? success);
    }
}