using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action that sets the value of a web element using JavaScript.
    /// </summary>
    public class SetTheElementValue : ISingleElementPerformable
    {
        readonly object value;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} uses JavaScript to set the value of {Element} to {Value}", actor, element.Value, value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            return actor.PerformAsync(ExecuteAScript(Scripts.SetElementValue, element.Value, value), cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTheElementValue"/> class.
        /// </summary>
        /// <param name="value">The value to set on the element.</param>
        public SetTheElementValue(object value)
        {
            this.value = value;
        }
    }
}