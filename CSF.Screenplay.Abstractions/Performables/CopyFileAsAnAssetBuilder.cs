namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// Builder which gets performable actions related to assets.
    /// </summary>
    public class CopyFileAsAnAssetBuilder
    {
        readonly string path;

        /// <summary>
        /// Gets an Action which copies the source file into an asset of the specified name.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <returns>A performable Action.</returns>
        public CopyFileAsAnAsset AsAnAssetNamed(string name) => new CopyFileAsAnAsset(path, name);

        /// <summary>
        /// Gets a builder for an Action which copies a file at the specified source path into the assets for the current performance.
        /// </summary>
        /// <param name="path">The path to the source file which should be copied</param>
        /// <returns>A builder to specify the name of the asset.</returns>
        public static CopyFileAsAnAssetBuilder CopyTheFile(string path) => new CopyFileAsAnAssetBuilder(path);

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyFileAsAnAssetBuilder"/> class.
        /// </summary>
        /// <param name="path">The path to the source file which should be copied.</param>
        public CopyFileAsAnAssetBuilder(string path)
        {
            this.path = path ?? throw new System.ArgumentNullException(nameof(path));
        }
    }
}