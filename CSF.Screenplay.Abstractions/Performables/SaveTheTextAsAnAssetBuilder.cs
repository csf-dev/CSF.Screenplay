using System.Text;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A builder for customising the <see cref="SaveAStreamAsAnAsset"/> action, specifying the asset filename.
    /// </summary>
    public class SaveTheTextAsAnAssetFilenameBuilder
    {
        readonly string text;

        /// <summary>
        /// Gets a builder which may be used as a performable, or which may further customise the Action,
        /// having specified the filename for the asset.
        /// </summary>
        /// <param name="filename">The filename of the asset to create, including its extension, but without any path/dirctory information.</param>
        /// <returns>A builder.</returns>
        public SaveTheTextAsAnAssetBuilder AsAnAssetWithTheFilename(string filename)
            => new SaveTheTextAsAnAssetBuilder(text, filename);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveTheTextAsAnAssetFilenameBuilder"/> class.
        /// </summary>
        /// <param name="text">The text which is to be saved.</param>
        public SaveTheTextAsAnAssetFilenameBuilder(string text)
        {
            this.text = text ?? throw new System.ArgumentNullException(nameof(text));
        }
    }

    /// <summary>
    /// A builder for customising the <see cref="SaveAStreamAsAnAsset"/> action.
    /// </summary>
    public class SaveTheTextAsAnAssetBuilder : IGetsPerformable
    {
        readonly string text;
        readonly string filename;
        string summary;
        Encoding encoding;

        /// <summary>
        /// Specifies a human-readable summary for the asset.
        /// </summary>
        /// <param name="summary">A brief human-readable summary of the asset, which will not be used as a filename.</param>
        /// <returns>A performable action</returns>
        public SaveTheTextAsAnAssetBuilder WithTheSummary(string summary)
        {
            this.summary = summary;
            return this;
        }

        /// <summary>
        /// Picks the text encoding with which to save the text.
        /// </summary>
        /// <param name="encoding">The encoding to use</param>
        /// <returns>A performable Action.</returns>
        public SaveTheTextAsAnAssetBuilder UsingTheEncoding(Encoding encoding)
        {
            this.encoding = encoding;
            return this;
        }

        IPerformable IGetsPerformable.GetPerformable() => new SaveTextAsAnAsset(text, filename, summary, encoding);

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileAsAnAssetBuilder"/> class.
        /// </summary>
        /// <param name="text">The text which is to be saved.</param>
        /// <param name="filename">The filename of the asset to create, including its extension, but without any path/dirctory information.</param>
        public SaveTheTextAsAnAssetBuilder(string text, string filename)
        {
            this.text = text ?? throw new System.ArgumentNullException(nameof(text));
            this.filename = filename ?? throw new System.ArgumentNullException(nameof(filename));
        }
    }
}