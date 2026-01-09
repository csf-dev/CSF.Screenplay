using System;
using System.Threading;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class for filtering Selenium elements based on a specification.
    /// </summary>
    /// <example>
    /// <para>
    /// Here is a sample usage which combines both the <see cref="QueryPredicatePrototypeBuilder"/> and <see cref="FilterElementsBuilder"/> classes:
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// var buyNowButtons = await actor.PerformAsync(Filter(someElements).ForThoseWhich(have => have.Clickability(true).And(have.Text("Buy now")), cancellationToken);
    /// </code>
    /// <para>
    /// The code sample above assumes that <c>actor</c> is an instance of <see cref="ICanPerform"/>, that <c>someElements</c> is a collection
    /// of <see cref="SeleniumElement"/> instances, and that <c>cancellationToken</c> is a <see cref="CancellationToken"/> instance.
    /// It would filter the elements in <c>someElements</c> to only those which are clickable and have the text "Buy now".
    /// </para>
    /// </example>
    /// <seealso cref="QueryPredicatePrototypeBuilder"/>
    /// <seealso cref="PerformableBuilder.Filter(SeleniumElementCollection)"/>
    public class FilterElementsBuilder
    {
        readonly SeleniumElementCollection elements;

        /// <summary>
        /// Specifies the specification which will be used to filter the collection of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Consider using the <see cref="QueryPredicatePrototypeBuilder"/> class to create the specification function
        /// which is passed to this method.  Do this via the other overload to this method..
        /// </para>
        /// </remarks>
        /// <param name="specification">The specification function to filter elements.</param>
        /// <returns>A builder which permits naming the filtered elements.</returns>
        public NamedFilterElementsBuilder ForThoseWhich(ISpecificationFunction<SeleniumElement> specification)
            => new NamedFilterElementsBuilder(elements, specification);

        /// <summary>
        /// Specifies the specification which will be used to filter the collection of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload uses the <see cref="QueryPredicatePrototypeBuilder"/> class to create the specification function
        /// which is passed to this method.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// Here is a sample usage which combines both the <see cref="QueryPredicatePrototypeBuilder"/> and <see cref="FilterElementsBuilder"/> classes:
        /// </para>
        /// <code>
        /// using static CSF.Screenplay.Selenium.PerformableBuilder;
        /// 
        /// var buyNowButtons = await actor.PerformAsync(Filter(someElements).ForThoseWhich(have => have.Clickability(true).And(have.Text("Buy now")), cancellationToken);
        /// </code>
        /// <para>
        /// The code sample above assumes that <c>actor</c> is an instance of <see cref="ICanPerform"/>, that <c>someElements</c> is a collection
        /// of <see cref="SeleniumElement"/> instances, and that <c>cancellationToken</c> is a <see cref="CancellationToken"/> instance.
        /// It would filter the elements in <c>someElements</c> to only those which are clickable and have the text "Buy now".
        /// </para>
        /// </example>
        /// <param name="predicatePrototype">A lambda function which builds a predicate prototype from a fluent interface.</param>
        /// <returns>A builder which permits naming the filtered elements.</returns>
        public NamedFilterElementsBuilder ForThoseWhich(Func<QueryPredicatePrototypeBuilder, IBuildsElementPredicates> predicatePrototype)
        {
            var target = new SeleniumElementCollection(elements);
            var prototypeBuilder = new QueryPredicatePrototypeBuilder(target, true);
            var namedBuilder = predicatePrototype(prototypeBuilder);
            return new NamedFilterElementsBuilder(elements, namedBuilder.GetElementSpecification());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterElementsBuilder"/> class.
        /// </summary>
        /// <param name="elements">The collection of Selenium elements to be filtered.</param>
        public FilterElementsBuilder(SeleniumElementCollection elements)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }
    }
}