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
            if(!uri.Uri.IsAbsoluteUri)
                throw new InvalidOperationException($"The URL to open must be absolute; have you forgotten to grant {actor} the {nameof(UseABaseUri)} ability?");
            ability.WebDriver.Url = uri.Uri.ToString();
            return default;
        }
        
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} opens their browser at {UriName}: {Uri}", actor.Name, uri.Name, uri.Uri.ToString());

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