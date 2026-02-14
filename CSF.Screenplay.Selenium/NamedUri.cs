using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Model containing a <see cref="Uri"/> with a corresponding human-readable name.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This model allows for the association of human-readable names with URIs.  This is most useful when making use
    /// of Screenplay reports, where the human-readable name can be used to describe the URI in a more user-friendly way.
    /// Named Uris are used with the <see cref="Actions.OpenUrl"/> and <see cref="Tasks.OpenUrlRespectingBase"/> performables
    /// to facilitate direct web browser navigation.
    /// </para>
    /// <para>
    /// Note the inclusion of implicit conversion operators which allow for seamless conversion from <see cref="System.Uri"/> and/or
    /// <see cref="string"/> instances to instances of this type.  It is not recommended to use them, though, as neither
    /// supplies a name.  This would mean that the naked Uri would appear in reports, instead of a human-readable name.
    /// </para>
    /// <para>
    /// If you have a need to switch environments at runtime, consider specifying named Uris using <b>relative</b> Uri fragments.
    /// This may be combined with the <see cref="UseABaseUri"/> ability to <see cref="RebaseTo(Uri)">rebase the relative Uri onto
    /// a base Uri</see> at runtime.
    /// This could be useful in testing, for example, whereby the same test suite must be run against a number of environments:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Locally, on a developer's computer, with a base Uri such as <c>https://localhost:8080/</c></description></item>
    /// <item><description>On a testing environment, with a base Uri such as <c>https://testing.example.com/</c></description></item>
    /// <item><description>On a staging environment, with a base Uri such as <c>https://staging.example.com/</c></description></item>
    /// </list>
    /// </remarks>
    /// <seealso cref="Actions.OpenUrl"/>
    /// <seealso cref="Tasks.OpenUrlRespectingBase"/>
    /// <seealso cref="UseABaseUri"/>
    public sealed class NamedUri : IHasName
    {
        /// <summary>
        /// Gets the human-readable name for this Uri.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Uri associated with this instance.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Gets a copy of the current named URI, except 'rebased' using the specified base URI.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the current <see cref="Uri"/> is <see cref="UriKind.Absolute"/> then this method has not effect and the named URI which is
        /// returned is the unmodified current instance.
        /// </para>
        /// <para>
        /// If the current Uri is not absolute, then the specified base URI is prepended to the current URI, serving as a base.
        /// The new URI is then returned from this method.  Note that this method will never result in the current instance being
        /// mutated, at most it will only return a copy of the current instance, which has the newly-rebased URI.
        /// </para>
        /// </remarks>
        /// <param name="baseUri">A new base URI</param>
        /// <returns>A URI which might have been rebased onto the new base URI</returns>
        public NamedUri RebaseTo(Uri baseUri)
        {
            if(baseUri == null) throw new ArgumentNullException(nameof(baseUri));

            if (Uri.IsAbsoluteUri) return this;

            var rebased = new Uri(baseUri, Uri);
            return new NamedUri(rebased, Name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUri"/> class.
        /// </summary>
        /// <param name="uri">The URI to associate with this instance.</param>
        /// <param name="name">The human-readable name for the URI. If null, the URI string will be used as the name.</param>
        public NamedUri(Uri uri, string name = null)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            Name = name ?? uri.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedUri"/> class.
        /// </summary>
        /// <param name="uri">The URI to associate with this instance.</param>
        /// <param name="name">The human-readable name for the URI. If null, the URI string will be used as the name.</param>
        public NamedUri(string uri, string name = null)
        {
            if(uri is null)
                throw new ArgumentNullException(nameof(uri));
            Uri = new Uri(uri, UriKind.RelativeOrAbsolute);
            Name = name ?? uri.ToString();
        }

        /// <summary>
        /// Converts a <see cref="Uri"/> to a <see cref="NamedUri"/>.
        /// </summary>
        /// <param name="uri">The URI to convert.</param>
        /// <returns>A new <see cref="NamedUri"/> instance.</returns>
        public static implicit operator NamedUri(Uri uri) => uri is null ? null : new NamedUri(uri);

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="NamedUri"/>.
        /// </summary>
        /// <param name="uri">The URI to convert.</param>
        /// <returns>A new <see cref="NamedUri"/> instance.</returns>
        public static implicit operator NamedUri(string uri) => uri is null ? null : new NamedUri(uri);
    }
}