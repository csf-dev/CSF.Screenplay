using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Represents an action to delete a specific browser cookie by name.
    /// </summary>
    public class DeleteTheCookie : IPerformable, ICanReport
    {
        readonly string cookieName;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deletes the cookie named {CookieName}", actor.Name, cookieName);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
            var cookies = browseTheWeb.WebDriver.Manage().Cookies;
            cookies.DeleteCookieNamed(cookieName);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTheCookie"/> class with the specified cookie name.
        /// </summary>
        /// <param name="cookieName">The name of the cookie to delete.</param>
        public DeleteTheCookie(string cookieName)
        {
            this.cookieName = cookieName ?? throw new System.ArgumentNullException(nameof(cookieName));
        }
    }
}