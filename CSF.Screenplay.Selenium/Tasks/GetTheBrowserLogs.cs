using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Questions;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A screenplay task which attempts to get the web browser console logs using the best technique available.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The order of precedence is:
    /// </para>
    /// <list type="number">
    /// <item><description>If the WebDriver has the quirk/capability <see cref="BrowserQuirks.HasNativeLogsSupport"/> then
    /// <see cref="GetNativeBrowserLogs"/> is used.</description></item>
    /// <item><description>If the WebDriver has the quirk/capability <see cref="BrowserQuirks.RequiresJavascriptToGetLogs"/> then
    /// <see cref="GetBrowserLogsWithJavascript"/> is used.</description></item>
    /// <item><description>If the current instance has been constructed in such a manner as to throw if getting logs is unsupported
    /// then this task will throw <see cref="NotSupportedException"/>.</description></item>
    /// <item><description>If the current instance has been constructed not to throw if unsupported, then if neither technique
    /// of getting logs is available, this task will always return an empty collection of log entries.</description></item>
    /// </list>
    /// </remarks>
    public class GetTheBrowserLogs : IPerformableWithResult<IReadOnlyList<BrowserLog>>, ICanReport
    {
        readonly bool throwIfUnsupported;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<BrowserLog>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<BrowseTheWeb>();
            if(ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport))
                return actor.PerformAsync(GetNativeBrowserLogs(), cancellationToken);

            if(ability.WebDriver.HasQuirk(BrowserQuirks.RequiresJavascriptToGetLogs))
                return actor.PerformAsync(GetBrowserLogsWithJavascript(), cancellationToken);

            if(throwIfUnsupported)
                throw new NotSupportedException("The current WebDriver does not support retrieving console logs, and throwIfUnsupported is set to true");

            return new ValueTask<IReadOnlyList<BrowserLog>>(Array.Empty<BrowserLog>());
        }

        /// <summary>
        /// Constructs an instance of <see cref="GetTheBrowserLogs"/>.
        /// </summary>
        /// <param name="throwIfUnsupported">If true, performing this task will throw when the current WebDriver does not support retrieving
        /// console logs; otherwise an empty collection will be returned.</param>
        public GetTheBrowserLogs(bool throwIfUnsupported = true)
        {
            this.throwIfUnsupported = throwIfUnsupported;
        }
    }
}