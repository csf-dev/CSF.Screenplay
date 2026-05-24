using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A performable which retrieves web browser logs using a native technique.
    /// </summary>
    /// <remarks>
    /// <para>
    /// At present, this technique is only verified to work in Chromium-based browsers (Chrome, Edge etc).
    /// Note that the logs exposed to this technique are affected by the WebDriver's logging preferences.
    /// See <see cref="Extensions.WebDriver.Factories.WebDriverCreationOptions"/> for more info.
    /// </para>
    /// <para>
    /// Also note that this technique is not viable with Remote WebDrivers which use the W3C WebDriver protocol.
    /// See <see href="https://github.com/w3c/webdriver/issues/1428">this issue for the W3C protocol</see> in which it is
    /// confirmed that retrieving logs is not part of the protocol and has not been implemented.  That discussion signposts
    /// <see href="https://www.w3.org/2018/10/25-webdriver-minutes.html#item05">these minutes from a meeting in 2018</see>
    /// where the reasons behind that were discussed.
    /// </para>
    /// <para>
    /// As a result of the above, this Question will throw an exception if the underlying WebDriver is an instance of
    /// <see cref="OpenQA.Selenium.Remote.RemoteWebDriver"/>.  Instead of using this technique, use the remote web driver's
    /// own built-in technique to access console logs.  For example, some providers offer a separate non-WebDriver-based API
    /// to read the logs.  Accessing these is outside the scope of this class.
    /// </para>
    /// </remarks>
    public class GetLogsNatively : IPerformableWithResult<IReadOnlyList<BrowserLog>>, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} reads the logs from the web browser using the native technique");

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<BrowserLog>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var driver = actor.GetAbility<BrowseTheWeb>();
            if(!driver.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport))
                throw new NotSupportedException("The WebDriver must have support for native log retrieval.");
            if(driver.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
                throw new NotSupportedException("Getting native logs is not supported for Remote Web Drivers, see the documentation for this class for more information");
            
            var logProvider = driver.WebDriver.Manage().Logs;
            var logs = logProvider.GetLog(LogType.Browser);
            return new ValueTask<IReadOnlyList<BrowserLog>>(logs
                .Select(x => new BrowserLog { Level = x.Level.ToString(), Message = x.Message, Timestamp = x.Timestamp })
                .ToArray());
        }
    }
}