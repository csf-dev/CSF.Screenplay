using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A <see cref="ISingleElementPerformable"/> which represents an actor clicking upon a single HTML element.
    /// </summary>
    public class Click : ISingleElementPerformable
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clicks on {Element}", actor.Name, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.WebElement.Click();
            return default;
        }
    }
}