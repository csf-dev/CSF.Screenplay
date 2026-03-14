using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A Screenplay Task which gets the Shadow Root element from the specified Selenium Element, using the best available technique for the current web browser.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is used when working with web pages which use
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Web_components/Using_shadow_DOM">The Shadow DOM technique</see>.
    /// This question allows Screenplay to 'pierce' the Shadow DOM and get the Shadow Root element, so that the Performance
    /// may continue and interact with elements which are inside the Shadow DOM.
    /// </para>
    /// <para>
    /// Note that the <see cref="SeleniumElement"/> which is returned from this question is not a fully-fledged Selenium Element.
    /// It may be used only to get/find elements from inside the Shadow DOM. Use with any other performables will raise
    /// <see cref="NotSupportedException"/>.
    /// </para>
    /// <para>
    /// The <see cref="ITarget"/> passed to this performable as a parameter must be the Shadow Host element.
    /// </para>
    /// <para>
    /// This task will automatically select the best technique by which to get a Shadow Root.  For modern Chromium or Firefox-based
    /// browsers, it will use the native technique: <see cref="GetTheShadowRootNativelyFrom(ITarget)"/>. For older Chromium-based
    /// browsers, or any version of Safari it will use a JavaScript approach: <see cref="GetTheShadowRootWithJavaScriptFrom(ITarget)"/>.
    /// For very old versions of Firefox, this performable will throw an exception, as there is no supported way to get a Shadow Root.
    /// </para>
    /// </remarks>
    public class GetShadowRoot : IPerformableWithResult<SeleniumElement>, ICanReport
    {
        readonly ITarget element;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} gets the Shadow Root from {Element}", actor, element);

        /// <inheritdoc/>
        public ValueTask<SeleniumElement> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

            if(browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.CannotGetShadowRoot))
                throw new NotSupportedException("The current web browser is not capable of getting Shadow Roots via any known technique");

            if(browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.NeedsJavaScriptToGetShadowRoot))
                return actor.PerformAsync(GetTheShadowRootWithJavaScriptFrom(element), cancellationToken);
            
            return actor.PerformAsync(GetTheShadowRootNativelyFrom(element), cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GetShadowRoot"/>.
        /// </summary>
        /// <param name="element">The Shadow Host element</param>
        /// <exception cref="ArgumentNullException">If <paramref name="element"/> is <see langword="null"/></exception>
        public GetShadowRoot(ITarget element)
        {
            this.element = element ?? throw new ArgumentNullException(nameof(element));
        }
    }
}