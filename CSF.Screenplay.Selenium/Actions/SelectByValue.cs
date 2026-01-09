using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor selecting an option from a <c>&lt;select&gt;</c> element,
    /// where the option is specified by its underlying value.
    /// </summary>
    public class SelectByValue : ISingleElementPerformable
    {
        readonly string value;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} selects the option with value {Value} from {Element}", actor.Name, value, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().SelectByValue(value);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectByValue"/> class with the specified text.
        /// </summary>
        /// <param name="value">The underlying value of the option to select.</param>
        public SelectByValue(string value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}