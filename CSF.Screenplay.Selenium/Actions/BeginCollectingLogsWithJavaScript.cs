using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Executes a custom Javascript which begins collection/interception of web browser console log messages.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When used, this action should be executed as soon as possible after the current page has completed loading.
    /// Ideally, directly after <see cref="Tasks.ClickAndWaitForDocumentReady"/>.
    /// Any log messages which have been sent to the native browser console before this action is executed will be missed
    /// and will not be available to the counterpart question which retrieves log messages: <see cref="Questions.GetLogsWithJavaScript"/>.
    /// </para>
    /// <para>
    /// Note that this action/the script needs to be re-run after each traditional web page navigation/reload.
    /// However, due to the nature of SPAs, it <em>does not need to be re-run</em> following an SPA-style navigation.
    /// On supported browsers, there is no harm in re-running this script when it is not needed, except for the impact on
    /// performance (wasted network roundtrips).
    /// </para>
    /// <para>
    /// This action is for use only with web browsers which have the <see cref="BrowserQuirks.RequiresJavascriptToGetLogs"/> quirk.
    /// </para>
    /// </remarks>
    public class BeginCollectingLogsWithJavaScript : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} begins collecting browser logs from the current page, using a JavaScript technique");

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var driver = actor.GetAbility<BrowseTheWeb>();
            if(!driver.WebDriver.HasQuirk(BrowserQuirks.RequiresJavascriptToGetLogs))
                throw new NotSupportedException("The WebDriver must have support for retrieving logs using a Javascript workaround.");

            await actor.PerformAsync(PerformableBuilder.ExecuteAScript(Scripts.CaptureLogs), cancellationToken);
        }
    }
}