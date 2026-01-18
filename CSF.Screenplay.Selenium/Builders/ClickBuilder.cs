using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Tasks;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Builder for creating click actions on a target element.
    /// </summary>
    public class ClickBuilder : IGetsPerformable
    {
        readonly ITarget target;

        /// <inheritdoc/>
        public IPerformable GetPerformable()
            => SingleElementPerformableAdapter.From(new Click(), target);

        /// <summary>
        /// Gets a more sophisticated <see cref="IPerformable"/> which waits for a page-load to complete after clicking.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method when the click is expected to cause a new web page to load into the browser.
        /// In that case, the performable returned by this method will not only click on the target element.
        /// It will also wait for the <c>DOMContentLoaded</c> event from the web page which is loaded following that click.
        /// This ensures that subsequent interactions with the Web Browser are not performed upon a page which is not yet loaded.
        /// </para>
        /// <para>
        /// Note that the meaning of "a new page loading" is a full Web Browser page load (an entirely new HTML document).
        /// It does not mean an SPA/JavaScript-based navigation.  This method is not for JavaScrpit/SPA navigation.
        /// </para>
        /// </remarks>
        /// <param name="forAtMost"></param>
        /// <returns></returns>
        public IPerformable AndWaitForANewPageToLoad(TimeSpan? forAtMost = null)
            => SingleElementPerformableAdapter.From(new ClickAndWaitForDocumentReady(forAtMost ?? TimeSpan.FromSeconds(5)), target);

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickBuilder"/> class with the specified target.
        /// </summary>
        /// <param name="target">The target element to click. Cannot be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is null.</exception>
        public ClickBuilder(ITarget target)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}