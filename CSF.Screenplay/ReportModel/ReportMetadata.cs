using System;

namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Model represents the metadata about a Screenplay report.
    /// </summary>
    public class ReportMetadata
    {
        const string reportFormatVersion = "2.0.0";

        /// <summary>
        /// Gets or sets the date &amp; time at which the Screenplay began.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Other time-related values within the Screenplay report are expressed relative to this time.
        /// </para>
        /// </remarks>
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Gets or sets a version number for the format of report that has been produced.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This version number is intended to comply with Semantic versioning: <see href="https://semver.org/spec/v2.0.0.html"/>.
        /// It may be used to assist parsing logic determine whether or not it is reading a compatible report file.
        /// </para>
        /// </remarks>
        public string ReportFormatVersion { get; set; } = reportFormatVersion;
    }
}