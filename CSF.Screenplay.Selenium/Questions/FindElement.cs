using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Represents a question that finds a single element within a specified target.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This question is used to find a single element within a target. If more than one element would have been matched by the specified locator
    /// then the first matching element (only) will be returned.
    /// </para>
    /// <para>
    /// Consumers are encouraged to specify a human-readable name for the collection of elements which are found, this will be
    /// kept with the returned collection of elements and will be visible in reports.
    /// </para>
    /// <para>
    /// Note that if no element is found, which is a descendent of the target and which is matched by the locator, then the
    /// <see cref="PerformAsAsync(ICanPerform, IWebDriver, Lazy{SeleniumElement}, CancellationToken)"/> method of this question will return
    /// a value task of a <see langword="null" /> reference.
    /// </para>
    /// </remarks>
    public class FindElement : ISingleElementPerformableWithResult<SeleniumElement>
    {
        readonly string elementsName;
        readonly Locator locatorBasedMatcher;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} finds {ElementsName} from {Element}", actor, GetElementsName(element.Value) ?? "HTML elements", element.Value);

        /// <inheritdoc/>
        public ValueTask<SeleniumElement> PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            var webElement = element.Value.WebElement.FindElement(locatorBasedMatcher ?? CssSelector.AnyElement);
            return new ValueTask<SeleniumElement>(new SeleniumElement(webElement, GetElementsName(element.Value)));
        }

        string GetElementsName(SeleniumElement element) => elementsName ?? $"{locatorBasedMatcher?.Name} within {element.Name}";

        /// <summary>
        /// Initializes a new instance of the <see cref="FindElement"/> class.
        /// </summary>
        /// <param name="elementsName">An optional short, descriptive, human-readable name to give to the collection of elements which are found.</param>
        /// <param name="locatorBasedMatcher">An optional <see cref="Locator"/> which should be used to filter the elements which are returned.</param>
        public FindElement(string elementsName = null, Locator locatorBasedMatcher = null)
        {
            this.elementsName = elementsName;
            this.locatorBasedMatcher = locatorBasedMatcher;
        }
    }
}