using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor selecting an option from a <c>&lt;select&gt;</c> element,
    /// where the option is specified by its human-readable display text.
    /// </summary>
    public class SelectByText : ISingleElementPerformable
    {
        readonly string text;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} selects '{Text}' from {Element}", actor.Name, text, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().SelectByText(text);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectByText"/> class with the specified text.
        /// </summary>
        /// <param name="text">The human-readable text of the option to select.</param>
        public SelectByText(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}