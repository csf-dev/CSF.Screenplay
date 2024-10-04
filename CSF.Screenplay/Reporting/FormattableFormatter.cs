namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Implementation of <see cref="IValueFormatter"/> which formats objects that implement <see cref="IFormattableValue"/>.
    /// </summary>
    public class FormattableFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is IFormattableValue;

        /// <inheritdoc/>
        public string Format(object value) => ((IFormattableValue)value).Format();
    }
}