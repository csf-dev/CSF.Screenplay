using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which navigates to a specified URL, as if the user had entered it into the browser address bar.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.OpenTheUrl"/>.
    /// The <c>OpenTheUrl</c> builder method does not have a one-to-one relationship with this action, though.
    /// The builder method actually returns a <xref href="TaskGlossaryItem?text=Screenplay+task"/> named
    /// <see cref="Tasks.OpenUrlRespectingBase"/>.  The purpose of that task is to prepend a base URL to
    /// URLs which are relative.  This action is capable only of navigating to absolute URLs, and it will
    /// raise an exception if the URL is not absolute.
    /// </para>
    /// <para>
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to navigate to the specified URL.  This is performed in the same way as if the user had entered
    /// the URL into their web browser address bar and pressed enter.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In this example, the action will navigate the web browser to <c>https://example.com/myPage</c>.
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// var examplePage = new NamedUri("https://example.com/myPage", "the example web page");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(OpenTheUrl(examplePage), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="Tasks.OpenUrlRespectingBase"/>
    /// <seealso cref="PerformableBuilder.OpenTheUrl(NamedUri)"/>
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