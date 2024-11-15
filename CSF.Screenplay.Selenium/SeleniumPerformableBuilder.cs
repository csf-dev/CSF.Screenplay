using System;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Builder type for creating performables which interact with Selenium WebDriver via Screenplay.
    /// </summary>
    public static class SeleniumPerformableBuilder
    {
        /// <summary>
        /// Gets a performable action which opens a URL.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the specified Uri is a relative Uri, then this task will use the actor's <see cref="UseABaseUri"/> ability (if present)
        /// to transform the relative Uri into an absolute one.  The specified Uri will be used directly if it is already absolute.
        /// </para>
        /// </remarks>
        /// <param name="uri">The uri at which to open the web browser.</param>
        /// <returns>A performable</returns>
        public static IPerformable OpenTheUrl(NamedUri uri) => new Tasks.OpenUrlRespectingBase(uri);
    }
}