using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A Screenplay task which combines the taking of a screenshot of the current web browser viewport and saving it
    /// as an asset file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this task via one of the builder methods <see cref="TakeAndSaveAScreenshot"/> or <see cref="TakeAndSaveAScreenshotIfSupported"/>.
    /// This performable task is a composition of two others: <see cref="Questions.TakeScreenshot"/> and <see cref="Actions.SaveScreenshot"/>.
    /// See the documentation for this question and action for more information.
    /// </para>
    /// <para>
    /// As with <see cref="Actions.SaveScreenshot"/>, this performable requires the actor to have the ability <see cref="Abilities.GetAssetFilePaths"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// using OpenQA.Selenium;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(TakeAndSaveAScreenshot().WithTheName("Shopping cart items"), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="TakeAndSaveAScreenshot"/>
    /// <seealso cref="TakeAndSaveAScreenshotIfSupported"/>
    /// <seealso cref="Questions.TakeScreenshot"/>
    /// <seealso cref="Actions.SaveScreenshot"/>
    /// <seealso cref="Abilities.GetAssetFilePaths"/>
    public class TakeAndSaveScreenshot : IPerformable, ICanReport
    {
        readonly string name;
        readonly ScreenshotImageFormat format;
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
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