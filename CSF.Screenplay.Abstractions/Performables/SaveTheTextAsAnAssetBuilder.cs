using System.Text;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// Builder which gets performable actions related to saving text as assets.
    /// </summary>
    public class SaveTheTextAsAnAssetBuilder : IGetsPerformable
    {
        readonly string text;
        string name;

        /// <summary>
        /// Picks the name of the asset by which to save the text.  Use of this method is mandatory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <see cref="UsingTheEncoding"/> is not used, then the created action will default to using UTF8 encoding.
        /// </para>
        /// </remarks>
        /// <param name="name">The name of the asset.</param>
        /// <returns>A builder which implements <see cref="IGetsPerformable"/>.</returns>
        public SaveTheTextAsAnAssetBuilder AsAnAssetNamed(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Gets a performable Action which saves the text using a specified encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use</param>
        /// <returns>A performable Action.</returns>
        public SaveTextAsAnAsset UsingTheEncoding(Encoding encoding) => new SaveTextAsAnAsset(text, name, encoding);

        /// <summary>
        /// Gets a builder for an Action which saves a stream of data into the assets for the current performance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// At the minimum, the consumer must then use <see cref="AsAnAssetNamed(string)"/>, in order to get a usable performable.
        /// Use of <see cref="UsingTheEncoding(Encoding)"/> is optional, as the created Action will default to UTF8 if no encoding
        /// is specified.
        /// </para>
        /// </remarks>
        /// <param name="text">The text which should be saved as an asset.</param>
        /// <returns>A builder to specify the name of the asset.</returns>
        public static SaveTheTextAsAnAssetBuilder SaveTheText(string text) => new SaveTheTextAsAnAssetBuilder(text);

        IPerformable IGetsPerformable.GetPerformable() => new SaveTextAsAnAsset(text, name);

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveTheTextAsAnAssetBuilder"/> class.
        /// </summary>
        /// <param name="text">The text which should be saved as an asset.</param>
        public SaveTheTextAsAnAssetBuilder(string text)
        {
            this.text = text ?? throw new System.ArgumentNullException(nameof(text));
        }
    }
}