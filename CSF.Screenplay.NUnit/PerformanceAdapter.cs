using System;
using System.Collections.Generic;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay
{
    /// <summary>
    /// An adapter which enables the use of <see cref="IPerformance"/> within an NUnit3 test, without needing to parameter-inject the instance
    /// as <c>Lazy&lt;IPerformance&gt;</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Due to NUnit architectural limitations, injectable parameters cannot be resolved from DI at the point the test method is built.
    /// If we were to attempt this, then the parameter value would not be associated with the correct Screenplay/DI scope (and thus Event Bus).
    /// This is due to the two-process model which NUnit uses; one process for building the test methods and another process for running the
    /// tests. By using an adapter with Lazy resolution of the real implementation, we ensure that DI resolution is deferred into the test-run
    /// process and not the test-building process.
    /// </para>
    /// </remarks>
    public class PerformanceAdapter : IPerformance
    {
        readonly Lazy<IPerformance> wrappedPerformance;

        public IReadOnlyList<IdentifierAndName> NamingHierarchy => wrappedPerformance.Value.NamingHierarchy;

        public PerformanceState PerformanceState => wrappedPerformance.Value.PerformanceState;

        public Guid PerformanceIdentity => wrappedPerformance.Value.PerformanceIdentity;

        public IServiceProvider ServiceProvider => wrappedPerformance.Value.ServiceProvider;

        public void BeginPerformance() => wrappedPerformance.Value.BeginPerformance();

        public void Dispose() => wrappedPerformance.Value.Dispose();

        public void FinishPerformance(bool? success) => wrappedPerformance.Value.FinishPerformance(success);

        /// <summary>
        /// Creates a new instance of <see cref="PerformanceAdapter"/> for the specified performance identity.
        /// </summary>
        /// <param name="performanceIdentity">A performance identity, corresponding to <see cref="IHasPerformanceIdentity.PerformanceIdentity"/>.</param>
        public PerformanceAdapter(Guid performanceIdentity)
        {
            wrappedPerformance = new Lazy<IPerformance>(() => ScreenplayLocator.GetScopedPerformance(performanceIdentity).Performance);
        }
    }
}

