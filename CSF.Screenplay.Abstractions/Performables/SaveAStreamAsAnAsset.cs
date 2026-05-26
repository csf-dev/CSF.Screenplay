using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A performable Action which saves a stream to an asset file.
    /// </summary>
    public class SaveAStreamAsAnAsset : IPerformable, ICanReport
    {
        readonly Stream stream;
        readonly string assetName, assetSummary;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} saves a stream as an asset file named {Name}", actor, assetName);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<GetAssetFilePaths>();
            var path = ability.GetAssetFilePath(assetName);

            using (var fileStream = File.Create(path))
            {
                await stream.CopyToAsync(fileStream, 81920, cancellationToken);
            }
            actor.RecordAsset(this, path, assetSummary);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SaveAStreamAsAnAsset"/>.
        /// </summary>
        /// <param name="stream">The stream to save.</param>
        /// <param name="assetName">The name of the asset file.</param>
        /// <param name="assetSummary">An optional human-readable summary of the asset</param>
        public SaveAStreamAsAnAsset(Stream stream, string assetName, string assetSummary = null)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                throw new ArgumentException($"'{nameof(assetName)}' cannot be null or whitespace.", nameof(assetName));
            
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            this.assetName = assetName;
            this.assetSummary = assetSummary;
        }
    }
}