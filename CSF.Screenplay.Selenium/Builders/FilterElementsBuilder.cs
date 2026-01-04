using System.Collections.Generic;
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
    /// Here is a sample usage which combines both the <see cref="FilterSpecificationBuilder"/> and <see cref="FilterElementsBuilder"/> classes:
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.SeleniumPerformableBuilder;
    /// using static CSF.Screenplay.Selenium.Builders.FilterSpecificationBuilder;
    /// 
    /// await actor.PerformAsync(FilterTheElements(someElements).ForThoseWhichAre(Clickable(x => x).And(TheText(x => x == "Buy now")), cancellationToken);
    /// </code>
    /// <para>
    /// The code sample above assumes that <c>actor</c> is an instance of <see cref="ICanPerform"/>, that <c>someElements</c> is a collection
    /// of <see cref="SeleniumElement"/> instances, and that <c>cancellationToken</c> is a <see cref="CancellationToken"/> instance.
    /// It would filter the elements in <c>someElements</c> to only those which are clickable and have the text "Buy now".
    /// </para>
    /// </example>
    /// <seealso cref="FilterSpecificationBuilder"/>
    /// <seealso cref="PerformableBuilder.FilterTheElements(IReadOnlyCollection{SeleniumElement})"/>
    public class FilterElementsBuilder
    {
        readonly IReadOnlyCollection<SeleniumElement> elements;

        /// <summary>
        /// Specifies the specification which will be used to filter the collection of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method and <see cref="ForThoseWhichAre(ISpecificationFunction{SeleniumElement})"/> are equivalent synonyms.
        /// They are provided to allow for more readable code.
        /// </para>
        /// <para>
        /// Consider using the <see cref="FilterSpecificationBuilder"/> class to create the specification function
        /// which is passed to this method.  Do this via <c>using static CSF.Screenplay.Selenium.Builders.FilterSpecificationBuilder;</c>.
        /// This permits a fluent syntax such as <c>.ForThoseWhichHave(TheAttribute("class", c => c.Contains("myClass")))</c>.
        /// </para>
        /// </remarks>
        /// <param name="specification">The specification function to filter elements.</param>
        /// <returns>A builder which permits naming the filtered elements.</returns>
        public NamedFilterElementsBuilder ForThoseWhichHave(ISpecificationFunction<SeleniumElement> specification)
            => new NamedFilterElementsBuilder(elements, specification);

        /// <summary>
        /// Specifies the specification which will be used to filter the collection of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method and <see cref="ForThoseWhichHave(ISpecificationFunction{SeleniumElement})"/> are equivalent synonyms.
        /// They are provided to allow for more readable code.
        /// </para>
        /// <para>
        /// Consider using the <see cref="FilterSpecificationBuilder"/> class to create the specification function
        /// which is passed to this method.  Do this via <c>using static CSF.Screenplay.Selenium.Builders.FilterSpecificationBuilder;</c>.
        /// This permits a fluent syntax such as <c>.ForThoseWhichAre(TheAttribute("class", c => c.Contains("myClass")))</c>.
        /// </para>
        /// </remarks>
        /// <param name="specification">The specification function to filter elements.</param>
        /// <returns>A builder which permits naming the filtered elements.</returns>
        public NamedFilterElementsBuilder ForThoseWhichAre(ISpecificationFunction<SeleniumElement> specification)
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