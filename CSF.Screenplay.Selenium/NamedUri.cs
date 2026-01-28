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
    /// </para>
    /// <para>
    /// Note the inclusion of implicit conversion operators which allow for seamless conversion from <see cref="Uri"/> and/or
    /// <see cref="string"/> instances to instances of this type.
    /// </para>
    /// </remarks>
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