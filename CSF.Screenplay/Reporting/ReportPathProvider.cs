using System;
using System.IO;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Stateful implementation of <see cref="IGetsReportPath"/> which caches the outcome of the path-determination logic.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class uses <see cref="ScreenplayOptions.ReportPath"/> as its primary source of truth. However, if that value is null/whitespace
    /// or if the path is not writable then this class will return <see langword="null" /> from <see cref="GetReportPath"/>, disabling the reporting
    /// functionality.
    /// </para>
    /// <para>
    /// If <see cref="ScreenplayOptions.ReportPath"/> is a relative path then it is combined with the current working directory to form an
    /// absolute path, thus (if <see cref="GetReportPath"/> does not return null), its return value will always be an absolute path.
    /// </para>
    /// <para>
    /// Because of the caching functionality, this class is stateful and should be used as a singleton.
    /// </para>
    /// </remarks>
    public class ReportPathProvider : IGetsReportPath
    {
        readonly ScreenplayOptions screenplayOptions;
        readonly ITestsPathForWritePermissions permissionsTester;

        bool hasCachedReportPath;
        string cachedReportPath;

        /// <inheritdoc/>
        public string GetReportPath()
        {
            if(!hasCachedReportPath)
            {
                cachedReportPath = ShouldEnableReporting(out var reportPath) ? reportPath : null;
                hasCachedReportPath = true;
            }

            return cachedReportPath;
        }

        /// <summary>
        /// Contains the core logic which sanitises and determines whether reporting should be enabled, and if so, what the report path should be.
        /// </summary>
        /// <param name="reportPath">Exposes the final/absolute path to the report file, if this method returns <see langword="true" />.</param>
        /// <returns><see langword="true" /> if reporting should be enabled; <see langword="false" /> if not.</returns>
        bool ShouldEnableReporting(out string reportPath)
        {
            if (string.IsNullOrWhiteSpace(screenplayOptions.ReportPath))
            {
                reportPath = null;
                return false;
            }

            reportPath = Path.IsPathRooted(screenplayOptions.ReportPath)
                ? screenplayOptions.ReportPath
                : Path.Combine(Environment.CurrentDirectory, screenplayOptions.ReportPath);
            return permissionsTester.HasWritePermission(reportPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportPathProvider"/> class.
        /// </summary>
        /// <param name="screenplayOptions">The screenplay options.</param>
        /// <param name="permissionsTester">The permissions tester.</param>
        public ReportPathProvider(ScreenplayOptions screenplayOptions, ITestsPathForWritePermissions permissionsTester)
        {
            this.screenplayOptions = screenplayOptions ?? throw new System.ArgumentNullException(nameof(screenplayOptions));
            this.permissionsTester = permissionsTester ?? throw new System.ArgumentNullException(nameof(permissionsTester));
        }
    }
}