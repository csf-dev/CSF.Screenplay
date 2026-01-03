using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Screenplay action which opens (navigates) directly to a specified URL using the actor's WebDriver.
    /// </summary>
    public class OpenUrl : IPerformable, ICanReport
    {
        readonly NamedUri uri;

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<BrowseTheWeb>();
            ability.WebDriver.Url = uri.Uri.AbsoluteUri;
            return default;
        }
        
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} opens their browser at {UriName}: {Uri}", actor.Name, uri.Name, uri.Uri.AbsoluteUri);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenUrl"/> class with the specified URL.
        /// </summary>
        /// <param name="uri">The URL to open.</param>
        public OpenUrl(NamedUri uri)
        {
            this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }
    }
}