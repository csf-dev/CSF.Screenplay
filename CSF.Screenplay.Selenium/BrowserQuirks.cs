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
        /// Gets the name of a browser quirk which means that a Web Driver cannot set the value of an <c>&lt;input type="date"&gt;</c> using
        /// "Send Keys".
        /// </summary>
        /// <remarks>
        /// <para>
        /// Date input fields have patchy support amongst browsers and web drivers.  The UI which appears in order to set a date often responds
        /// poorly to typing a new date manually.  Additionally, the calendar UI which most browsers will render is not built from HTML elements
        /// and thus cannot be interacted with in the traditional way, by Web Drivers.  Thus, if we cannot 'type' a new date and cannot interact
        /// by clicking on the calendar, all that remains is to use a JavaScript workaround to set the date.
        /// </para>
        /// </remarks>
        public static readonly string CannotSetInputTypeDateWithSendKeys = "CannotSetInputTypeDateWithSendKeys";

        /// <summary>
        /// Gets the name of a browser quirk which means that - after navigation to a different page - the browser must be instructed to wait
        /// until the following page document is ready.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Most Web Driver implementations, without this quirk, automatically wait for the 'incoming' page (in traditional web document
        /// navigation) to be ready before they attempt to interact with it.  That is - if the "document ready" event has not yet occurred,
        /// they will continue waiting.  Some browsers/web drivers (those with this quirk) need to be instructed to wait for that ready-state,
        /// or else they are liable to attempt to interact with a page which is not yet fully loaded.
        /// </para>
        /// <para>
        /// Note that this only applies to traditional web navigation.  It does not apply to navigating SPA-type apps.
        /// </para>
        /// </remarks>
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