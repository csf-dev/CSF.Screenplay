using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A Screenplay task which combines a <see cref="Click"/> action with cross-browser waiting logic,
    /// for navigating to a new web page.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this task in performance logic using the builder method <see cref="ClickOn(ITarget)"/>, following this up
    /// with the <see cref="Builders.ClickBuilder.AndWaitForANewPageToLoad(TimeSpan?)"/> method.
    /// That builder functionality differentiates the returned performable from the normal <see cref="Click"/> action.
    /// </para>
    /// <para>
    /// The rationale for this task's existence is primarily to deal with web browsers which are affected by the quirk
    /// <see cref="BrowserQuirks.NeedsToWaitAfterPageLoad"/>.  See the documentation for that quirk for more information
    /// on what it means for web browser.
    /// </para>
    /// <para>
    /// This task deals with browsers affected by that quirk by extending the click behaviour. When the WebDriver is
    /// affected by this quirk, as well as clicking upon an element, a reference to that element is saved. The WebDriver
    /// is then polled until the element clicked has become <b>stale</b>.  A stale element is one which is no longer
    /// present in the web browser.  The staleness of the clicked element is used as a proxy for the unloading of the
    /// 'outgoing' web page.
    /// Once the mechanism describe above has determined that the outgoing web page has unloaded, this task begins a second
    /// waiting process. The second wait executes the JavaScript <see cref="Scripts.GetTheDocumentReadyState"/> repeatedly
    /// until it returns the result <c>complete</c>. At that point, the waiting is over and the performance may continue.
    /// </para>
    /// <para>
    /// The mechanism described above also includes a (constructor injected) timeout. The timeout is used twice; it applies
    /// both to the unloading of the old page the ready-state of the new page returning <c>complete</c>.  Thus in a theoretical
    /// worst-case scenario, this task could lead to a wait of twice the specified timeout value.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// This example demonstrates clicking a link to a settings page and waiting for the page to be ready.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget settingsLink = new CssSelector("nav#app_navigation .settings a", "the link to the settings page");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     await actor.PerformAsync(ClickOn(settingsLink).AndWaitForANewPageToLoad(), cancellationToken);
    ///     // regardless of WebDriver implementation, further performables here will not execute
    ///     // until the settings page has completely loaded.
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="BrowserQuirks.NeedsToWaitAfterPageLoad"/>
    /// <seealso cref="ClickOn(ITarget)"/>
    /// <seealso cref="Builders.ClickBuilder.AndWaitForANewPageToLoad(TimeSpan?)"/>
    public class ClickAndWaitForDocumentReady : ISingleElementPerformable
    {
        const string completeReadyState = "complete";

        readonly TimeSpan waitTimeout;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clicks on {Element} and waits up to {Time} for the next page to load", actor, element.Value, waitTimeout);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            await actor.PerformAsync(ClickOn(element.Value), cancellationToken);
            if(!webDriver.HasQuirk(BrowserQuirks.NeedsToWaitAfterPageLoad)) return;

            await actor.PerformAsync(WaitUntil(ElementIsStale(element.Value.WebElement))
                                                .ForAtMost(waitTimeout)
                                                .Named($"{element.Value} is no longer on the page"),
                                     cancellationToken);
            await actor.PerformAsync(WaitUntil(PageIsReady).ForAtMost(waitTimeout).Named("the next page is ready"),
                                     cancellationToken);
        }

        /// <summary>
        /// Gets a function which accesses <see cref="IWebElement.Enabled"/>, which forces communication with the WebDriver and verifies the
        /// existence of the <paramref name="element"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The returned function catches and ignores <see cref="StaleElementReferenceException"/>, returning <see langword="true"/> if it is
        /// caught.  Indeed, the technique used by this task to detect an outgoing page is to "wait until a stale element exception is thrown".
        /// </para>
        /// </remarks>
        /// <param name="element">The element for which we wish to test the staleness</param>
        /// <returns>A function which will return <see langword="true"/> if the <paramref name="element"/> is stale, or <see langword="false"/> if not.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="element"/> is <see langword="null"/>.</exception>
        static Func<IWebDriver,bool> ElementIsStale(IWebElement element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));

            return driver =>
            {
                try
                {
                    var _ = element.Enabled;
                    return false;
                }
                catch(StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        static Func<IWebDriver,bool> PageIsReady => driver
            => driver.ExecuteJavaScript<string>(Scripts.GetTheDocumentReadyState.ScriptBody) == completeReadyState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickAndWaitForDocumentReady"/> class.
        /// </summary>
        /// <param name="waitTimeout">The timeout duration for both the page-unload and document-ready waits.
        /// See the remarks on this class for more info.</param>
        public ClickAndWaitForDocumentReady(TimeSpan waitTimeout)
        {
            this.waitTimeout = waitTimeout;
        }
    }
}