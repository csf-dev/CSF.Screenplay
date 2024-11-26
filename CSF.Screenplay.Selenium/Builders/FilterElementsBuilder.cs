using System.Collections.Generic;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class for filtering Selenium elements based on a specification.
    /// </summary>
    public class FilterElementsBuilder
    {
        readonly IReadOnlyCollection<SeleniumElement> elements;

        /// <summary>
        /// Specifies the specification which will be used to filter the collection of elements.
        /// </summary>
        /// <param name="specification">The specification function to filter elements.</param>
        /// <returns>A builder which permits naming the filtered elements.</returns>
        public NamedFilterElementsBuilder ForThoseWhichMatch(ISpecificationFunction<SeleniumElement> specification)
            => new NamedFilterElementsBuilder(elements, specification);

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterElementsBuilder"/> class.
        /// </summary>
        /// <param name="elements">The collection of Selenium elements to be filtered.</param>
        public FilterElementsBuilder(IReadOnlyCollection<SeleniumElement> elements)
        {
            this.elements = elements ?? throw new System.ArgumentNullException(nameof(elements));
        }
    }
}