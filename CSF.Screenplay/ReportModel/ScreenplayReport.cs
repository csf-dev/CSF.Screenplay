using System.Collections.Generic;

namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Represents a complete Screenplay report.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is not used in the writing of Screenplay reports; the <see cref="Reporting.JsonScreenplayReporter"/> writes the report by streaming
    /// it to a file as it is generated.  This type is used only for the deserialization of a report file.
    /// </para>
    /// </remarks>
    public class ScreenplayReport
    {
        /// <summary>
        /// Gets or sets the metadata for the report.
        /// </summary>
        public ReportMetadata Metadata { get; set; } = new ReportMetadata();

        /// <summary>
        /// Gets or sets the performances which are included within the report.
        /// </summary>
        /// <remarks>
        /// <para>
        /// There is no defined order to the performances within the report.  The order in which they appear in this collection will correspond to the
        /// order in which they were serialized to the report file. However, because performances may occur in parallel, there is no guarantee that
        /// the order will be stable between different reports, even if the <see cref="Screenplay"/> and <see cref="IPerformance"/> instances contained
        /// are the same, with the same logic.
        /// </para>
        /// </remarks>
        public ICollection<PerformanceReport> Performances { get; set; } = new List<PerformanceReport>();
    }
}