using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Screenplay ability which allows an <see cref="Actor"/> to use a base URI.
    /// </summary>
    public class UseABaseUri : ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
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
        /// <param name="baseUri">The base URI.</param>
        public UseABaseUri(Uri baseUri)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }
    }
}