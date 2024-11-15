using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor deselecting an option from a <c>&lt;select&gt;</c> element,
    /// where the option is specified by its underlying value.
    /// </summary>
    public class DeselectByValue : ISingleElementPerformable
    {
        readonly string value;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deselects the option with value {Value} from {Element}", actor.Name, value, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().DeselectByValue(value);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeselectByValue"/> class with the specified text.
        /// </summary>
        /// <param name="value">The underlying value of the option to deselect.</param>
        public DeselectByValue(string value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}