using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A Screenplay task which navigates directly to a specified URL using the actor's WebDriver.
    /// If the specified Uri is relative then it is made absolute by basing it upon the Uri indicated by the <see cref="UseABaseUri"/> ability.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this task via the builder method <see cref="PerformableBuilder.OpenTheUrl(NamedUri)"/>, which automatically makes
    /// use of this task. This task behaves very similarly to the action <see cref="OpenUrl"/>, except for base-URI-replacement.
    /// </para>
    /// <para>
    /// When this task is used, if the specified named Uri (constructor injected) is already absolute then this task has no effect
    /// beyond passing that same absolute Uri to <see cref="OpenUrl"/>.
    /// If the specified Uri is relative then the <see cref="UseABaseUri"/> ability is retrieved from the actor; this will throw if
    /// the actor does not possess the ability.
    /// If they do, then the Uri specified to this task will be <em>rebased</em> onto the <see cref="UseABaseUri.BaseUri"/>, via
    /// <see cref="NamedUri.RebaseTo(Uri)"/>. The rebased Uri will then be used with <see cref="OpenUrl"/>.
    /// </para>
    /// <para>
    /// The purpose of this task is to permit developers to specify <see cref="NamedUri"/> instances using relative <see cref="Uri"/>
    /// values instead of absolute ones.  When doing so, the performance must grant the <see cref="UseABaseUri"/> ability to the actor
    /// who is making use of the WebDriver (via <see cref="BrowseTheWeb"/>). This allows the final absolute Uris for the functionality
    /// to be determined at runtime, via the combination of a base Uri and a relative part.
    /// </para>
    /// <para>
    /// This is expected to be useful to developers, who may need their Screenplay logic to operate upon a specified environment
    /// (determined by a base Uri) at runtime.  However, the relative Uri paths to pages within that environment remain identical
    /// regardless of the environment.  The combination of the <see cref="UseABaseUri"/> ability and this task make such an
    /// architecture possible.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// This task is used in exactly the same way as <see cref="OpenUrl"/>.  The example below shows the opening of the URL
    /// <c>https://example.com/user/shoppingCart</c>.  Note that in a real performance, the actor would be granted the
    /// <see cref="UseABaseUri"/> ability at a far higher-level performable than the position in which they are performing navigation.
    /// This example is 'compressed' for brevity.
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// var examplePage = new NamedUri("user/shoppingCart", "the user's shopping cart");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     actor.IsAbleTo(new UseABaseUri(new Uri("https://example.com")));
    ///     await actor.PerformAsync(OpenTheUrl(examplePage), cancellationToken);
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="UseABaseUri"/>
    /// <seealso cref="PerformableBuilder.OpenTheUrl(NamedUri)"/>
    /// <seealso cref="OpenUrl"/>
    public class OpenUrlRespectingBase : IPerformable, ICanReport
    {
        readonly NamedUri uri;

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            if(uri.Uri.IsAbsoluteUri)
                return actor.PerformAsync(new OpenUrl(uri), cancellationToken);

            var ability = actor.GetAbility<UseABaseUri>();
            var rebased = uri.RebaseTo(ability.BaseUri);
            return actor.PerformAsync(new OpenUrl(rebased), cancellationToken);
        }
        
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
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