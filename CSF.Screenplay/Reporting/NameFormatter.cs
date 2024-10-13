namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Implementation of <see cref="IValueFormatter"/> which formats objects that implement <see cref="IHasName"/>.
    /// </summary>
    public class NameFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is IHasName;

        /// <inheritdoc/>
        public string FormatForReport(object value) => ((IHasName)value).Name;
    }
}