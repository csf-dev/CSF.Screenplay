using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor pressing some keys whilst focussed upon a single HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable may be used to enter text into an element which supports it, such as an <c>&lt;input&gt;</c> or <c>&lt;textarea&gt;</c> element.
    /// It may also be used, with Selenium's <c>Keys</c> class, to send special keys such as the Enter key, directional Arrow keys and so on.
    /// </para>
    /// </remarks>
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