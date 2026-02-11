using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which saves a Selenium <c>Screenshot</c> object to disk as an asset file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.SaveTheScreenshot"/>.
    /// Instead of this action, consider using the task <see cref="Tasks.TakeAndSaveScreenshot"/>,
    /// via either of the builder methods <see cref="PerformableBuilder.TakeAndSaveAScreenshot"/>
    /// or <see cref="PerformableBuilder.TakeAndSaveAScreenshotIfSupported"/>. Use of this action (standalone)
    /// is only required if you wish to examine or interact with the Screenshot object. If all you want to
    /// achieve is to take the screenshot and save it, the task is likely to be more convenient.
    /// </para>
    /// <para>
    /// Performing this action with a specified Screenshot object, such as one retrieved via the
    /// <see cref="Questions.TakeScreenshot"/> question, saves that Screenshot object to disk as
    /// <xref href="AssetGlossaryItem?text=an+asset+file"/>
    /// </para>
    /// <para>
    /// To perform this action, the actor <em>must have</em> the ability <see cref="GetAssetFilePaths"/>.
    /// If the actor does not have the ability then this action will throw an exception.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In this example, the action will save the screenshot with the name <c>Shopping cart items</c>, using the JPEG
    /// file format.
    /// Note that because of the internal workings of the <see cref="GetAssetFilePaths"/> mechanism, the precise file
    /// name by which the asset is saved is indeterminate. The precise details of file naming are determined by
    /// <see cref="CSF.Screenplay.Reporting.AssetPathProvider"/>.
    /// </para>
    /// <code>
    /// using OpenQA.Selenium;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Retrieved via (for example) the TakeScreenshot question
    /// readonly Screenshot screenshot;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(SaveTheScreenshot(screenshot)
    ///                                  .WithTheName("Shopping cart items")
    ///                                  .WithTheFormat(ScreenshotImageFormat.Jpeg),
    ///                              cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="Questions.TakeScreenshot"/>
    /// <seealso cref="Tasks.TakeAndSaveScreenshot"/>
    public class SaveScreenshot : IPerformable, ICanReport
    {
        readonly Screenshot screenshot;
        readonly string name;
        readonly ScreenshotImageFormat format;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
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