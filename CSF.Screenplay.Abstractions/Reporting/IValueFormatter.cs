namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which can format a value which appears within a Screenplay report.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implement this interface in your own types in order to create a service which may externally format values for inclusion in
    /// a Screenplay report.
    /// </para>
    /// <para>
    /// A typical Screenplay solution will include many implementations of this interface, each of which is responsible for formatting a specific
    /// subset of values.  Before an implementation of this type is used to format a value, the <see cref="CanFormat"/> method should be called
    /// in order to determine whether the formatter is suitable for the value in question. This is an application of the strategy pattern:
    /// <see href="https://en.wikipedia.org/wiki/Strategy_pattern"/>.
    /// For any concrete implementation of this interface to be considered in formatting a value with a report, its type must first be registered
    /// with the <see cref="IFormatterRegistry"/>.
    /// </para>
    /// <para>
    /// The <see cref="FormatForReport"/> method should be used to create a human-readable string which represents the object in the report text.
    /// This interface is a part of the mechanism for <xref href="ReportFormattingArticle?text=formatting+values+in+reports" /> in Screenplay.
    /// </para>
    /// </remarks>
    /// <seealso cref="IHasName"/>
    /// <seealso cref="IFormattableValue"/>
    /// <seealso cref="IFormatterRegistry"/>
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
        string FormatForReport(object value);
    }
}