using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// An adapter which enables the use of <see cref="ICast"/> within an NUnit3 test, without needing to parameter-inject the instance
    /// as <c>Lazy&lt;ICast&gt;</c>.
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
    public class CastAdapter : ICast
    {
        readonly Lazy<ICast> wrappedCast;

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider => wrappedCast.Value.ServiceProvider;

        /// <inheritdoc/>
        public Guid PerformanceIdentity => wrappedCast.Value.PerformanceIdentity;

        /// <inheritdoc/>
        public Actor GetActor(string name) => wrappedCast.Value.GetActor(name);

        /// <inheritdoc/>
        public Actor GetActor(IPersona persona) => wrappedCast.Value.GetActor(persona);

        /// <inheritdoc/>
        public IReadOnlyCollection<string> GetCastList() => wrappedCast.Value.GetCastList();

        /// <summary>
        /// Creates a new instance of <see cref="CastAdapter"/> for the specified performance identity.
        /// </summary>
        /// <param name="performanceIdentity">A performance identity, corresponding to <see cref="IHasPerformanceIdentity.PerformanceIdentity"/>.</param>
        public CastAdapter(Guid performanceIdentity)
        {
            wrappedCast = new Lazy<ICast>(() => ScreenplayLocator.GetScopedPerformance(performanceIdentity).Scope.ServiceProvider.GetRequiredService<ICast>());
        }
    }
}

