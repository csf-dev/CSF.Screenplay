using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Selenium.Reporting
{
    /// <summary>
    /// Implementation of <see cref="ITakesScreenshotWhenPerformableFails"/> which takes a screenshot in PNG format, with the name
    /// <c>Auto-screenshot on performable failure</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A screenshot is only taken if the <see cref="PerformableFailureEventArgs.Exception"/> is not an instance of
    /// <see cref="PerformableException"/> and if the <see cref="ActorEventArgs.Actor"/> has the ability <see cref="BrowseTheWeb"/>.
    /// If the exception is a performable exception then this indicates that an 'inner' performable is the true source of the exception
    /// and that this failure simply represents an exception bubbling upward through the performable stack.
    /// Additionally, if the actor cannot browse the web, then they of course cannot possibly take a screenshot.
    /// </para>
    /// <para>
    /// This handler internally uses the task <see cref="Tasks.TakeAndSaveScreenshot"/> and configures it to fail silently if taking
    /// a screenshot is not supported by the current browser.  Thus, this is not certain to always result in a screenshot.
    /// When a screenshot is taken in this way, it is saved alongside the report as an asset file.
    /// </para>
    /// </remarks>
    public class PerformableFailureScreenshotTaker : ITakesScreenshotWhenPerformableFails
    {
        /// <inheritdoc/>
        public void OnPerformableFailed(object sender, PerformableFailureEventArgs ev)
        {
            if (ev.Exception is PerformableException) return;
            if (!((ICanPerform)ev.Actor).HasAbility<BrowseTheWeb>()) return;

            var task = ev.Actor.PerformAsync(PerformableBuilder.TakeAndSaveAScreenshotIfSupported()
                .WithTheFormat(OpenQA.Selenium.ScreenshotImageFormat.Png)
                .WithTheName("Auto-screenshot on performable failure"));
            task.AsTask().Wait();
        }
    }
}
