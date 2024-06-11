using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Reports;

namespace CSF.Screenplay
{
    /// <summary>Extension methods for performable types</summary>
    public static class PerformableExtensions
    {
        /// <summary>
        /// Gets an <see cref="IPerformable"/> which allows the specified performable (which returns a result) to be used
        /// in a manner which discards its result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is implemented by wrapping the performable in an adapter instance, so the object returned will not be the
        /// original performable. It is not recommended to do so, but if the original performable is required then you may
        /// cast this object to <see cref="NoResultPerformableAdapter"/> and use the
        /// <see cref="NoResultPerformableAdapter.PerformableWithResult"/> property to get the original wrapped performable instance.
        /// </para>
        /// </remarks>
        /// <param name="performableWithResult">The performable instance to be wrapped in an adapter</param>
        public static IPerformable ToPerformable(this IPerformableWithResult performableWithResult)
            => new NoResultPerformableAdapter(performableWithResult);

        /// <summary>
        /// Gets an <see cref="IPerformableWithResult"/> which allows the specified generic performable to be used
        /// non-generically.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is implemented by wrapping the performable in an adapter instance, so the object returned will not be the
        /// original performable. It is not recommended to do so, but if the original performable is required then you may
        /// cast this object to <see cref="NonGenericNoResultPerformableAdapter{TResult}"/> and use the
        /// <see cref="NonGenericNoResultPerformableAdapter{TResult}.PerformableWithResult"/> property to get the original
        /// wrapped performable instance. Of course, you will need to know &amp; use the correct generic type to do so.
        /// </para>
        /// </remarks>
        /// <param name="performableWithResult">The performable instance to be wrapped in an adapter</param>
        public static IPerformableWithResult ToNonGenericPerformableWithResult<TResult>(this IPerformableWithResult<TResult> performableWithResult)
            => new NonGenericPerformableWithResultAdapter<TResult>(performableWithResult);

        /// <summary>
        /// Gets an <see cref="IPerformable"/> which allows the specified performable (which returns a generic result) to be used
        /// non-generically and in a manner which discards its result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is implemented by wrapping the performable in an adapter instance, so the object returned will not be the
        /// original performable. It is not recommended to do so, but if the original performable is required then you may
        /// cast this object to <see cref="NonGenericPerformableWithResultAdapter{TResult}"/> and use the
        /// <see cref="NonGenericPerformableWithResultAdapter{TResult}.PerformableWithResult"/> property to get the original
        /// wrapped performable instance. Of course, you will need to know &amp; use the correct generic type to do so.
        /// </para>
        /// </remarks>
        /// <param name="performableWithResult">The performable instance to be wrapped in an adapter</param>
        public static IPerformable ToPerformable<TResult>(this IPerformableWithResult<TResult> performableWithResult)
            => new NonGenericNoResultPerformableAdapter<TResult>(performableWithResult);

        /// <summary>Gets a report fragment for the specified performable and actor</summary>
        /// <remarks>
        /// <para>
        /// If the performable item does not implement <see cref="ICanReport"/> then a default fallback report will
        /// be produced and returned.
        /// </para>
        /// </remarks>
        /// <param name="performable">The performable item</param>
        /// <param name="actor">The actor</param>
        /// <exception cref="ArgumentNullException">If <paramref name="performable"/> is <see langword="null" /></exception>
        public static string GetReport(this IPerformable performable, IHasName actor)
        {
            if (performable is null)
                throw new ArgumentNullException(nameof(performable));

            return performable is ICanReport reporter ? reporter.GetReportFragment(actor) : DefaultStrings.GetReport(actor, performable);
        }

        /// <summary>Gets a report fragment for the specified performable and actor</summary>
        /// <remarks>
        /// <para>
        /// If the performable item does not implement <see cref="ICanReport"/> then a default fallback report will
        /// be produced and returned.
        /// </para>
        /// </remarks>
        /// <param name="performable">The performable item</param>
        /// <param name="actor">The actor</param>
        /// <exception cref="ArgumentNullException">If <paramref name="performable"/> is <see langword="null" /></exception>
        public static string GetReport(this IPerformableWithResult performable, IHasName actor)
        {
            if (performable is null)
                throw new ArgumentNullException(nameof(performable));
                
            return performable is ICanReport reporter ? reporter.GetReportFragment(actor) : DefaultStrings.GetReport(actor, performable);
        }

        /// <summary>Gets a report fragment for the specified performable and actor</summary>
        /// <remarks>
        /// <para>
        /// If the performable item does not implement <see cref="ICanReport"/> then a default fallback report will
        /// be produced and returned.
        /// </para>
        /// </remarks>
        /// <param name="performable">The performable item</param>
        /// <param name="actor">The actor</param>
        /// <typeparam name="T">The result type returned by the performable</typeparam>
        /// <exception cref="ArgumentNullException">If <paramref name="performable"/> is <see langword="null" /></exception>
        public static string GetReport<T>(this IPerformableWithResult<T> performable, IHasName actor)
        {
            if (performable is null)
                throw new ArgumentNullException(nameof(performable));
                
            return performable is ICanReport reporter ? reporter.GetReportFragment(actor) : DefaultStrings.GetReport(actor, performable);
        }

        /// <summary>Adapter class which allows an <see cref="IPerformableWithResult"/> to be used as an <see cref="IPerformable"/>.</summary>
        public class NoResultPerformableAdapter : IPerformable, ICanReport
        {
            /// <summary>Gets the wrapped <see cref="IPerformableWithResult"/> instance.</summary>
            public IPerformableWithResult PerformableWithResult { get; }

            /// <inheritdoc/>
            public async Task PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
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

            internal NoResultPerformableAdapter(IPerformableWithResult performableWithResult)
            {
                PerformableWithResult = performableWithResult ?? throw new System.ArgumentNullException(nameof(performableWithResult));
            }
        }

        /// <summary>Adapter class which allows an <see cref="IPerformableWithResult{TResult}"/> to be used as an <see cref="IPerformable"/>.</summary>
        public class NonGenericNoResultPerformableAdapter<T> : IPerformable, ICanReport
        {
            /// <summary>Gets the wrapped <see cref="IPerformableWithResult{TResult}"/> instance.</summary>
            public IPerformableWithResult<T> PerformableWithResult { get; }

            /// <inheritdoc/>
            public async Task PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
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

        /// <summary>Adapter class which allows an <see cref="IPerformableWithResult{TResult}"/> to be used as an <see cref="IPerformableWithResult"/>.</summary>
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
            public async Task<object> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
            {
                return await PerformableWithResult.PerformAsAsync(actor, cancellationToken).ConfigureAwait(false);
            }

            internal NonGenericPerformableWithResultAdapter(IPerformableWithResult<T> performableWithResult)
            {
                PerformableWithResult = performableWithResult ?? throw new System.ArgumentNullException(nameof(performableWithResult));
            }
        }
    }
}