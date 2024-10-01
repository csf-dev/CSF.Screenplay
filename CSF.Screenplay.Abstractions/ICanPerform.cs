using System.Threading;
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
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which completes when the performable is complete</returns>
        ValueTask PerformAsync(IPerformable performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs a question or question-like task which returns an untyped result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        ValueTask<object> PerformAsync(IPerformableWithResult performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs a question or question-like task which returns a strongly typed result.
        /// </summary>
        /// <param name="performable">The performable item</param>
        /// <param name="cancellationToken">An optional token to cancel the performable</param>
        /// <typeparam name="T">The result type</typeparam>
        /// <returns>A task which exposes a result when the performable is complete</returns>
        ValueTask<T> PerformAsync<T>(IPerformableWithResult<T> performable, CancellationToken cancellationToken = default);

        /// <summary>
        /// Records the existence of a new performable asset file at the specified path.
        /// </summary>
        /// <remarks>
        /// <para>
        /// File assets are sometimes created during a performance as a reporting/verification mechanism. For example a performable
        /// which controls the user interface of an application might take and save a screenshot of that UI so that a human may later
        /// verify that everything looked as it should.
        /// </para>
        /// <para>
        /// Alternatively, file assets might constitute part of the output of a performance.  Imagine an application of Screenplay which
        /// captures video from a security camera; that video file would be an asset.
        /// </para>
        /// <para>
        /// This method may be used from performables which generate and save asset files.
        /// They ensure that appropriate events are called and passed 'upward' through the Screenplay architecture, such that
        /// subscribers may be notified.
        /// This will allow the presence and details of assets to be included in Screenplay artifacts such as reports.
        /// </para>
        /// </remarks>
        /// <param name="performable">The performable item</param>
        /// <param name="filePath">The full absolute path to the asset file</param>
        /// <param name="fileSummary">An optional human-readable summary of the asset file</param>
        void RecordAsset(object performable, string filePath, string fileSummary = null);
    }
}
