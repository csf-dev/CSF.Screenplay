using System;
using CSF.Screenplay.Performables;

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
        public static string GetReportFragment(this IPerformable performable, IHasName actor)
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
        public static string GetReportFragment(this IPerformableWithResult performable, IHasName actor)
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
        public static string GetReportFragment<T>(this IPerformableWithResult<T> performable, IHasName actor)
        {
            if (performable is null)
                throw new ArgumentNullException(nameof(performable));
                
            return performable is ICanReport reporter ? reporter.GetReportFragment(actor) : DefaultStrings.GetReport(actor, performable);
        }
    }
}