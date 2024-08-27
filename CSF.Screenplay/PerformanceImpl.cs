using System;
using System.Collections.Generic;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Primary implementation type of <see cref="Performance"/>.
    /// </summary>
    public sealed class PerformanceImpl : Performance
    {
        readonly IRelaysPerformanceEvents performanceEventBus;

        /// <inheritdoc/>
        protected override void InvokePerformanceBegun()
            => performanceEventBus.InvokePerformanceBegun(PerformanceIdentity, NamingHierarchy);

        /// <inheritdoc/>
        protected override void InvokePerformanceFinished(bool? success)
            => performanceEventBus.InvokePerformanceFinished(PerformanceIdentity, NamingHierarchy, success);

        /// <inheritdoc/>
        protected override bool Equals(Performance other)
        {
            if(ReferenceEquals(other, this)) return true;
            if(ReferenceEquals(other, null)) return false;

            return Equals(PerformanceIdentity, other.PerformanceIdentity);
        }

        /// <summary>Initialises a new instance of <see cref="Performance"/></summary>
        /// <param name="serviceProvider">A dependency injection service provider</param>
        /// <param name="namingHierarchy">A collection of identifiers and names providing the hierarchical name of this
        /// performance; see <see cref="Performance.NamingHierarchy"/> for more information.</param>
        /// <param name="performanceIdentity">A unique identifier for the performance; if omitted (equal to <see cref="Guid.Empty"/>)
        /// then a new Guid will be generated as the identity for this performance</param>
        /// <exception cref="ArgumentNullException">If <paramref name="serviceProvider"/> is <see langword="null" /></exception>
        public PerformanceImpl(IServiceProvider serviceProvider,
                               IList<IdentifierAndName> namingHierarchy = default,
                               Guid performanceIdentity = default) : base(serviceProvider, namingHierarchy, performanceIdentity)
        {
            performanceEventBus = serviceProvider.GetRequiredService<IRelaysPerformanceEvents>();
        }
    }
}