using System;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// A model which contains both an <see cref="IPerformance"/> and a dependency injection <see cref="IServiceScope"/>.
    /// </summary>
    public sealed class ScopeAndPerformance : IDisposable
    {
        /// <summary>
        /// Gets the performance.
        /// </summary>
        public IPerformance Performance { get; }

        /// <summary>
        /// Gets the DI scope.
        /// </summary>
        public IServiceScope Scope { get; }

        /// <inheritdoc/>
        public void Dispose() => Scope.Dispose();
        
        /// <summary>
        /// Initialises a new instance of <see cref="ScopeAndPerformance"/>.
        /// </summary>
        /// <param name="performance">The performance</param>
        /// <param name="scope">The scope</param>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public ScopeAndPerformance(IPerformance performance, IServiceScope scope)
        {
            Performance = performance ?? throw new ArgumentNullException(nameof(performance));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}