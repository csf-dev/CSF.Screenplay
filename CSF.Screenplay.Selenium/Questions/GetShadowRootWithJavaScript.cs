using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A Screenplay Question which gets the Shadow Root element from the specified Selenium Element, using the simple JavaScript technique.
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
    /// The <see cref="SeleniumElement"/> passed to this performable as a parameter must be the Shadow Host element.
    /// </para>
    /// <para>
    /// This technique is known to work on older Chromium versions (before 96) and Safari.
    /// </para>
    /// </remarks>
    public class GetShadowRootWithJavaScript : ISingleElementPerformableWithResult<SeleniumElement>
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} gets the Shadow Root node from {Element} using JavaScript", actor, element.Value);

        /// <inheritdoc/>
        public string GetHumanReadableTypeName() => GetType().FullName;

        /// <inheritdoc/>
        public async ValueTask<SeleniumElement> PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            var shadowRoot = await actor.PerformAsync(ExecuteAScript(Scripts.GetShadowRoot, element.Value.WebElement), cancellationToken).ConfigureAwait(false);
            return new SeleniumElement(new ShadowRootAdapter(shadowRoot));
        }
    }
}