using System.Collections.Generic;
using CSF.Extensions.WebDriver.Quirks;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Static class which holds known browser quirks information.
    /// </summary>
    public static class BrowserQuirks
    {
        /// <summary>
        /// Gets the name of a browser quirk, for browsers which cannot set the value of an <c>&lt;input type="date"&gt;</c> using the
        /// "Send Keys" technique.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Some WebDriver implementations permit typing a locale-formatted date string into the HTML element, as if it were any other
        /// <c>&lt;input&gt;</c> element.  Other WebDriver implementations cannot interact with Date inputs in that way and
        /// can only enter a new value via a JavaScript workaround.  This quirk marks browsers which require such a workaround.
        /// </para>
        /// <para>
        /// The calendar UI which most browsers will render for a Date input field is not built from HTML elements
        /// and thus cannot be interacted with by a WebDriver.  Thus, if we cannot 'type' a new date and cannot interact
        /// by clicking on the calendar, all that remains is to use a JavaScript workaround to set the date.
        /// </para>
        /// </remarks>
        /// <seealso cref="Tasks.EnterTheDate"/>
        public static readonly string CannotSetInputTypeDateWithSendKeys = "CannotSetInputTypeDateWithSendKeys";

        /// <summary>
        /// Gets the name of a browser quirk, for browsers which must be instructed to wait after navigating to a new web page, until that new
        /// page has finished loading.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In traditional (non-SPA) web page navigation, there is a brief delay after clicking a hyperlink which navigates to a new web page.
        /// That delay is the time for the 'incoming' web page to load and render.  During that delay, the 'outgoing' web page is still visible
        /// on-screen.
        /// </para>
        /// <para>
        /// Most WebDriver implementations, those without this quirk, automatically wait during that loading delay. They do not execute further
        /// WebDriver commands until the 'incoming' web page has finished loading and is displayed on the screen. This prevents unexpected
        /// results and <see cref="OpenQA.Selenium.NoSuchElementException"/> being thrown, and similar.
        /// Browsers/WebDrivers which are affected by this quirk (at the time of writing, all versions of Apple Safari) do not wait during this
        /// loading delay. This means that unless they are explicitly instructed to wait, they can attempt to execute further WebDriver commands
        /// prematurely.
        /// </para>
        /// <para>
        /// The task <see cref="Tasks.ClickAndWaitForDocumentReady"/> analyses the WebDriver for the presence of this quirk.
        /// If the quirk is present then the task inserts wait logic, to work around this behaviour of the affected Browser/WebDriver.
        /// This allows Selenium/Screenplay logic to be written in a manner that behaves consistently cross-browser.
        /// </para>
        /// <para>
        /// It is important to remember that this behaviour and quirk applies only to traditional web browser navigation.  That is a sequence
        /// in which the outgoing page is fully unloaded and the incoming page is retrieved and loaded from the webserver.  It does not apply
        /// to navigating through <see href="https://en.wikipedia.org/wiki/Single-page_application">Single Page Applications</see>.
        /// </para>
        /// </remarks>
        /// <seealso cref="Tasks.ClickAndWaitForDocumentReady"/>
        public static readonly string NeedsToWaitAfterPageLoad = "NeedsToWaitAfterPageLoad";

        /// <summary>
        /// Gets hard-coded information about known browser quirks.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This information ships with CSF.Screenplay.Selenium.  It may be overridden by user-supplied configuration, should
        /// things change in the future.  See <see href="https://csf-dev.github.io/CSF.Extensions.WebDriver/docs/Quirks.html">the
        /// browser quirks reference material</see> for more information.
        /// </para>
        /// </remarks>
        /// <returns>Quirks data, about the peculiarities of specific browsers.</returns>
        public static QuirksData GetQuirksData()
        {
            return new QuirksData
            {
                Quirks = new Dictionary<string, BrowserInfoCollection>
                {
                    {
                        CannotSetInputTypeDateWithSendKeys,
                        new BrowserInfoCollection
                        {
                            AffectedBrowsers = new HashSet<BrowserInfo>
                            {
                                new BrowserInfo { Name = "firefox" },
                                new BrowserInfo { Name = "safari" },
                            }
                        }
                    },
                    {
                        NeedsToWaitAfterPageLoad,
                        new BrowserInfoCollection
                        {
                            AffectedBrowsers = new HashSet<BrowserInfo>
                            {
                                new BrowserInfo { Name = "safari" },
                            }
                        }
                    }
                }
            };
        }
    }
}