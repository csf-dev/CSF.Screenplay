namespace CSF.Screenplay.Performances
{
    /// <summary>Enumerates the states of a performance &amp; thus the scenario which consumes it.</summary>
    public enum ScenarioState
    {
        /// <summary>The scenario is not yet started; use <see cref="IBeginsAndEndsPerformance.BeginPerformance"/> to begin it.</summary>
        NotStarted = 0,

        /// <summary>The scenario has been started but is not yet complete; use
        /// <see cref="IBeginsAndEndsPerformance.CompletePerformance(bool?)"/> to complete it.</summary>
        InProgress,

        /// <summary>The scenario has completed and was a success.</summary>
        Success,

        /// <summary>The scenario has completed but it has failed.</summary>
        Failed,

        /// <summary>The scenario has completed but it has neither succeeded or failed.</summary>
        /// <remarks>
        /// <para>In some scenario frameworks, this might mean that the scenario was skipped or interrupted in a way that should not be treated as failure.</para>
        /// </remarks>
        Completed,
    }
}