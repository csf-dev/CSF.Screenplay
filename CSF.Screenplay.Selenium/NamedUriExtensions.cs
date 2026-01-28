
using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Extension methods for <see cref="NamedUri"/>.
    /// </summary>
    public static class NamedUriExtensions
    {
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
        /// <param name="namedUri">The named URI to rebase</param>
        /// <param name="baseUri">A new base URI</param>
        /// <returns>A URI which might have been rebased onto the new base URI</returns>
        public static NamedUri RebaseTo(this NamedUri namedUri, string baseUri)
        {
            if(namedUri == null) throw new ArgumentNullException(nameof(namedUri));
            if(baseUri == null) throw new ArgumentNullException(nameof(baseUri));
            return namedUri.RebaseTo(new Uri(baseUri, UriKind.Absolute));
        }
    }
    
}