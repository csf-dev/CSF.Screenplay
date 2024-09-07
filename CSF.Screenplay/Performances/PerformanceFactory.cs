using System;

namespace CSF.Screenplay.Performances
{
    /// <summary>A factory service for instances of <see cref="Performance"/></summary>
    public class PerformanceFactory : ICreatesPerformance
    {
        readonly IServiceProvider services;

        /// <inheritdoc/>
        public IPerformance CreatePerformance() => new Performance(services, performanceIdentity: Guid.NewGuid());

        /// <summary>Initialises a new instance of <see cref="PerformanceFactory"/></summary>
        /// <param name="services">Dependency injection services</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" /></exception>
        public PerformanceFactory(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}