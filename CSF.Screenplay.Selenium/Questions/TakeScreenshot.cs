using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which represents an actor taking a screenshot of the current web page.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The best way to use this action is via the builder method <see cref="PerformableBuilder.TakeAScreenshot()"/>
    /// or <see cref="PerformableBuilder.TakeAScreenshotIfSupported()"/>. The second method will not raise an exception
    /// if the browser is incapable of providing screenshots.
    /// </para>
    /// <para>
    /// This question instructs the WebDriver to capture the currently-visible area of the browser window (the viewport)
    /// in a screenshot, and return that screenshot to the <see cref="IPerformance"/>.
    /// </para>
    /// <para>
    /// Since the most common thing to do with a <see cref="Screenshot"/> object is to save it to disk, consumers should
    /// consider using the <see cref="Tasks.TakeAndSaveScreenshot"/> task instead, via either the builder methods
    /// <see cref="PerformableBuilder.TakeAndSaveAScreenshot"/> or
    /// <see cref="PerformableBuilder.TakeAndSaveAScreenshotIfSupported"/>. These deal with both the taking and saving of
    /// the screenshot in a single step.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// using OpenQA.Selenium;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;Screenshot&gt;
    /// public async ValueTask&lt;Screenshot&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     return await actor.PerformAsync(TakeAScreenshot(), cancellationToken);
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.TakeAScreenshot"/>
    /// <seealso cref="PerformableBuilder.TakeAScreenshotIfSupported"/>
    /// <seealso cref="Actions.SaveScreenshot"/>
    /// <seealso cref="Tasks.TakeAndSaveScreenshot"/>
    public class TakeScreenshot : IPerformableWithResult<Screenshot>, ICanReport
    {
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} takes a screenshot of the web page", actor.Name);

        /// <inheritdoc/>
        public ValueTask<Screenshot> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var webDriver = actor.GetAbility<BrowseTheWeb>().WebDriver;
            if(!(webDriver is ITakesScreenshot takesScreenshot))
            {
                return throwIfUnsupported
                    ? throw new InvalidOperationException($"The WebDriver must support taking screenshots.")
                    : new ValueTask<Screenshot>((Screenshot) null);
            }

            return new ValueTask<Screenshot>(takesScreenshot.GetScreenshot());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TakeScreenshot"/> class.
        /// </summary>
        /// <param name="throwIfUnsupported">If set to <c>true</c>, throws an exception if the WebDriver does not support taking screenshots.</param>
        public TakeScreenshot(bool throwIfUnsupported = true)
        {
            this.throwIfUnsupported = throwIfUnsupported;
        }
    }
}