using System.IO;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// Builder which gets performable actions related to saving streams as assets.
    /// </summary>
    public class SaveStreamAsAnAssetBuilder
    {
        readonly Stream stream;

        /// <summary>
        /// Gets an Action which saves the contents of the stream into an asset of the specified name.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <returns>A performable Action.</returns>
        public SaveAStreamAsAnAsset AsAnAssetNamed(string name) => new SaveAStreamAsAnAsset(stream, name);

        /// <summary>
        /// Gets a builder for an Action which saves a stream of data into the assets for the current performance.
        /// </summary>
        /// <param name="stream">The stream which should be saved as an asset.</param>
        /// <returns>A builder to specify the name of the asset.</returns>
        public static SaveStreamAsAnAssetBuilder SaveTheStream(Stream stream) => new SaveStreamAsAnAssetBuilder(stream);

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStreamAsAnAssetBuilder"/> class.
        /// </summary>
        /// <param name="stream">The stream which should be saved as an asset.</param>
        public SaveStreamAsAnAssetBuilder(Stream stream)
        {
            this.stream = stream ?? throw new System.ArgumentNullException(nameof(stream));
        }
    }
}