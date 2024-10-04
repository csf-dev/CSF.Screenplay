using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Implementation of <see cref="IValueFormatter"/> which formats any object by using its default <see cref="object.ToString()"/> method,
    /// or returns the string <c>&lt;null&gt;</c> if the value is <see langword="null" />.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This formatter should be used as a default/last resort.  It is very likely that this could produce results which are not particularly human readable.
    /// </para>
    /// </remarks>
    public class ToStringFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is IFormattableValue;

        /// <inheritdoc/>
        public string Format(object value) => value is null ? FormatterStrings.NullValue : value.ToString();
    }
}