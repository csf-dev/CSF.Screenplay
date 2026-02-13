using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which reads and returns the title of the current browser window.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The best way to use this action is via the builder method <see cref="PerformableBuilder.ReadTheWindowTitle()"/>.
    /// </para>
    /// <para>
    /// This very simple question gets the text which is displayed as the browser window (or tab) title.
    /// In a normal web page, that would be what's contained in the <c>&lt;title&gt;</c> element.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;string&gt;
    /// public async ValueTask&lt;string&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     return await actor.PerformAsync(ReadTheWindowTitle(), cancellationToken);
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.ReadTheWindowTitle"/>
    public class GetWindowTitle : IPerformableWithResult<string>, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} gets the title of the current browser window", actor);

        /// <inheritdoc/>
        public ValueTask<string> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
            => new ValueTask<string>(actor.GetAbility<BrowseTheWeb>().WebDriver.Title);
    }
}