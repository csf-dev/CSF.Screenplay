using System;

namespace CSF.Screenplay.Performances
{
    /// <summary>An object which exposes events for the beginning and completion of performances</summary>
    /// <remarks>
    /// <para>
    /// 
    /// Scenarios are the integrations which consume Screenplay performances. Typically scenarios are part of a testing framework;
    /// synonyms for a scenario include "test" or "test case" or "theory".
    /// This interface provides events which communicate the beginning and completion (success or failure) of those scenarios.
    /// </para>
    /// </remarks>
    public interface IHasPerformanceEvents
    {
        /// <summary>Occurs when a scenario starts its performance</summary>
        event EventHandler<ScenarioEventArgs> Begin;

        /// <summary>Occurs when a scenario completes its performance, regardless of whether that performance was successful or not</summary>
        event EventHandler<ScenarioCompleteEventArgs> Complete;
    }
}