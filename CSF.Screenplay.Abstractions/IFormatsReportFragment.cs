using System;

namespace CSF.Screenplay
{
    /// <summary>
    /// A service which produces formatted report fragments from a template and a collection of parameter values.
    /// </summary>
    public interface IFormatsReportFragment
    {
        /// <summary>
        /// Gets the formatted report fragment from the specified template and values.
        /// </summary>
        /// <para>
        /// This method works in a very similar fashion to <see cref="string.Format(string, object[])"/>, with a crucial
        /// difference.
        /// </para>
        /// <param name="template">A string template for the report fragment</param>
        /// <param name="values">A collection of values associated with the report fragment</param>
        /// <returns>A formatted report fragment</returns>
        ReportFragment Format(string template, params object[] values);
    }
}