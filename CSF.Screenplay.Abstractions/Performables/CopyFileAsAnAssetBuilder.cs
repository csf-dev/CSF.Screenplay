namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A builder for customising the <see cref="CopyFileAsAnAsset"/> action, specifying the asset filename.
    /// </summary>
    public class CopyFileAsAnAssetFilenameBuilder
    {
        readonly string sourcePath;

        /// <summary>
        /// Gets a builder which may be used as a performable, or which may further customise the Action,
        /// having specified the filename for the asset.
        /// </summary>
        /// <param name="filename">The filename of the asset to create, including its extension, but without any path/dirctory information.</param>
        /// <returns>A builder.</returns>
        public CopyFileAsAnAssetBuilder AsAnAssetWithTheFilename(string filename)
            => new CopyFileAsAnAssetBuilder(sourcePath, filename);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileAsAnAssetFilenameBuilder"/> class.
        /// </summary>
        /// <param name="sourcePath">The path to the source file which should be copied.</param>
        public CopyFileAsAnAssetFilenameBuilder(string sourcePath)
        {
            this.sourcePath = sourcePath ?? throw new System.ArgumentNullException(nameof(sourcePath));
        }
    }

    /// <summary>
    /// A builder for customising the <see cref="CopyFileAsAnAsset"/> action.
    /// </summary>
    public class CopyFileAsAnAssetBuilder : IGetsPerformable
    {
        readonly string sourcePath, filename;

        /// <summary>
        /// Gets a performable action, specifying a short human-readable summary of the asset.
        /// </summary>
        /// <param name="summary">A brief human-readable summary of the asset, which will not be used as a filename.</param>
        /// <returns>A performable action</returns>
        public CopyFileAsAnAsset WithTheSummary(string summary)
            => new CopyFileAsAnAsset(sourcePath, filename, summary);

        IPerformable IGetsPerformable.GetPerformable()
            => new CopyFileAsAnAsset(sourcePath, filename);

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileAsAnAssetBuilder"/> class.
        /// </summary>
        /// <param name="sourcePath">The path to the source file which should be copied.</param>
        /// <param name="filename">The filename of the asset to create, including its extension, but without any path/dirctory information.</param>
        public CopyFileAsAnAssetBuilder(string sourcePath, string filename)
        {
            this.sourcePath = sourcePath ?? throw new System.ArgumentNullException(nameof(sourcePath));
            this.filename = filename ?? throw new System.ArgumentNullException(nameof(filename));
        }
    }
}