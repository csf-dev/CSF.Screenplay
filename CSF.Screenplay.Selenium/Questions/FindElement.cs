using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which searches for an HTML element that matches some criteria, optionally within a specified target,
    /// returning the element it finds as a <see cref="SeleniumElement"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this question via either of the builder methods <see cref="PerformableBuilder.FindAnElementWithin(Locator)"/>,
    /// <see cref="PerformableBuilder.FindAnElementWithin(IHasSearchContext)"/> or <see cref="PerformableBuilder.FindAnElementOnThePage"/>.
    /// The first two search within a specified target,
    /// the third searches within the whole page <c>&lt;body&gt;</c>. This question will only ever return a single
    /// <see cref="SeleniumElement"/>, or it will raise an exception if the search does not find any matching elements.
    /// If multiple elements are found which match the criteria then this question will return only the first.
    /// If you are expecting to find multiple elements, then consider using the <see cref="FindElements"/> question
    /// instead.
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
    /// This example gets a <see cref="SeleniumElement"/> within the list which has the ID <c>todo</c> which has the class
    /// <c>urgent</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget todoList = new CssSelector("ul#todo", "the to-do list");
    /// readonly Locator urgent = new ClassName("urgent", "the urgent item");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;SeleniumElement&gt;
    /// public async ValueTask&lt;SeleniumElement&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     var element = await actor.PerformAsync(FindAnElementWithin(todoList).WhichMatches(urgent), cancellationToken);
    ///     // ... other performance logic
    ///     return element;
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.FindAnElementOnThePage"/>
    /// <seealso cref="PerformableBuilder.FindAnElementWithin(Locator)"/>
    /// <seealso cref="PerformableBuilder.FindAnElementWithin(IHasSearchContext)"/>
    public class FindElement : IPerformableWithResult<SeleniumElement>, ICanReport
    {
        readonly ITarget target;
        readonly IHasSearchContext searchContext;
        readonly string elementsName;
        readonly Locator locatorBasedMatcher;
        Lazy<IHasSearchContext> lazyElement;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        {
            lazyElement = lazyElement ?? GetLazyElement(actor);
            return formatter.Format("{Actor} finds {ElementsName} within {Element}", actor, GetElementsName(), lazyElement);
        }

        /// <inheritdoc/>
        public ValueTask<SeleniumElement> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            lazyElement = lazyElement ?? GetLazyElement(actor);
            var element = lazyElement.Value.SearchContext.FindElement(locatorBasedMatcher ?? CssSelector.AnyElement);
            return new ValueTask<SeleniumElement>(new SeleniumElement(element, GetElementsName()));
        }

        string GetElementsName() => elementsName != null ? elementsName : (locatorBasedMatcher?.Name ?? "HTML elements");

        Lazy<IHasSearchContext> GetLazyElement(ICanPerform actor)
        {
            if(target != null) return new Lazy<IHasSearchContext>(() => actor.GetLazyElement(target).Value);
            return new Lazy<IHasSearchContext>(() => searchContext);
        }

        /// <inheritdoc/>
        public string GetHumanReadableTypeName() => GetType().FullName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindElement"/> class.
        /// </summary>
        /// <param name="target">A target which describes an element</param>
        /// <param name="elementsName">An optional short, descriptive, human-readable name to give to the collection of elements which are found.</param>
        /// <param name="locatorBasedMatcher">An optional <see cref="Locator"/> which should be used to filter the elements which are returned.</param>
        public FindElement(ITarget target, string elementsName = null, Locator locatorBasedMatcher = null)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.elementsName = elementsName;
            this.locatorBasedMatcher = locatorBasedMatcher;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindElement"/> class.
        /// </summary>
        /// <param name="searchContext">An object which provides a search context, within which we can find elements</param>
        /// <param name="elementsName">An optional short, descriptive, human-readable name to give to the collection of elements which are found.</param>
        /// <param name="locatorBasedMatcher">An optional <see cref="Locator"/> which should be used to filter the elements which are returned.</param>
        public FindElement(IHasSearchContext searchContext, string elementsName = null, Locator locatorBasedMatcher = null)
        {
            this.searchContext = searchContext ?? throw new ArgumentNullException(nameof(searchContext));
            this.elementsName = elementsName;
            this.locatorBasedMatcher = locatorBasedMatcher;
        }
    }
}