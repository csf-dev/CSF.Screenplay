using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A Screenplay question to get the title of the current browser window.
    /// </summary>
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