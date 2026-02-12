using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which searches for HTML elements that matche some criteria, optionally within a specified target,
    /// returning the results as a <see cref="SeleniumElementCollection"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this question via either of the builder methods <see cref="PerformableBuilder.FindElementsWithin(ITarget)"/>
    /// or <see cref="PerformableBuilder.FindElementsOnThePage"/>. The first searches within a specified target,
    /// the second searches within the whole page <c>&lt;body&gt;</c>. This question returns a collection of elements
    /// but that collection could be empty if the search does not find any matching elements.
    /// If you are looking for a single element and a 'nothing found' result should raise an exception then
    /// consider using the <see cref="FindElement"/> question instead.
    /// </para>
    /// <para>
    /// The criteria by which an element is searched by this question is a class that derives from <see cref="Locator"/>.
    /// Particularly useful are the <see cref="CssSelector"/>, <see cref="ClassName"/> and <see cref="XPath"/>
    /// locators.  <see cref="ElementId"/> is less likely to be useful, as it should only ever match a single element per
    /// web page.
    /// </para>
    /// <include file="ElementQueryDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Questions.ISingleElementPerformableWithResult`1']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example gets a <see cref="SeleniumElementCollection"/> which contains every element in the list which has
    /// the ID <c>todo</c>, which also the class <c>low_priority</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget todoList = new CssSelector("ul#todo", "the to-do list");
    /// readonly Locator lowPriority = new ClassName("low_priority", "the low priority items");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;SeleniumElementCollection&gt;
    /// public async ValueTask&lt;SeleniumElementCollection&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     var elements = await actor.PerformAsync(FindElementsWithin(todoList).WhichMatch(lowPriority), cancellationToken);
    ///     // ... other performance logic
    ///     return elements;
    /// }
    /// </code>
    /// </example>

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