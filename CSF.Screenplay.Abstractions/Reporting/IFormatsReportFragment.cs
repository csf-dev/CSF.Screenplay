namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A service which produces formatted report fragments from a template and a collection of parameter values.
    /// </summary>
    public interface IFormatsReportFragment
    {
        /// <summary>
        /// Gets the formatted report fragment from the specified template and values.
        /// </summary>
        /// <param name="template">A string template for the report fragment</param>
        /// <param name="values">A collection of values associated with the report fragment</param>
        /// <returns>A formatted string</returns>
        string Format(string template, params object[] values);
    }
}