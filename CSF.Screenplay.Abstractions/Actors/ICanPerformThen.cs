using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// An actor which is able to perform in the <see cref="PerformancePhase.Then"/> phase of a <see cref="IPerformance"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is conceptually identical to <see cref="ICanPerform"/>, except that the nomenclature of its
    /// methods is written in the future tense, as is best pratice for the Then phase of a performance.
    /// </para>
    /// </remarks>
    /// <seealso cref="ICanPerform"/>
    public interface ICanPerformThen
    {
        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which completes when the performable is complete</returns>
        ValueTask Should(IPerformable performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        ValueTask<object> Should(IPerformableWithResult performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        ValueTask<T> Should<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken = default);
    }
}