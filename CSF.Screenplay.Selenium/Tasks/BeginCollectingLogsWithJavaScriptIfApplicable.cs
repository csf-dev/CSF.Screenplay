using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// Determines whether the current actor needs to use a JS technique to collect web browser console logs.
    /// If they do, executes the action which begins collecting them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When used, this action should be executed as soon as possible after the current page has completed loading.
    /// Ideally, directly after <see cref="ClickAndWaitForDocumentReady"/> and <see cref="Actions.OpenUrl"/>.
    /// This Task decides whether it is appropriate to make use of <see cref="Actions.BeginCollectingLogsWithJavaScript"/>
    /// action, invoking it if appropriate.
    /// </para>
    /// <para>
    /// Note that this action/the script needs to be re-run after each traditional web page navigation/reload.
    /// However, due to the nature of SPAs, it <em>does not need to be re-run</em> following an SPA-style navigation.
    /// On supported browsers, there is no harm in re-running this script when it is not needed, except for the impact on
    /// performance (wasted network roundtrips).
    /// </para>
    /// <para>
    /// The logic used by this task is:
    /// </para>
    /// <list type="bullet">
    /// <item><description>If the actor's <see cref="BrowseTheWeb"/> ability has <see cref="BrowseTheWeb.ShouldCollectLogs"/>
    /// set to <see langword="false"/> then the JavaScript technique <em>will not</em> be used.</description></item>
    /// <item><description>If the actor's <see cref="BrowseTheWeb"/> ability has the quirk <see cref="BrowserQuirks.HasNativeLogsSupport"/>
    /// <em>and</em> the implementation of <see cref="IWebDriver"/> is not a <see cref="RemoteWebDriver"/> then the JavaScript
    /// technique <em>will not</em> be used.</description></item>
    /// <item><description>If the <see cref="IWebDriver"/> does not have the quirk <see cref="BrowserQuirks.CanGetLogsWithJavascriptWorkaround"/>
    /// then the JavaScript technique <em>will not</em> be used.</description></item>
    /// <item><description>Otherwise, <see cref="BrowseTheWeb.ShouldCollectLogs"/> is <see langword="true"/>, the <see cref="IWebDriver"/>
    /// has the quirk <see cref="BrowserQuirks.CanGetLogsWithJavascriptWorkaround"/> and does not have a better way to get the logs, then the
    /// JavaScript technique <em>will be used</em>.</description></item>
    /// </list>
    /// <para>
    /// Note that <em><see cref="RemoteWebDriver"/> instances cannot collect the logs natively</em>.  This is because the WebDriver remote protocol
    /// does not support this functionality.  Web Drivers which support it locally cannot do so remotely.  See
    /// <see href="https://github.com/w3c/webdriver/issues/1428">this issue about logging being missing from the w3c WebDriver standard</see>
    /// and the signposted <see href="https://www.w3.org/2018/10/25-webdriver-minutes.html#item05">minutes from this meeting</see> for more info.
    /// </para>
    /// </remarks>
    public class BeginCollectingLogsWithJavaScriptIfApplicable : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        {
            var shouldPerform = ShouldCollectLogsWithJavaScript(actor);
            return formatter.Format("{Actor} chooses whether they should collect web browser console logs using JavaScript; {Outcome}",
                                    actor,
                                    shouldPerform ? "they should" : "they should not");

        }

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var shouldPerform = ShouldCollectLogsWithJavaScript(actor);
            if(!shouldPerform) return;

            await actor.PerformAsync(BeginCollectingLogsWithJavaScript(), cancellationToken);
        }

        /// <summary>
        /// Gets a value indicating whether the specified actor should use the JavaScript technique to get browser logs.
        /// </summary>
        /// <remarks>
        /// <para>
        /// See the remarks for this type for more information about that logic.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor</param>
        /// <returns><see langword="true"/> if the JavaScript technique for log collection is applicable; <see langword="false"/> if not.</returns>
        public static bool ShouldCollectLogsWithJavaScript(ICanPerform actor)
        {
            var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
            return ShouldCollectLogsWithJavaScript(browseTheWeb);
        }

        /// <summary>
        /// Gets a value indicating whether the specified browse the web ability should use the JavaScript technique to get browser logs.
        /// </summary>
        /// <remarks>
        /// <para>
        /// See the remarks for this type for more information about that logic.
        /// </para>
        /// </remarks>
        /// <param name="browseTheWeb">The ability</param>
        /// <returns><see langword="true"/> if the JavaScript technique for log collection is applicable; <see langword="false"/> if not.</returns>
        public static bool ShouldCollectLogsWithJavaScript(BrowseTheWeb browseTheWeb)
        {
            if(!browseTheWeb.ShouldCollectLogs) return false;

            return browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround)
                && (!browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport)
                 || browseTheWeb.WebDriver.Unproxy() is RemoteWebDriver);
        }
    }
}