using System.IO;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A builder for customising the <see cref="SaveAStreamAsAnAsset"/> action, specifying the asset filename.
    /// </summary>
    public class SaveStreamAsAnAssetFilenameBuilder
    {
        readonly Stream stream;

        /// <summary>
        /// Gets a builder which may be used as a performable, or which may further customise the Action,
        /// having specified the filename for the asset.
        /// </summary>
        /// <param name="filename">The filename of the asset to create, including its extension, but without any path/dirctory information.</param>
        /// <returns>A builder.</returns>
        public SaveStreamAsAnAssetBuilder AsAnAssetWithTheFilename(string filename)
            => new SaveStreamAsAnAssetBuilder(stream, filename);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStreamAsAnAssetFilenameBuilder"/> class.
        /// </summary>
        /// <param name="stream">The stream which is to be saved.</param>
        public SaveStreamAsAnAssetFilenameBuilder(Stream stream)
        {
            this.stream = stream ?? throw new System.ArgumentNullException(nameof(stream));
        }
    }

    /// <summary>
    /// A builder for customising the <see cref="SaveAStreamAsAnAsset"/> action.
    /// </summary>
    public class SaveStreamAsAnAssetBuilder : IGetsPerformable
    {
        readonly Stream stream;
        readonly string filename;

        /// <summary>
        /// Gets a performable action, specifying a short human-readable summary of the asset.
        /// </summary>
        /// <param name="summary">A brief human-readable summary of the asset, which will not be used as a filename.</param>
        /// <returns>A performable action</returns>
        public SaveAStreamAsAnAsset WithTheSummary(string summary)
            => new SaveAStreamAsAnAsset(stream, filename, summary);

        IPerformable IGetsPerformable.GetPerformable()
            => new SaveAStreamAsAnAsset(stream, filename);

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileAsAnAssetBuilder"/> class.
        /// </summary>
        /// <param name="stream">The stream which is to be saved.</param>
        /// <param name="filename">The filename of the asset to create, including its extension, but without any path/dirctory information.</param>
        public SaveStreamAsAnAssetBuilder(Stream stream, string filename)
        {
            this.stream = stream ?? throw new System.ArgumentNullException(nameof(stream));
            this.filename = filename ?? throw new System.ArgumentNullException(nameof(filename));
        }
    }
}