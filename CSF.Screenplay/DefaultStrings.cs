using System;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Reports
{
    /// <summary>
    /// An object which provides default human readable strings
    /// </summary>
    /// <remarks>
    /// <para>This is most commonly used when dealing with performables which do not implement <see cref="ICanReport"/>.</para>
    /// </remarks>
    public static class DefaultStrings
    {
        /// <summary>Gets a fallback report fragment for a performable which does not implement <see cref="ICanReport"/>.</summary>
        /// <remarks>
        /// <para>
        /// This mechanism of getting a report fragment uses a simple fallback string which is similar to:
        /// <c>{Actor} performs {Performable}</c>. The actor and performable objects will be formatted by <see cref="FormatValue(object)"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor on whom to report</param>
        /// <param name="performable">The performable object</param>
        /// <returns>The report fragment</returns>
        public static string GetReport(object actor, object performable)
            => string.Format(ReportStrings.FallbackReportFormat, FormatValue(actor), FormatValue(performable));

        /// <summary>Gets a fallback report fragment for an ability which does not implement <see cref="ICanReport"/>.</summary>
        /// <remarks>
        /// <para>
        /// This mechanism of getting a report fragment uses a simple fallback string which is similar to:
        /// <c>{Actor} is able to {Ability}</c>. The actor and ability objects will be formatted by <see cref="FormatValue(object)"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor on whom to report</param>
        /// <param name="ability">The ability object</param>
        /// <returns>The report fragment</returns>
        public static string GetAbilityReport(object actor, object ability)
            => string.Format(ReportStrings.FallbackAbilityReportFormat, FormatValue(actor), FormatValue(ability));

        /// <summary>Formats a value</summary>
        /// <remarks>
        /// <para>
        /// If the value implements <see cref="IHasName"/> then its <see cref="IHasName.Name"/> will be returned, otherwise the result
        /// of <see cref="object.ToString"/> will be returned, or an empty string if the value is <see langword="null" />.
        /// If, by any chance, there is an exception raised whilst formatting the value then a string will be returned which indicates this
        /// and contains the exception details.
        /// </para>
        /// </remarks>
        /// <param name="value">The value to format</param>
        /// <returns>The formatted value</returns>
        public static string FormatValue(object value)
        {
            try
            {
                return value is IHasName named
                    ? named.Name
                    : value?.ToString() ?? string.Empty;
            }
            catch(Exception e)
            {
                return string.Format(ReportStrings.ExceptionFormattingFormat, value.GetType(), e);
            }
        }
    }
}