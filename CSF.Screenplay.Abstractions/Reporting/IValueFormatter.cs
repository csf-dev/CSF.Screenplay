namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which can format a value which appears within a Screenplay report.
    /// </summary>
    public interface IValueFormatter
    {
        /// <summary>
        /// Gets a value indicating whether this object is suitable for formatting the specified value.
        /// </summary>
        /// <param name="value">The value to be formatted</param>
        /// <returns><see langword="true" /> if this formatter is suitable for formatting the specified value; <see langword="false" /> if not.</returns>
        bool CanFormat(object value);

        /// <summary>
        /// Gets a formatted string which represents the specified value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Ensure that the <paramref name="value"/> has been tested with <see cref="CanFormat(object)"/> before executing this method.
        /// The behaviour of this method is undefined for any value for which the can-format method does not return <see langword="true" />.
        /// It may lead to exceptions or garbage output.
        /// </para>
        /// </remarks>
        /// <param name="value">The value to be formatted</param>
        /// <returns>A formatted string which represents the specified value.</returns>
        string Format(object value);
    }
}