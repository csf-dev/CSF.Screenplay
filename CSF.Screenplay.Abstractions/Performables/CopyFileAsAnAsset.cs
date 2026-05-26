using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A performable Action which copies an existing file as an asset file.
    /// </summary>
    public class CopyFileAsAnAsset : IPerformable, ICanReport
    {
        readonly string sourceFilePath, assetName, assetSummary;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} copies {SourcePath} as an asset file named {Name}", actor, sourceFilePath, assetName);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<GetAssetFilePaths>();
            var path = ability.GetAssetFilePath(assetName);

            if(!File.Exists(sourceFilePath)) throw new FileNotFoundException($"The source file '{sourceFilePath}' must exist", sourceFilePath);
            File.Copy(sourceFilePath, path);
            actor.RecordAsset(this, path, assetSummary);

            return default;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CopyFileAsAnAsset"/>.
        /// </summary>
        /// <param name="sourceFilePath">The file system path to the file which should be copied into the assets.</param>
        /// <param name="assetName">The name of the asset file.</param>
        /// <param name="assetSummary">An optional human-readable summary of the asset</param>
        public CopyFileAsAnAsset(string sourceFilePath, string assetName, string assetSummary = null)
        {
            if (string.IsNullOrWhiteSpace(assetName))
                throw new ArgumentException($"'{nameof(assetName)}' cannot be null or whitespace.", nameof(assetName));
            
            this.sourceFilePath = sourceFilePath ?? throw new ArgumentNullException(nameof(sourceFilePath));
            this.assetName = assetName;
            this.assetSummary = assetSummary;
        }
    }
}