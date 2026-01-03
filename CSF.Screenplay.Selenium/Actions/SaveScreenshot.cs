using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Screenplay action which saves a Selenium Screenshot instance as an asset file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use of this performable requires the actor to have the ability <see cref="GetAssetFilePaths"/>.
    /// </para>
    /// </remarks>
    public class SaveScreenshot : IPerformable, ICanReport
    {
        readonly Screenshot screenshot;
        readonly string name;
        readonly ScreenshotImageFormat format;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} saves {Name} as an asset file", actor.Name, name);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<GetAssetFilePaths>();
            var path = ability.GetAssetFilePath($"{name}.{format.ToString().ToLowerInvariant()}");
            if(path == null) return default;

            screenshot.SaveAsFile(path, format);
            actor.RecordAsset(this, path, name);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveScreenshot"/> class.
        /// </summary>
        /// <param name="screenshot">The screenshot to save.</param>
        /// <param name="name">A human-readable name for the screenshot, which identifies it within the performance.</param>
        /// <param name="format">An optional format in which to save the screenshot; if omitted then PNG will be used by default.</param>
        public SaveScreenshot(Screenshot screenshot, string name = null, ScreenshotImageFormat format = ScreenshotImageFormat.Png)
        {
            this.screenshot = screenshot ?? throw new ArgumentNullException(nameof(screenshot));
            this.format = format;
            this.name = name ?? "screenshot";
        }
    }
}