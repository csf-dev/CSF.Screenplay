using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A no-op implementation of <see cref="IReporter"/> which does nothing.
    /// </summary>
    public sealed class NoOpReporter : IReporter
    {
        /// <inheritdoc/>
        public void SubscribeTo(IHasPerformanceEvents events) {}

        /// <inheritdoc/>
        public void UnsubscribeFrom(IHasPerformanceEvents events) {}

        /// <inheritdoc/>
        public void Dispose() {}
    }
}