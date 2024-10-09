using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Model used for creating a <see cref="ReportFragment"/>, which includes the original report fragment template string,
    /// a reworked template string suitable for use with <see cref="string.Format(string, object[])"/> and the placeholder values
    /// to be inserted into that format.
    /// </summary>
    public class ReportFormat
    {
        /// <summary>
        /// Gets the original template string, which may include named placeholders.
        /// </summary>
        public string OriginalTemplate { get; }

        /// <summary>
        /// Gets a reformatted version of <see cref="OriginalTemplate"/>, which includes only numeric placeholders.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This string should be in a format which could be used by <see cref="string.Format(string, object[])"/>.
        /// </para>
        /// </remarks>
        public string FormatTemplate { get; }

        /// <summary>
        /// Gets a collection of the placeholder values which are to be inserted into the <see cref="FormatTemplate"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The value's numeric position/index in this collection corresponds to which of the numeric placeholders of
        /// <see cref="FormatTemplate"/> the value should be inserted into.
        /// The <see cref="NameAndValue.Name"/> associated with the value may be used to report upon the name of that
        /// particular value, preserving that aspect of the <see cref="OriginalTemplate"/> for informational purposes.
        /// </para>
        /// </remarks>
        public IReadOnlyList<NameAndValue> Values { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ReportFormat"/>.
        /// </summary>
        /// <param name="originalTemplate">The original template string</param>
        /// <param name="formatTemplate">The reformatted template string</param>
        /// <param name="values">The placeholder values and their names</param>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public ReportFormat(string originalTemplate, string formatTemplate, IList<NameAndValue> values)
        {
            OriginalTemplate = originalTemplate ?? throw new ArgumentNullException(nameof(originalTemplate));
            FormatTemplate = formatTemplate ?? throw new ArgumentNullException(nameof(formatTemplate));
            Values = new List<NameAndValue>(values ?? throw new ArgumentNullException(nameof(values))).AsReadOnly();
        }
    }
}
