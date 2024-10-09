using System;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Extension methods for <see cref="IGetsValueFormatter"/>.
    /// </summary>
    public static class ValueFormatterExtensions
    {
        /// <summary>
        /// Formats the specified value using an appropriate implementation of <see cref="IValueFormatter"/>, retrieved from the factory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This extension method is a convenience, equivalent to using <see cref="IGetsValueFormatter.GetValueFormatter(object)"/>,
        /// followed by <see cref="IValueFormatter.Format(object)"/>, both using <paramref name="value"/> as the parameter.
        /// </para>
        /// </remarks>
        /// <param name="formatterProvider">A value formatter factory</param>
        /// <param name="value">The value to be formatted</param>
        /// <returns>The formatted value</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="formatterProvider"/> is <see langword="null" />.</exception>
        public static string FormatValue(this IGetsValueFormatter formatterProvider, object value)
        {
            if (formatterProvider is null)
                throw new ArgumentNullException(nameof(formatterProvider));
            return formatterProvider.GetValueFormatter(value).Format(value);
        }
    }
}