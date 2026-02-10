using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which deletes a specific browser cookie by name.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.DeleteTheCookieNamed(string)"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to delete the cookie (for the current domain) which has the specified name.
    /// This is equivalent to a human user entering the browser's developer tools and doing the same.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In this example, the action will delete a cookie named "MyCookieName".
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(DeleteTheCookieNamed("MyCookieName"), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
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