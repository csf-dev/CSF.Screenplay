using System.IO;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Extension methods for <see cref="IGetsReportPath"/>.
    /// </summary>
    public static class ReportPathProviderExtensions
    {
        const string reportFilename = "ScreenplayReport.json";

        /// <summary>
        /// Gets the file path to which the report JSON file should be written.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the returned path is <see langword="null" /> then Screenplay's reporting functionality should be disabled and no report should be written.
        /// Otherwise, implementations of this interface should return an absolute file system path to which the report JSON file should be written.
        /// This path must be writable by the executing process.
        /// </para>
        /// <para>
        /// Reporting could be disabled if either the Screenplay Options report path is <see langword="null" /> or a whitespace-only string, or if the path
        /// indicated by those options is not writable.
        /// </para>
        /// </remarks>
        /// <returns>The report file path.</returns>
        public static string GetReportFilePath(this IGetsReportPath provider)
        {
            var directoryPath = provider.GetReportPath();
            if(directoryPath is null) return null;
            return Path.Combine(directoryPath, reportFilename);
        }
    }    
}
