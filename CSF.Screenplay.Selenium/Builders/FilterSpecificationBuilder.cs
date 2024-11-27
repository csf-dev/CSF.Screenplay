using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.Questions;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides methods to build filter specifications for Selenium elements.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This builder class is intended to be consumed via a <c>using static CSF.Screenplay.Selenium.Builders.FilterSpecificationBuilder;</c> directive.
    /// It may then be used to create filter specifications for Selenium elements, such as within a usage of <see cref="FilterElementsBuilder"/>.
    /// </para>
    /// </remarks>
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
    /// <seealso cref="FilterElementsBuilder"/>
    /// <seealso cref="SeleniumPerformableBuilder.FilterTheElements(IReadOnlyCollection{SeleniumElement})"/>
    public static class FilterSpecificationBuilder
    {
        /// <summary>
        /// Creates a filter specification based on a specified HTML attribute and a predicate for the attribute's value.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <param name="predicate">The predicate to apply to the attribute value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> TheAttribute(string attributeName, Func<string,bool> predicate)
            => new FilterSpecification<string>(new AttributeQuery(attributeName), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on whether or not the element is clickable.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's "clickability".</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> Clickable(Func<bool,bool> predicate)
            => new FilterSpecification<bool>(new ClickableQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on a specified CSS propert and a predicate for the property's value.
        /// </summary>
        /// <param name="propertyName">The name of the property to query.</param>
        /// <param name="predicate">The predicate to apply to the property value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> TheCssProperty(string propertyName, Func<string,bool> predicate)
            => new FilterSpecification<string>(new CssPropertyQuery(propertyName), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's location (its top-left corner).
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's location.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> TheLocation(Func<Point,bool> predicate)
            => new FilterSpecification<Point>(new LocationQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's size (width &amp; height in pixels).
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's size.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> TheSize(Func<Size,bool> predicate)
            => new FilterSpecification<Size>(new SizeQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's text content.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's text.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> TheText(Func<string,bool> predicate)
            => new FilterSpecification<string>(new TextQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's <c>value</c> attribute.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> TheValue(Func<string,bool> predicate)
            => new FilterSpecification<string>(new ValueQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on whether or not the element is visible.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's visibility.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> Visible(Func<bool,bool> predicate)
            => new FilterSpecification<bool>(new VisibilityQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's selected options.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> SelectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => new FilterSpecification<IReadOnlyList<Option>>(new OptionsQuery(excludeUnselectedOptions: true), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's unselected options.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> UnselectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => new FilterSpecification<IReadOnlyList<Option>>(new OptionsQuery(excludeSelectedOptions: true), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's options.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> AnyOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => new FilterSpecification<IReadOnlyList<Option>>(new OptionsQuery(), Spec.Func(predicate));
    }
}