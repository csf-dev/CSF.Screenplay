using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which filters a collection of elements by specified criteria, getting a new collection
    /// which contains only those which match.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this question via the builder method <see cref="PerformableBuilder.Filter(SeleniumElementCollection)"/>.
    /// The builder will then guide you through specifying the
    /// criteria by which you wish to filter those elements. There are two primary routes by which to do this,
    /// which depend upon which overload of <c>ForThoseWhich</c> you choose.
    /// </para>
    /// <para>
    /// You may specify the criteria within a Specification class.  That is a class which implements
    /// <c>ISpecificationFunction&lt;SeleniumElement&gt;</c>, from
    /// <see href="https://github.com/csf-dev/CSF.Specifications">the CSF.Specifications</see> package.
    /// This allows you to pass just a variable into the builder, containing an instance of that specification.
    /// This is the recommended technique, as the specification class becomes a reusable and first-class named
    /// part of the Screenplay.
    /// When using this technique, use the <see cref="Builders.FilterElementsBuilder.ForThoseWhich(ISpecificationFunction{SeleniumElement})"/>
    /// overload.
    /// </para>
    /// <para>
    /// Alternatively, you may specify the criteria ad-hoc within the filter-elements builder.
    /// To use this technique, use the
    /// <see cref="Builders.FilterElementsBuilder.ForThoseWhich(System.Func{Builders.QueryPredicatePrototypeBuilder, Builders.IBuildsElementPredicates})"/>
    /// overload.  You must specify a function which interrogates one or more aspects of the element, specifying each
    /// criterion individually. This approach allows fast specification of criteria, but at the cost of reusability.
    /// </para>
    /// <para>
    /// Note that this question is evaluated with an already-in-memory collection of <see cref="SeleniumElement"/>.
    /// However, each criterion that is evaluated may cause communication with the WebDriver, for each element in the source collection.
    /// This nested loop could lead to unexpected impacts upon performance, particularly with larger collections of elements
    /// or where the WebDriver is hosted remotely.
    /// If you are able to find the elements you want using an implementation of <see cref="Locator"/>, then prefer
    /// using <see cref="FindElements"/> instead.  Finding elements performs far better than this question.
    /// On the other hand, this question offers far greater power in the creation of custom criteria.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// This example filters a collection of items in a todo list, only for those with red background colors.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium;
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget todoListItems = new CssSelector("ul#todo li", "the to-do list items");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;SeleniumElementCollection&gt;
    /// public async ValueTask&lt;SeleniumElementCollection&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     var elements = await actor.PerformAsync(FindElementsOnThePage().WhichMatch(todoListItems), cancellationToken);
    ///     var redElements = await actor.PerformAsync(Filter(elements)
    ///                                                 .ForThoseWhich(q => q.CssProperty("background-color",
    ///                                                                                   x => x == Colors.RED)),
    ///                                                cancellationToken);
    ///     return redElements;
    /// }
    /// </code>
    /// </example>
    public class FilterElements : IPerformableWithResult<SeleniumElementCollection>, ICanReport
    {
        readonly IReadOnlyCollection<SeleniumElement> elements;
        readonly ISpecificationFunction<SeleniumElement> specification;
        readonly string resultsName;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        {
            if(resultsName is null)
                return formatter.Format("{Actor} filters {ElementsName}", actor, GetElementsName());

            return formatter.Format("{Actor} filters {ElementsName} to get {ResultsName}", actor, GetElementsName(), resultsName);
        }

        string GetElementsName() => (elements is IHasName namedCollecton) ? namedCollecton.Name : string.Format(SeleniumElementCollection.UnknownNameFormat, elements.Count);

        /// <inheritdoc/>
        public ValueTask<SeleniumElementCollection> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
            => new ValueTask<SeleniumElementCollection>(new SeleniumElementCollection(elements.Where(specification).ToList(), resultsName));

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterElements"/> class.
        /// </summary>
        /// <param name="elements">The collection of elements to be filtered.</param>
        /// <param name="specification">The specification function to filter the elements.</param>
        /// <param name="resultsName">An optional short, descriptive, human-readable name which summarizes the elements matched by the filter.</param>
        public FilterElements(IReadOnlyCollection<SeleniumElement> elements,
                              ISpecificationFunction<SeleniumElement> specification,
                              string resultsName = null)
        {
            this.elements = elements ?? throw new System.ArgumentNullException(nameof(elements));
            this.specification = specification ?? throw new System.ArgumentNullException(nameof(specification));
            this.resultsName = resultsName;
        }
    }
}