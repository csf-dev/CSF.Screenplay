using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay
{
    /// <summary>Adapter class which allows an <see cref="IPerformableWithResult{TResult}"/> to be used as an <see cref="IPerformableWithResult"/>.</summary>
    /// <remarks>
    /// <para>
    /// This class intentionally has only an <c>internal</c> constructor, which means it may only be constructed by Screenplay framework
    /// logic. This is the extension method <see cref="PerformableExtensions.ToNonGenericPerformableWithResult{TResult}(IPerformableWithResult{TResult})"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="PerformableExtensions"/>
    public class NonGenericPerformableWithResultAdapter<T> : IPerformableWithResult, ICanReport
    {
        /// <summary>Gets the wrapped <see cref="IPerformableWithResult{TResult}"/> instance.</summary>
        public IPerformableWithResult<T> PerformableWithResult { get; }

        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor)
        {
            return PerformableWithResult is ICanReport reporter
                ? reporter.GetReportFragment(actor)
                : DefaultStrings.GetReport(actor, PerformableWithResult);
        }

        /// <inheritdoc/>
        public async ValueTask<object> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            return await PerformableWithResult.PerformAsAsync(actor, cancellationToken).ConfigureAwait(false);
        }

        internal NonGenericPerformableWithResultAdapter(IPerformableWithResult<T> performableWithResult)
        {
            PerformableWithResult = performableWithResult ?? throw new System.ArgumentNullException(nameof(performableWithResult));
        }
    }
}