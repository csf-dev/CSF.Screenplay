using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Screenplay ability which allows an <see cref="Actor"/> to use a base URI.
    /// </summary>
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