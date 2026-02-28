using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;
using System.Linq;
using System.IO;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// Default implementation of <see cref="IEmbedsReportAssets"/>.
    /// </summary>
    public class AssetEmbedder : IEmbedsReportAssets
    {
        /// <inheritdoc/>
        public async Task EmbedReportAssetsAsync(ScreenplayReport report, ReportConverterOptions options)
        {
            if (report is null)
                throw new ArgumentNullException(nameof(report));
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            var applicableExtensions = options.GetEmbeddedFileExtensions();
            var ignoreExtension = options.ShouldEmbedAllFileTypes();
            var maxSizeKb = options.EmbeddedFileSizeThresholdKb;

            var allAssets = GetAssets(report);
            foreach(var asset in allAssets)
                await EmbedAssetIfApplicable(asset, maxSizeKb, applicableExtensions, ignoreExtension);
        }

        static IEnumerable<PerformableAsset> GetAssets(ScreenplayReport report)
        {
            return from performance in report.Performances
                   from performable in GetAllPerformables(performance)
                   from asset in performable.Assets
                   select asset;
        }

        static IEnumerable<PerformableReport> GetAllPerformables(PerformanceReport performance)
        {
            var open = new List<PerformableReport>();
            var closed = new List<PerformableReport>();
            open.AddRange(performance.Reportables.OfType<PerformableReport>());

            while(open.Count > 0)
            {
                var current = open.First();
                open.RemoveAt(0);
                closed.Add(current);
                open.AddRange(current.Reportables.OfType<PerformableReport>());
            }

            return closed;
        }

        static Task EmbedAssetIfApplicable(PerformableAsset asset,
                                           int maxSizeKb,
                                           IReadOnlyCollection<string> applicableExtensions,
                                           bool ignoreExtension)
        {
            if(!ShouldEmbed(asset, maxSizeKb, applicableExtensions, ignoreExtension)) return Task.CompletedTask;
            return EmbedAssetIfApplicableAsync(asset);
        }

        static bool ShouldEmbed(PerformableAsset asset,
                                int maxSizeKb,
                                IReadOnlyCollection<string> applicableExtensions,
                                bool ignoreExtension)
        {
            if(string.IsNullOrWhiteSpace(asset.FilePath)) return false;

            var info = new FileInfo(asset.FilePath);
            var fileSizeBytes = info.Length;
            var extension = info.Extension;

            return fileSizeBytes <= (maxSizeKb * 1000)
                && (ignoreExtension || applicableExtensions.Contains(extension));
        }

#if !NETSTANDARD2_0 && !NET462
        static async Task EmbedAssetIfApplicableAsync(PerformableAsset asset)
        {
            var bytes = await File.ReadAllBytesAsync(asset.FilePath).ConfigureAwait(false);
#else
        static Task EmbedAssetIfApplicableAsync(PerformableAsset asset)
        {
            var bytes = File.ReadAllBytes(asset.FilePath);
#endif

            asset.FileData = Convert.ToBase64String(bytes);
            asset.FilePath = null;
#if NETSTANDARD2_0 || NET462
            return Task.CompletedTask;
#endif
        }
    }
}