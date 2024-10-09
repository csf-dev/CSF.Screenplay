using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which can get a <see cref="ReportFormat"/> from a report format template and the associated values.
    /// </summary>
    public interface IGetsReportFormat
    {
        /// <summary>
        /// Gets a <see cref="ReportFormat"/> instance from the specified template and collection of values.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <paramref name="template"/> is intended to be a human-readable string, with value placeholders enclosed within braces.
        /// The <paramref name="values"/> is a collection of data values to be used as the 'filler' for those placeholders.
        /// The order in which the values are provided is important, as the placeholders will be filled in the order in which they
        /// appear in the template string. Note that duplicate placeholders will receive the same value and do not require the value
        /// to be repeated in the <paramref name="values"/> collection.
        /// </para>
        /// </remarks>
        /// <param name="template">The original report format template</param>
        /// <param name="values">An ordered collection of placeholder values to be used with the template</param>
        /// <returns>An instance of <see cref="ReportFormat"/> which combines the template and the values.</returns>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        ReportFormat GetReportFormat(string template, IList<object> values);
    }
}