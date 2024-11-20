using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Implementation of <see cref="IGetsAssetFilePath"/> which filename paths for assets.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Paths which are generated by this class are in the format: <c>YYYY-MM-DDTHHMMSSZ_PERFORMANCEID_ASSETNUMBER_BASENAME</c>.
    /// The first part of the filename is a timestamp, similar to ISO 8601, except that the <c>:</c> characters separating
    /// the hours, minutes and seconds are omitted. The second part is the performance identifier, equal to either the
    /// <see cref="IdentifierAndName.Identifier"/> of the last item from <see cref="Performance.NamingHierarchy"/>, or if the naming
    /// hierarchy is empty, the <see cref="Performance.PerformanceIdentity"/>. The third part is a zero-padded asset number, to
    /// differentiate between multiple assets generated during the same performance. The final part is the base name of the asset
    /// as specified by the consuming logic.
    /// </para>
    /// <para>
    /// The path returned from <see cref="GetAssetFilePath(string)"/> will be in the same directory as the report file, as returned by
    /// <see cref="IGetsReportPath.GetReportPath"/>. If the report path returned by that service is <see langword="null" /> then this
    /// method will also return <see langword="null" />, meaning that the asset file should not be written.
    /// </para>
    /// <para>
    /// This type is somewhat stateful, because it maintains an internal counter in order to provide the asset numbers (described above).
    /// It should be consumed from dependency injection as a scoped service, so that each performance has its own instance of this type.
    /// </para>
    /// </remarks>
    public class AssetPathProvider : IGetsAssetFilePath
    {
        readonly IGetsReportPath reportPathProvider;
        readonly IPerformance performance;
        int assetNumber = 1;
        
        /// <inheritdoc/>
        public string GetAssetFilePath(string baseName)
        {
            if (string.IsNullOrWhiteSpace(baseName))
                throw new ArgumentException($"'{nameof(baseName)}' cannot be null or whitespace.", nameof(baseName));
            var baseFilename = Path.GetFileName(baseName);
            if(string.IsNullOrWhiteSpace(baseFilename))
                throw new ArgumentException($"'{nameof(baseName)}' must indicate a non-whitespace filename, not a directory name.", nameof(baseName));

            var reportPath = reportPathProvider.GetReportPath();
            if(reportPath == null) return null;

            var performanceId = performance.NamingHierarchy.LastOrDefault()?.Identifier ?? performance.PerformanceIdentity.ToString();
            var sanitisedPerformanceId = RemoveInvalidFilenameChars(performanceId);
            var sanitisedBaseFilename = RemoveInvalidFilenameChars(baseFilename);
            var filename = $"{GetTimestamp()}_{sanitisedPerformanceId}_{assetNumber++:000}_{sanitisedBaseFilename}";
            return Path.Combine(Path.GetDirectoryName(reportPath), filename);
        }

        static string RemoveInvalidFilenameChars(string input)
            => Path.GetInvalidFileNameChars().Cast<string>().Aggregate(input, (current, c) => current.Replace(c, string.Empty));

        static string GetTimestamp() => DateTime.UtcNow.ToString("yyyy-MM-ddTHHmmssZ", CultureInfo.InvariantCulture);

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetPathProvider"/> class.
        /// </summary>
        /// <param name="reportPathProvider">The report path provider.</param>
        /// <param name="performance">The performance.</param>
        public AssetPathProvider(IGetsReportPath reportPathProvider, IPerformance performance)
        {
            this.reportPathProvider = reportPathProvider ?? throw new ArgumentNullException(nameof(reportPathProvider));
            this.performance = performance ?? throw new ArgumentNullException(nameof(performance));
        }
    }
}