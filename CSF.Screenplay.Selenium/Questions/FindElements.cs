using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Represents a question that finds a collection of elements within a specified target.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This question is used to find a collection of elements within a target.  The matched/returned elements may be filtered
    /// by a <see cref="Locator"/> if one is specified, otherwise all descendent elements of the target will be returned.
    /// </para>
    /// <para>
    /// Consumers are encouraged to specify a human-readable name for the collection of elements which are found, this will be
    /// kept with the returned collection of elements and will be visible in reports.
    /// </para>
    /// </remarks>
    public class FindElements : ISingleElementPerformableWithResult<SeleniumElementCollection>
    {
        readonly string elementsName;
        readonly Locator locatorBasedMatcher;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} finds {ElementsName} from {Element}", actor, GetElementsName(element.Value) ?? "HTML elements", element.Value);

        /// <inheritdoc/>
        public ValueTask<SeleniumElementCollection> PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            var elements = element.Value.WebElement.FindElements(locatorBasedMatcher ?? CssSelector.AnyElement);
            return new ValueTask<SeleniumElementCollection>(new SeleniumElementCollection(elements, GetElementsName(element.Value)));
        }

        string GetElementsName(SeleniumElement element) => elementsName ?? $"{locatorBasedMatcher?.Name} within {element.Name}";

        /// <summary>
        /// Initializes a new instance of the <see cref="FindElements"/> class.
        /// </summary>
        /// <param name="elementsName">An optional short, descriptive, human-readable name to give to the collection of elements which are found.</param>
        /// <param name="locatorBasedMatcher">An optional <see cref="Locator"/> which should be used to filter the elements which are returned.</param>
        public FindElements(string elementsName = null, Locator locatorBasedMatcher = null)
        {
            this.elementsName = elementsName;
            this.locatorBasedMatcher = locatorBasedMatcher;
        }
    }
}