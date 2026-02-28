using System.IO;

namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Model represents a single asset which was recorded from a performable.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Assets are typically files which are saved to disk, containing arbitrary information, recorded by a performable.
    /// This might be a screenshot, some generated content or diagnostic information.  Its real content is arbitrary and
    /// down to the implementation.
    /// </para>
    /// <para>
    /// Every asset has one or two properties in common, the <see cref="FileName"/> and optionally <see cref="FileSummary"/>.
    /// The data for the asset file is then described by one of two ways:
    /// </para>
    /// <list type="bullet">
    /// <item><description>The content of the asset is located on disk at a path indicated by <see cref="FilePath"/></description></item>
    /// <item><description>The content of the asset is embedded within this model as base64-encoded text, within <see cref="FileData"/></description></item>
    /// </list>
    /// <para>
    /// Typically when reports are first created, for expedience, assets are recorded to disk as files.
    /// However, for portability, it is often useful to embed the asset data within the report so that the entire report may be transported as a single file.
    /// </para>
    /// </remarks>
    public class PerformableAsset
    {
        /// <summary>
        /// Gets the content type (aka MIME type) of the asset.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets a full/absolute path to the asset file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets base64-encoded data which contains the data for the asset.
        /// </summary>
        public string FileData { get; set; }

        /// <summary>
        /// Gets or sets the name of the asset file, including its extension.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets an optional human-readable summary of what this asset represents.  This should be one sentence at most, suitable
        /// for display in a UI tool-tip.
        /// </summary>
        public string FileSummary { get; set; }

    }
}