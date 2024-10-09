namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Model represents a single asset which was recorded from a performable.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Assets are files which are saved to disk, containing arbitrary information, recorded by a performable.
    /// This might be a screenshot, some generated content or diagnostic information.  Its real content is arbitrary and
    /// down to the implementation.
    /// An asset is described here by a file path and an optional human-readable summary.
    /// </para>
    /// </remarks>
    public class PerformableAsset
    {
        /// <summary>
        /// Gets or sets a full/absolute path to the asset file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets an optional human-readable summary of what this asset represents.  This should be one sentence at most, suitable
        /// for display in a UI tool-tip.
        /// </summary>
        public string FileSummary { get; set; }

    }
}