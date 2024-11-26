using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Represents an action to clear all browser cookies for the current site.
    /// </summary>
    public class ClearCookies : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
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