using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Actors
{
    /// <summary>
    /// An actor which is able to perform in the <see cref="PerformancePhase.Given"/> phase of a performance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface is conceptually identical to <see cref="ICanPerform"/>, except that the nomenclature of its
    /// methods is written in the past tense, as is best pratice for the Given phase of a performance.
    /// </para>
    /// </remarks>
    /// <seealso cref="ICanPerform"/>
    public interface ICanPerformGiven
    {
        /// <summary>
        /// Performs an action or task which returns no result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <returns>A task which completes when the performance is complete</returns>
        Task WasAbleTo(IPerformable performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an action or task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <returns>A task which exposes a result when the performance is complete</returns>
        Task<object> WasAbleTo(IPerformableWithResult performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an action or task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performance</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performance is complete</returns>
        Task<T> WasAbleTo<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken = default);
    }
}