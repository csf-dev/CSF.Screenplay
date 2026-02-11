using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A partial Screenplay Action which sends keys (enters text) to an HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.EnterTheText(string[])"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to press the specified keys, essentially entering text into the HTML element indicated by
    /// <xref href="SeleniumElementsAndTargetsArticle?text=the+target"/>.
    /// </para>
    /// <para>
    /// Typically, this action is performed upon an element which supports user-input such as an
    /// <c>&lt;input&gt;</c> or <c>&lt;textarea&gt;</c> element. However, it is not limited to these elements; any element
    /// may receive key-presses.
    /// </para>
    /// <para>
    /// You may send non-printable or special keys such as Enter or directional arrow key-presses by
    /// using Selenium's <c>Keys</c> class.
    /// </para>
    /// <include file="SingleElementPerformableDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Actions.ISingleElementPerformable']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example types "Jane Doe" into the element with ID <c>name</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget name = new ElementId("name", "the name field");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(EnterTheText("Jane Doe").Into(name), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class SendKeys : ISingleElementPerformable
    {
        readonly string text;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} enters the text '{Text}' into {Element}", actor.Name, text, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.WebElement.SendKeys(text);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendKeys"/> class with the specified text.
        /// </summary>
        /// <param name="text">The text to enter into the element.</param>
        public SendKeys(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}