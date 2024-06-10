﻿using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay
{
    /// <summary>
    /// An actor which may perform in the Screenplay.
    /// </summary>
    /// <seealso cref="IPerformable"/>
    /// <seealso cref="IPerformableWithResult"/>
    /// <seealso cref="IPerformableWithResult{TResult}"/>
    /// <seealso cref="IHasAbilities"/>
    public interface ICanPerform
    {
        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <returns>A task which completes when the performance is complete</returns>
        Task PerformAsync(IPerformable performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <returns>A task which exposes a result when the performance is complete</returns>
        Task<object> PerformAsync(IPerformableWithResult performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performance is complete</returns>
        Task<T> PerformAsync<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken = default);
    }
}