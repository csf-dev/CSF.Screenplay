using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.SeleniumPerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// Screenplay task which represents an actor taking a screenshot of the current web page and saving it as a report asset.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable task is a composition of two others: <see cref="Questions.TakeScreenshot"/> and <see cref="Actions.SaveScreenshot"/>.
    /// It offers an optional name for the screenshot, which is used as part of its asset filename if specified.
    /// </para>
    /// <para>
    /// As with <see cref="Actions.SaveScreenshot"/>, this performable requires the actor to have the ability <see cref="Abilities.GetAssetFilePaths"/>.
    /// </para>
    /// </remarks>
    public class TakeAndSaveScreenshot : IPerformable, ICanReport
    {
        readonly string name;
        readonly ScreenshotImageFormat format;
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} takes a screenshot in {Format} format and saves it as an asset named {Name}", actor.Name, format, name);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var screenshot = await actor.PerformAsync(GetTakeAScreenshotPerformable(), cancellationToken);
            if(screenshot == null) return;
            await actor.PerformAsync(SaveTheScreenshot(screenshot).WithTheName(name).WithTheFormat(format), cancellationToken);
        }

        IPerformableWithResult<Screenshot> GetTakeAScreenshotPerformable()
            => throwIfUnsupported ? TakeAScreenshot() : TakeAScreenshotIfSupported();

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeAndSaveScreenshot"/> class.
        /// </summary>
        /// <param name="name">An optional human-readable name for the screenshot.</param>
        /// <param name="format">An optional image format by which to save the screenshot.</param>
        /// <param name="throwIfUnsupported">If set to <c>true</c>, throws an exception if the WebDriver does not support taking screenshots.</param>
        public TakeAndSaveScreenshot(string name = null, ScreenshotImageFormat format = ScreenshotImageFormat.Png, bool throwIfUnsupported = true)
        {
            this.name = name ?? "screenshot";
            this.format = format;
            this.throwIfUnsupported = throwIfUnsupported;
        }
    }
}