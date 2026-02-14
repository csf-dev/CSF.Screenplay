using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Screenplay ability which allows an <see cref="Actor"/> to use a base Uri, 'completing' relative Uris at runtime.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This ability enables the use of a technique whereby the environment/location of a web application (which is being
    /// tested or manipulated by the Selenium Extension for Screenplay) may be set and changed at runtime, instead of hard-coded.
    /// This could be particularly useful if you are using this extension for testing and wish to be able to run the same suite
    /// of tests against multiple environments.  For example:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Locally, on a developer's computer, with a base Uri such as <c>https://localhost:8080/</c></description></item>
    /// <item><description>On a testing environment, with a base Uri such as <c>https://testing.example.com/</c></description></item>
    /// <item><description>On a staging environment, with a base Uri such as <c>https://staging.example.com/</c></description></item>
    /// </list>
    /// <para>
    /// If you wish to use this technique then when writing instances of <see cref="NamedUri"/>, specify these using relative Uris, such as
    /// <c>new NamedUri("user/shoppingCart", "the user's shopping cart")</c>.  Then, grant your actor this ability, using
    /// <see cref="Actor.IsAbleTo(object)"/>:
    /// </para>
    /// <code>
    /// // The following would not realistically be expected to be seen together in one place.
    /// // This is a 'compressed' example for brevity.
    /// // In a realistic Screenplay, the Actor set-up would be in an IPersona implementation,
    /// // the NamedUri would be declared in a library class and the Actor's performance would
    /// // be within an IPerformable implementation.
    /// 
    /// using CSF.Screenplay.Selenium;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// var useABaseUri = new UseABaseUri(new Uri("https://testing.example.com/"));
    /// myActor.IsAbleTo(useABaseUri);
    /// // Details of getting a BrowseTheWeb ability instance are omitted for brevity.
    /// myActor.IsAbleTo(GetBrowseTheWeb());
    /// 
    /// var shoppingCart = new NamedUri("user/shoppingCart", "the user's shopping cart");
    /// await myActor.PerformAsync(OpenTheUrl(shoppingCart));
    /// 
    /// // Actually opens the URL https://testing.example.com/user/shoppingCart
    /// </code>
    /// <para>
    /// The Uri to use as the base Uri does not need to be hard-coded, it could come from configuration or an environment variable etc.
    /// When the actor makes use of the <see cref="BrowseTheWeb"/> ability and opens a Url using the performable returned from
    /// <see cref="PerformableBuilder.OpenTheUrl(NamedUri)"/>, the specified named Uri <see cref="NamedUri.RebaseTo(Uri)">will be rebased</see>
    /// to the base Uri specified by this ability.
    /// </para>
    /// </remarks>
    /// <seealso cref="Actions.OpenUrl"/>
    /// <seealso cref="Tasks.OpenUrlRespectingBase"/>
    /// <seealso cref="NamedUri"/>
    public class UseABaseUri : ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} is able to use the base URI {BaseUri}",
                                actor.Name,
                                BaseUri);
        
        /// <summary>
        /// Gets the base URI which is associated with the current ability instance.
        /// </summary>
        public Uri BaseUri { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UseABaseUri"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When specifying a base URI via this ability, remember to include a trailing slash where applicable.
        /// The manner in which relative URIs will be resolved into absolute ones, when this ability is present, is via
        /// the two-parameter overload of the <see cref="Uri"/> constructor, taking a <see cref="Uri"/> and a <see cref="string"/>.
        /// As stated in the documentation for this constructor:
        /// </para>
        /// <para>
        /// If the <c>baseUri</c> has relative parts (like <c>/api</c>), then the relative part must be terminated with a slash, (like
        /// <c>/api/</c>), if the relative part of <c>baseUri</c> is to be preserved in the constructed Uri.
        /// </para>
        /// <para>
        /// The Uri held within this ability will be used as that base URI, as described above.
        /// </para>
        /// </remarks>
        /// <param name="baseUri">The base URI.</param>
        public UseABaseUri(Uri baseUri)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }
    }
}