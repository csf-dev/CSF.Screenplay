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
            
            var logProvider = driver.WebDriver.Manage()?.Logs;
            var logs = logProvider.GetLog(LogType.Browser);
            return new ValueTask<IReadOnlyList<BrowserLog>>(logs
                .Select(x => new BrowserLog { Level = x.Level.ToString(), Message = x.Message, Timestamp = x.Timestamp })
                .ToArray());
        }
    }
}