using System;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which can select the most appropriate implementation of <see cref="IValueFormatter"/> from a <see cref="IFormatterRegistry"/>.
    /// </summary>
    public interface IGetsValueFormatter
    {
        /// <summary>
        /// Selects and returns an <see cref="IValueFormatter"/> which is most appropriate to the specified <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// See the remarks for <see cref="IFormatterRegistry"/> for more information about the algorithm by which an
        /// appropriate formatter is selected.
        /// </para>
        /// <para>
        /// It should be very rare for this method to raise an exception; as implementations of this type should come pre-loaded
        /// with fallback formatters which may format any value.
        /// Exceptions might only be expected if a developer removes these default formatters and does not replace them with suitable
        /// implementaton types that can cover all scenarios.
        /// </para>
        /// </remarks>
        /// <param name="value">The value to be formatted</param>
        /// <returns>A value formatter</returns>
        /// <exception cref="InvalidOperationException">If no appropriate formatter could be selected</exception>
        IValueFormatter GetValueFormatter(object value);
    }
}