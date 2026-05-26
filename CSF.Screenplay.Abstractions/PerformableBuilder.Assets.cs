using System.IO;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay
{
    /// <summary>
    /// Static helper class used to create/build instances of the performables which are bundled with Screenplay.
    /// </summary>
    /// <remarks>
    /// <para>Tip: Consume the members of this class via:</para>
    /// <code>using static CSF.Screenplay.PerforamableBuilder;</code>
    /// <para>
    /// This will allow you use the method names in this class in a more human-readable fashion.
    /// </para>
    /// </remarks>
    public static partial class PerforamableBuilder
    {
        /// <summary>
        /// Gets a builder for an Action which copies a file at the specified source path into the assets for the current performance.
        /// </summary>
        /// <param name="path">The path to the source file which should be copied</param>
        /// <returns>A builder to specify the name of the asset.</returns>
        public static CopyFileAsAnAssetFilenameBuilder CopyTheFile(string path)
            => new CopyFileAsAnAssetFilenameBuilder(path);

        /// <summary>
        /// Gets a builder for an Action which saves a stream of data into the assets for the current performance.
        /// </summary>
        /// <param name="stream">The stream which should be saved as an asset.</param>
        /// <returns>A builder to specify the name of the asset.</returns>
        public static SaveStreamAsAnAssetFilenameBuilder SaveTheStream(Stream stream)
            => new SaveStreamAsAnAssetFilenameBuilder(stream);
    }
}