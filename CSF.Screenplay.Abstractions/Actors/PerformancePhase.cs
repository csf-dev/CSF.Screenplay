namespace CSF.Screenplay.Actors
{
    /// <summary>Enumerates the phases of a performance</summary>
    /// <remarks>
    /// <para>
    /// It is common for a <see cref="IPerformance"/> to occur in phases. This is especially true when Screenplay
    /// is being used as a testing tool.
    /// This type enumerates the possible phases.
    /// </para>
    /// <para>
    /// If phases are irrelevant to your usage of Screenplay then feel free to use <see cref="Unspecified"/>, which is the default
    /// phase when no phase has been specified.
    /// </para>
    /// </remarks>
    public enum PerformancePhase
    {
        /// <summary>Indicates that no phase has been specified</summary>
        Unspecified = 0,

        /// <summary>The 'Given' phase of a performance is for set-up and fulfilling preconditions</summary>
        /// <remarks>
        /// <para>
        /// In the "Arrange", "Act", "Assert" manner of describing test logic, this phase corresponds to "Arrange".
        /// </para>
        /// <para>
        /// In the Given phase, perform whatever tasks/actions are required to set up the test, or in other words,
        /// whatever needs to be done so that we can get to the <see cref="When"/> phase.
        /// In a test, if part of the Given performance fails, then it would be reasonable to expect that another test
        /// should also be failing.  In the context of this single test/performance, the Given phase should be just that
        /// - taken as a given.
        /// </para>
        /// </remarks>
        Given,

        /// <summary>The 'When' phase of a performance describes the activity which is under test</summary>
        /// <remarks>
        /// <para>
        /// In the "Arrange", "Act", "Assert" manner of describing test logic, this phase corresponds to "Act".
        /// </para>
        /// <para>
        /// In the When phase, the actor(s) should perform the tasks/actions that are being tested; this is the part which
        /// matters to the test.
        /// </para>
        /// </remarks>
        When,

        /// <summary>The 'Then' phase of a performance is where the results of the When phase are as expected; is it a pass or a fail?</summary>
        /// <remarks>
        /// <para>
        /// In the "Arrange", "Act", "Assert" manner of describing test logic, this phase corresponds to "Assert".
        /// </para>
        /// <para>
        /// In the Then phase, perform whatever is required to conclusively determine whether or not the activity in the <see cref="When"/>
        /// phase was a success or not. Typically this involves using performables which return result values, in order to interrogate
        /// the app state.
        /// </para>
        /// </remarks>
        Then
    }
}