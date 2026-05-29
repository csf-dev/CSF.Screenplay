using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A performable Action which saves some text an asset file.
    /// </summary>
    public class SaveTextAsAnAsset : IPerformable, ICanReport
    {
        readonly string text, assetName, assetSummary;
        readonly Encoding encoding;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} saves some text as an asset file named {Name}", actor, assetName);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<GetAssetFilePaths>();
            var path = ability.GetAssetFilePath(assetName);

            using (var fileStream = File.Create(path))
            using (var writer = new StreamWriter(fileStream, encoding))
            {
                await writer.WriteAsync(text);
            }
            actor.RecordAsset(this, path, assetSummary);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SaveTextAsAnAsset"/>.
        /// </summary>
        /// <param name="text">The text to save.</param>
        /// <param name="assetName">The name of the asset file.</param>
        /// <param name="assetSummary">An optional human-readable summary of the asset</param>
        /// <param name="encoding">The text encoding with which to save the file, if null or unspecified this defaults to UTF8.</param>
        public SaveTextAsAnAsset(string text, string assetName, string assetSummary, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                throw new ArgumentException($"'{nameof(assetName)}' cannot be null or whitespace.", nameof(assetName));
            
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            this.assetName = assetName;
            this.assetSummary = assetSummary;
            this.encoding = encoding ?? Encoding.UTF8;
        }
    }
}