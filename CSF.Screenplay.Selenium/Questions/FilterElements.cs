using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A performable question which filters a collection of elements by specified criteria.
    /// </summary>
    public class FilterElements : IPerformableWithResult<SeleniumElementCollection>, ICanReport
    {
        readonly IReadOnlyCollection<SeleniumElement> elements;
        readonly ISpecificationFunction<SeleniumElement> specification;
        readonly string resultsName;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
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