using System;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A specification class which matches elements based upon the value of a query and a specification for that value.
    /// </summary>
    public class FilterSpecification<T> : ISpecificationFunction<SeleniumElement>
    {
        readonly IQuery<T> query;
        readonly ISpecificationFunction<T> valueSpec;

        /// <inheritdoc/>
        public Func<SeleniumElement, bool> GetFunction()
        {
            return element => {
                var value = query.GetValue(element);
                return valueSpec.Matches(value);
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterSpecification{T}"/> class.
        /// </summary>
        /// <param name="query">The query to get the value from the element.</param>
        /// <param name="valueSpec">The specification function to match the value.</param>
        public FilterSpecification(IQuery<T> query, ISpecificationFunction<T> valueSpec)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.valueSpec = valueSpec ?? throw new ArgumentNullException(nameof(valueSpec));
        }
    }
}