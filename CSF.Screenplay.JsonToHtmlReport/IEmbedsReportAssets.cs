using System.Threading.Tasks;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.JsonToHtmlReport
{
    /// <summary>
    /// A service which reworks a <see cref="ScreenplayReport"/>, embedding assets within the report.
    /// </summary>
    public interface IEmbedsReportAssets
    {
        /// <summary>
        /// Processes the specified <see cref="ScreenplayReport"/>, converting external file assets to embedded assets,
        /// where they meet the criteria specified by the specified <see cref="ReportConverterOptions"/>.
        /// </summary>
        /// <param name="report">A Screenplay report</param>
        /// <param name="options">A set of options for converting a report to HTML</param>
        /// <returns>A task which completes when the process is done.</returns>
        Task EmbedReportAssetsAsync(ScreenplayReport report, ReportConverterOptions options);
    }
}