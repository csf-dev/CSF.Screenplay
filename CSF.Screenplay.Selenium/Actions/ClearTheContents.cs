using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Represents an action to clear the contents of a specified HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The element to be cleared is typically an HTML <c>&lt;input&gt;</c> or <c>&lt;textarea&gt;</c> element.
    /// It could be any element which supports Webdriver's <c>Clear()</c> method.
    /// For input or textarea elements, this action is equivalent to the user selecting all text and pressing the delete key.
    /// </para>
    /// </remarks>
    public class ClearTheContents : ISingleElementPerformable
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clears the contents of {Element}", actor.Name, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.WebElement.Clear();
            return default;
        }
    }
}