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

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} saves {Name} as an asset file", actor.Name, name);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<GetAssetFilePaths>();
            var path = ability.GetAssetFilePath($"{name}.png");
            if(path == null) return default;

            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            actor.RecordAsset(this, path, name);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveScreenshot"/> class.
        /// </summary>
        /// <param name="screenshot">The screenshot to save.</param>
        /// <param name="name">The name of the screenshot file.</param>
        public SaveScreenshot(Screenshot screenshot, string name = null)
        {
            this.screenshot = screenshot ?? throw new System.ArgumentNullException(nameof(screenshot));
            this.name = name ?? "screenshot";
        }
    }
}