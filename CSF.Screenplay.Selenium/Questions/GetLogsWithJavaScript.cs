using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A Screenplay Question which retrieves the logs which have been intercepted using a Javascript workaround, via
    /// <see cref="Actions.BeginCollectingLogsWithJavaScript"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When used, this question retrieves all logs which have been intercepted since the last time this question was used,
    /// or since <see cref="Actions.BeginCollectingLogsWithJavaScript"/> was first executed on the current web page, whichever is
    /// more recent.
    /// Repeated use of this question will not return duplicate log messages; messages are marked as 'consumed' as they are returned
    /// and previously-consumed messages are filtered-out by the Javascript.
    /// </para>
    /// <para>
    /// Note that because of the imperfect nature of the JavaScript workaround, it is possible for some messages to be missed/lost
    /// if they occur very early on in the page-loading sequence, before collection of messages has begun. This is a limitation of
    /// the Javascript technique; use a browser which <see cref="BrowserQuirks.HasNativeLogsSupport"/> if you need to be sure you are
    /// not missing any messages.
    /// </para>
    /// <para>
    /// This action is for use only with web browsers which have the <see cref="BrowserQuirks.CanGetLogsWithJavascriptWorkaround"/> quirk.
    /// </para>
    /// <para>
    /// Note that this question will not (and cannot) respect the WebDriver's logging preference, such as set at
    /// <see cref="Extensions.WebDriver.Factories.WebDriverCreationOptions.BrowserLogLevel"/> or
    /// <see cref="DriverOptions.SetLoggingPreference(string, LogLevel)"/>.
    /// Consumers will need to manually filter for messages which they care about, based upon the log level.
    /// </para>
    /// </remarks>
    public class GetLogsWithJavaScript : IPerformableWithResult<IReadOnlyList<BrowserLog>>, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} reads the logs from the web browser using JavaScript");

        /// <inheritdoc/>
        public async ValueTask<IReadOnlyList<BrowserLog>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var driver = actor.GetAbility<BrowseTheWeb>();
            if(!driver.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
                throw new NotSupportedException("The WebDriver must have support for retrieving logs using a Javascript workaround.");

            var result = await actor.PerformAsync(PerformableBuilder.ExecuteAScript(Scripts.GetLogs), cancellationToken);
            return result
                .Cast<IDictionary<string,object>>()
                .Select(x => new BrowserLog { Level = x["Level"].ToString(), Message = x["Message"].ToString(), Timestamp = DateTime.Parse((string) x["Timestamp"], CultureInfo.InvariantCulture) })
                .ToArray();
        }
    }
}