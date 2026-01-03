using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class which permits specifying an optional name for the collection of elements which match an element filter.
    /// </summary>
    public class NamedFilterElementsBuilder : IGetsPerformableWithResult<SeleniumElementCollection>
    {
        readonly IReadOnlyCollection<SeleniumElement> elements;
        readonly ISpecificationFunction<SeleniumElement> specification;

        /// <summary>
        /// Specifies a human-readable name for the collection of elements which are found matching the specification.
        /// </summary>
        /// <param name="resultsName">A short, descriptive, human-readable name for the collection of matching elements.</param>
        /// <returns>A performable question</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resultsName"/> is <see langword="null" />.</exception>
        public IPerformableWithResult<SeleniumElementCollection> AndNameThem(string resultsName)
        {
            if (resultsName is null)
                throw new ArgumentNullException(nameof(resultsName));

            return new FilterElements(elements, specification, resultsName);
        }

        IPerformableWithResult<SeleniumElementCollection> IGetsPerformableWithResult<SeleniumElementCollection>.GetPerformable()
        {
            return new FilterElements(elements, specification);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFilterElementsBuilder"/> class.
        /// </summary>
        /// <param name="elements">The elements to filter</param>
        /// <param name="specification">The specification by which to filter the elements</param>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public NamedFilterElementsBuilder(IReadOnlyCollection<SeleniumElement> elements, ISpecificationFunction<SeleniumElement> specification)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
            this.specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        /// <summary>
        /// Converts a <see cref="NamedFilterElementsBuilder"/> to a <see cref="FilterElements"/> question.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This conversion is only used when the <see cref="AndNameThem(string)"/> function is not used (the name is not specified).
        /// </para>
        /// </remarks>
        /// <param name="builder">The builder to convert.</param>
        public static implicit operator FilterElements(NamedFilterElementsBuilder builder)
            => new FilterElements(builder.elements, builder.specification);
    }
}