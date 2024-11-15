using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// Screenplay task which opens (navigates) to a specified URL using the actor's WebDriver
    /// This task respects a base URL, chosen via the ability <see cref="UseABaseUri"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the specified Uri is a relative Uri, then this task will use the actor's <see cref="UseABaseUri"/> ability (if present)
    /// to transform the relative Uri into an absolute one.
    /// This task behaves identically to <see cref="OpenUrl"/> when the specified Uri is already absolute.
    /// </para>
    /// </remarks>
    public class OpenUrlRespectingBase : IPerformable, ICanReport
    {
        readonly NamedUri uri;

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            if(uri.Uri.IsAbsoluteUri)
                return actor.PerformAsync(new OpenUrl(uri.Uri), cancellationToken);
            
            if(!actor.TryGetAbility<UseABaseUri>(out var ability))
                return actor.PerformAsync(new OpenUrl(uri.Uri), cancellationToken);

            var absoluteUri = new Uri(ability.BaseUri, uri.Uri);
            return actor.PerformAsync(new OpenUrl(absoluteUri), cancellationToken);
        }
        
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} opens their browser at {Uri}, respecting a base URL if applicable", actor.Name, uri.Name);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenUrlRespectingBase"/> class with the specified URL.
        /// </summary>
        /// <param name="uri">The URL to open.</param>
        public OpenUrlRespectingBase(NamedUri uri)
        {
            this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }
    }
}