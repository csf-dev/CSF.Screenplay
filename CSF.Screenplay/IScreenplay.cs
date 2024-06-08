using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>An object which represents the complete Screenplay application integration.</summary>
    /// <remarks>
    /// <para>
    /// This object represents the entire Screenplay architecture, as such it is expected to be used as a singleton from
    /// within the consuming logic.  It is the entry point from which consuming logic may integrate with Screenplay.
    /// It might not be particularly visible in day-to-day use because its work is done in the architecture level of
    /// the integration.
    /// </para>
    /// <para>
    /// Integrations with, for example, testing frameworks make use of this object to create <see cref="Performance"/>
    /// instances which correspond to individual scenarios (aka "tests", "test cases", "theories" etc). The functionality
    /// of this object then coordinates other functionality in the Screenplay architecture.
    /// </para>
    /// </remarks>
    public interface IScreenplay
    {
        /// <summary>Gets the factory which should be used to create new instances of <see cref="Performance"/> within the
        /// current Screenplay.</summary>
        ICreatesPerformance PerformanceFactory { get; }

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay has begun.</summary>
        void BeginScreenplay();

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture that the
        /// Screenplay is now complete.</summary>
        void CompleteScreenplay();

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture the specified
        /// performance has begun.</summary>
        /// <param name="performance">The performance which has begun</param>
        void StartPerformance(Performance performance);

        /// <summary>Execute this method from the consuming logic in order to inform the Screenplay architecture the specified
        /// performance has has completed, and that its result should be recorded.</summary>
        /// <param name="performance">The performance which has completed</param>
        /// <param name="success">A value indicating the outcome of the performance; see
        /// <see cref="Performance.CompletePerformance(bool?)"/> for more information</param>
        void CompletePerformance(Performance performance, bool? success);
    }
}