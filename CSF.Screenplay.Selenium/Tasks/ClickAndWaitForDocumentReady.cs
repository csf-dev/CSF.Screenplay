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
    /// Screenplay task similar to <see cref="Actions.Click"/> but which additionally waits for a page-load to complete after clicking.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this task via <c>ClickOn(element).AndWaitForANewPageToLoad()</c>.
    /// The benefit of this task is that it ensures that (following a page-load navigation), the incoming page is ready before
    /// subsequent performables are executed.
    /// </para>
    /// </remarks>
    public class ClickAndWaitForDocumentReady : ISingleElementPerformable
    {
        const string COMPLETE_READY_STATE = "complete";

        static readonly NamedScriptWithResult<string> getReadyState = Scripts.GetTheDocumentReadyState;
        static readonly TimeSpan pollingInterval = TimeSpan.FromMilliseconds(200);

        readonly TimeSpan waitTimeout;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clicks on {Element} and waits up to {Time} for the next page to load", actor, element.Value, waitTimeout);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            await actor.PerformAsync(ClickOn(element.Value), cancellationToken);
            await actor.PerformAsync(WaitUntil(PageIsReady).ForAtMost(waitTimeout).Named("the page is ready").WithPollingInterval(pollingInterval),
                                     cancellationToken);
        }

        static Func<IWebDriver,bool> PageIsReady => driver => driver.ExecuteJavaScript<string>(getReadyState.ScriptBody) == COMPLETE_READY_STATE;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickAndWaitForDocumentReady"/> class.
        /// </summary>
        /// <param name="waitTimeout">The maximum duration to wait for the document to be ready.</param>
        public ClickAndWaitForDocumentReady(TimeSpan waitTimeout)
        {
            this.waitTimeout = waitTimeout;
        }
    }
}