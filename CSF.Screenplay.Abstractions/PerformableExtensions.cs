using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay
{
    /// <summary>Extension methods for performable types</summary>
    public static class PerformableExtensions
    {
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