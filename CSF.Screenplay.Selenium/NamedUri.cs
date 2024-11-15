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
        public static implicit operator NamedUri(string uri) => uri is null ? null : new NamedUri(new Uri(uri));
    }
}