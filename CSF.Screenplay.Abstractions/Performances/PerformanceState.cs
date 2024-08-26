namespace CSF.Screenplay.Performances
{
    /// <summary>Enumerates the states of a <see cref="Performance"/>.</summary>
    /// <remarks>
    /// <para>
    /// When Screenplay is being used with <xref href="IntegrationGlossaryItem?an+automated+testing+integration"/> then
    /// this will also closely correspond to the state &amp; outcome of <xref href="ScenarioGlossaryItem?text=the+corresponding+Scenario"/>
    /// </para>
    /// </remarks>
    public enum PerformanceState
    {
        /// <summary>The performance is not yet started; use <see cref="IBeginsAndEndsPerformance.BeginPerformance"/> to begin it.</summary>
        NotStarted = 0,

        /// <summary>The performance has been started but is not yet complete; use
        /// <see cref="IBeginsAndEndsPerformance.FinishPerformance(bool?)"/> to complete it.</summary>
        InProgress,

        /// <summary>The performance has completed and was a success.</summary>
        Success,

        /// <summary>The performance has completed but it has failed.</summary>
        Failed,

        /// <summary>The performance has completed but it has neither succeeded or failed.</summary>
        /// <remarks>
        /// <para>
        /// In some <xref href="IntegrationGlossaryItem?automated+testing+integrations"/>, this might mean that the
        /// <see cref="Performance"/> or <xref href="ScenarioGlossaryItem?text=Scenario"/> was skipped or interrupted in
        /// a way that should not be treated as a failure.
        /// </para>
        /// </remarks>
        Completed,
    }
}