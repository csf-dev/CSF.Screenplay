using System;
using System.Collections.Generic;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// An adapter which enables the use of <see cref="IStage"/> within an NUnit3 test, without needing to parameter-inject the instance
    /// as <c>Lazy&lt;IStage&gt;</c>.
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
    public sealed class StageAdapter : IStage
    {
        readonly Lazy<IStage> wrappedStage;

        /// <inheritdoc/>
        public ICast Cast => wrappedStage.Value.Cast;

        /// <inheritdoc/>
        public Actor GetSpotlitActor() => wrappedStage.Value.GetSpotlitActor();

        /// <inheritdoc/>
        public void Spotlight(Actor actor) => wrappedStage.Value.Spotlight(actor);

        /// <inheritdoc/>
        public Actor Spotlight(IPersona persona) => wrappedStage.Value.Spotlight(persona);

        /// <inheritdoc/>
        public Actor TurnSpotlightOff() => wrappedStage.Value.TurnSpotlightOff();

        /// <summary>
        /// Creates a new instance of <see cref="StageAdapter"/> for the specified performance identity.
        /// </summary>
        /// <param name="performanceIdentity">A performance identity, corresponding to <see cref="IHasPerformanceIdentity.PerformanceIdentity"/>.</param>
        public StageAdapter(Guid performanceIdentity)
        {
            wrappedStage = new Lazy<IStage>(() => ScreenplayLocator.GetScopedPerformance(performanceIdentity).Scope.ServiceProvider.GetRequiredService<IStage>());
        }
    }
}

