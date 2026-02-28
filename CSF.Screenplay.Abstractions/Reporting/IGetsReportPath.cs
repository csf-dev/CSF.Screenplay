namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A service which gets the path to which the Screenplay report should be written.
    /// </summary>
    public interface IGetsReportPath
    {
        /// <summary>
        /// Gets the directory path to which the report files should be written.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the returned path is <see langword="null" /> then Screenplay's reporting functionality should be disabled and no report should be written.
        /// Otherwise, implementations of this interface should return an absolute file system path to which the report should be written.
        /// This path must be writable by the executing process.
        /// </para>
        /// <para>
        /// Reporting could be disabled if either the Screenplay Options report path is <see langword="null" /> or a whitespace-only string, or if the path
        /// indicated by those options is not writable.
        /// </para>
        /// </remarks>
        /// <returns>The report path.</returns>
        string GetReportPath();
    }    
}

