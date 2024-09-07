using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Primary implementation type of <see cref="IPerformance"/>.
    /// </summary>
    public sealed class Performance : IPerformance, IEquatable<Performance>
    {
        readonly IRelaysPerformanceEvents performanceEventBus;
        bool hasBegun, hasCompleted;
        bool? success;

        /// <inheritdoc/>
        public Guid PerformanceIdentity { get; }

        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public List<IdentifierAndName> NamingHierarchy { get; } = new List<IdentifierAndName>();

        /// <inheritdoc/>
        public PerformanceState PerformanceState
        {
            get {
                if (!hasBegun) return PerformanceState.NotStarted;
                if (!hasCompleted) return PerformanceState.InProgress;
                switch(success)
                {
                    case true: return PerformanceState.Success;
                    case false: return PerformanceState.Failed;
                    default: return PerformanceState.Completed;
                }
            }
        }

        /// <inheritdoc/>
        public void BeginPerformance()
        {
            if(hasBegun) throw new InvalidOperationException($"An instance of {nameof(Performance)} may be begun only once; performance instances are not reusable.");
            hasBegun = true;
            performanceEventBus.InvokePerformanceBegun(PerformanceIdentity, NamingHierarchy);
        }

        /// <inheritdoc/>
        public void FinishPerformance(bool? success)
        {
            if (!hasBegun) throw new InvalidOperationException($"An instance of {nameof(Performance)} may not be completed before it has begun.");
            if(hasCompleted) throw new InvalidOperationException($"An instance of {nameof(Performance)} may be completed only once; performance instances are not reusable.");
            hasBegun = hasCompleted = true;
            this.success = success;
            performanceEventBus.InvokePerformanceFinished(PerformanceIdentity, NamingHierarchy, success);
        }

        /// <inheritdoc/>
        public bool Equals(Performance other)
        {
            if(ReferenceEquals(other, this)) return true;
            if(ReferenceEquals(other, null)) return false;

            return Equals(PerformanceIdentity, other.PerformanceIdentity);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as Performance);

        /// <inheritdoc/>
        public override int GetHashCode() => PerformanceIdentity.GetHashCode();

        /// <inheritdoc/>
        public void Dispose()
        {
            performanceEventBus.UnsubscribeFromAllActors(PerformanceIdentity);
        }

        /// <summary>Initialises a new instance of <see cref="Performance"/></summary>
        /// <param name="serviceProvider">A dependency injection service provider</param>
        /// <param name="namingHierarchy">A collection of identifiers and names providing the hierarchical name of this
        /// performance; see <see cref="IPerformance.NamingHierarchy"/> for more information.</param>
        /// <param name="performanceIdentity">A unique identifier for the performance; if omitted (equal to <see cref="Guid.Empty"/>)
        /// then a new Guid will be generated as the identity for this performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" /></exception>
        public Performance(IServiceProvider serviceProvider,
                           IList<IdentifierAndName> namingHierarchy = default,
                           Guid performanceIdentity = default)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            NamingHierarchy = namingHierarchy?.ToList() ?? new List<IdentifierAndName>();
            PerformanceIdentity = performanceIdentity != Guid.Empty ? performanceIdentity : Guid.NewGuid();
            performanceEventBus = serviceProvider.GetRequiredService<IRelaysPerformanceEvents>();
        }
    }
}