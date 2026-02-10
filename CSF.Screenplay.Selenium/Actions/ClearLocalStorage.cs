using System.Threading;
using System.Threading.Tasks;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which clears the browser's local storage for the current site.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.ClearLocalStorage()"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to clear the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage">local storage</see>
    /// store for the current domain.
    /// </para>
    /// <para>
    /// Note that this operation is not supported first-class by Selenium WebDriver (it is
    /// not part of the WebDriver specification).  Clearing local storage is implemented by sending the following
    /// JavaScript to the web browser via <see cref="ExecuteJavaScript"/>: <c>localStorage.clear()</c>.
    /// Bear this in mind when using this action, as it is not neccesarily identical to a user clearing the storage
    /// interactively via the browser's settings.
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
    ///     await actor.PerformAsync(ClearLocalStorage(), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class ClearLocalStorage : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clears their browser local storage for the current site", actor.Name);

        /// <inheritdoc/>
        public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            await actor.PerformAsync(ExecuteAScript(Scripts.ClearLocalStorage));
        }
    }
}