using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor deselecting all options from a <c>&lt;select&gt;</c> element.
    /// </summary>
    public class DeselectAll : ISingleElementPerformable
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deselects all of the options from {Element}", actor.Name, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().DeselectAll();
            return default;
        }
    }
}