using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Performances
{
    /// <summary>A factory service for instances of <see cref="Performance"/></summary>
    public class PerformanceFactory : ICreatesPerformance
    {
        readonly IServiceScopeFactory scopeFactory;

        /// <inheritdoc/>
        public Performance CreatePerformance(IList<IdentifierAndName> scenarioHierarchy = null)
        {
            var diScope = scopeFactory.CreateScope();
            var performanceId = Guid.NewGuid();

            return new Performance(diScope.ServiceProvider, scenarioHierarchy, performanceId);
        }

        /// <summary>Initialises a new instance of <see cref="PerformanceFactory"/></summary>
        /// <param name="scopeFactory">A dependency injection scope creator</param>
        /// <exception cref="ArgumentNullException">If <paramref name="scopeFactory"/> is <see langword="null" /></exception>
        public PerformanceFactory(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }
    }
}