using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Performables
{
    /// <summary>Adapter class which allows an <see cref="IPerformableWithResult{TResult}"/> to be used as an <see cref="IPerformable"/>.</summary>
    /// <remarks>
    /// <para>
    /// This class intentionally has only an <c>internal</c> constructor, which means it may only be constructed by Screenplay framework
    /// logic. This is the extension method <see cref="PerformableExtensions.ToPerformable{TResult}(IPerformableWithResult{TResult})"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="PerformableExtensions"/>
    public class NonGenericNoResultPerformableAdapter<T> : IPerformable, ICanReport
    {
        /// <summary>Gets the wrapped <see cref="IPerformableWithResult{TResult}"/> instance.</summary>
        public IPerformableWithResult<T> PerformableWithResult { get; }

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            await PerformableWithResult.PerformAsAsync(actor, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor)
        {
            return PerformableWithResult is ICanReport reporter
                ? reporter.GetReportFragment(actor)
                : DefaultStrings.GetReport(actor, PerformableWithResult);
        }

        internal NonGenericNoResultPerformableAdapter(IPerformableWithResult<T> performableWithResult)
        {
            PerformableWithResult = performableWithResult ?? throw new System.ArgumentNullException(nameof(performableWithResult));
        }
    }
}