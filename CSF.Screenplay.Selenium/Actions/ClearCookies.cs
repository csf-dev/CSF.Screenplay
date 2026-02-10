using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which clears all browser cookies for the current site/domain.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.ClearAllDomainCookies"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to clear all cookies for the current domain.  This is equivalent to a human user entering the browser's
    /// settings and doing the same.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(ClearAllDomainCookies(), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class ClearCookies : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clears all browser cookies for the current site", actor.Name);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

            var cookies = browseTheWeb.WebDriver.Manage().Cookies;
            cookies.DeleteAllCookies();
            return default;
        }
    }
}