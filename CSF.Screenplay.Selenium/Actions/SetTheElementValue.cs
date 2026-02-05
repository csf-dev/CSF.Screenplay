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
        readonly bool simulateInteractiveSet;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
        {
            return simulateInteractiveSet
                ? formatter.Format("{Actor} uses JavaScript to simulate setting the value of {Element} to {Value} interactively", actor, element.Value, value)
                : formatter.Format("{Actor} uses JavaScript to set the value of {Element} to {Value}", actor, element.Value, value);
        }

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            return simulateInteractiveSet
                ? actor.PerformAsync(ExecuteAScript(Scripts.SetElementValueSimulatedInteractively, element.Value.WebElement, value), cancellationToken)
                : actor.PerformAsync(ExecuteAScript(Scripts.SetElementValue, element.Value.WebElement, value), cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTheElementValue"/> class.
        /// </summary>
        /// <param name="value">The value to set on the element.</param>
        /// <param name="simulateInteractiveSet">If <see langword="true"/> then the JavaScript will fire UI events to simulate having set the element interactively</param>
        public SetTheElementValue(object value, bool simulateInteractiveSet)
        {
            this.value = value;
            this.simulateInteractiveSet = simulateInteractiveSet;
        }
    }
}