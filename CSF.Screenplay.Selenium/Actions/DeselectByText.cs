using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor deselecting an option from a <c>&lt;select&gt;</c> element,
    /// where the option is specified by its human-readable display text.
    /// </summary>
    public class DeselectByText : ISingleElementPerformable
    {
        readonly string text;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deselects '{Text}' from {Element}", actor.Name, text, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().DeselectByText(text);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeselectByText"/> class with the specified text.
        /// </summary>
        /// <param name="text">The human-readable text of the option to deselect.</param>
        public DeselectByText(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}