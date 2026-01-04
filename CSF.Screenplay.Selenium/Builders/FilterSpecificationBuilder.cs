using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
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
    /// await actor.PerformAsync(FilterTheElements(someElements).ForThoseWhich(AreClickable().And(HaveText("Buy now")), cancellationToken);
    /// </code>
    /// <para>
    /// The code sample above assumes that <c>actor</c> is an instance of <see cref="ICanPerform"/>, that <c>someElements</c> is a collection
    /// of <see cref="SeleniumElement"/> instances, and that <c>cancellationToken</c> is a <see cref="CancellationToken"/> instance.
    /// It would filter the elements in <c>someElements</c> to only those which are clickable and have the text "Buy now".
    /// </para>
    /// </example>
    /// <seealso cref="FilterElementsBuilder"/>
    /// <seealso cref="PerformableBuilder.FilterTheElements(IReadOnlyCollection{SeleniumElement})"/>
    public static class FilterSpecificationBuilder
    {
        const string classAttribute = "class";

        /// <summary>
        /// Creates a filter specification based on a specified HTML attribute and a predicate for the attribute's value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this specification makes use of Selenium's <c>GetAttribute</c> method.
        /// The behaviour of that method in fact queries the DOM property of the element first, before querying the HTML attribute.
        /// It also has special handling for certain attributes such as <c>class</c> &amp; <c>readonly</c> attributes (substituting them with <c>className</c>
        /// and <c>readOnly</c> respectively when querying DOM properties).
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// Note that the last behaviour, described above, means that this method may be used to match elements which <em>do not</em> have the specified attribute,
        /// by using a predicate which checks for <see langword="null"/>.
        /// </para>
        /// </remarks>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <param name="predicate">The predicate to apply to the attribute value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveAttributeValue(string attributeName, Func<string,bool> predicate)
            => new FilterSpecification<string>(new AttributeQuery(attributeName), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification for the presence of a specified HTML attribute, with a specified value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this specification makes use of Selenium's <c>GetAttribute</c> method.
        /// The behaviour of that method in fact queries the DOM property of the element first, before querying the HTML attribute.
        /// It also has special handling for certain attributes such as <c>class</c> &amp; <c>readonly</c> attributes (substituting them with <c>className</c>
        /// and <c>readOnly</c> respectively when querying DOM properties).
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// </remarks>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <param name="value">The value of the attribute to query.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveAttributeValue(string attributeName, string value)
            => HaveAttributeValue(attributeName, x => x == value);

        /// <summary>
        /// Creates a filter specification for the presence of a specified HTML attribute.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this specification makes use of Selenium's <c>GetAttribute</c> method.
        /// The behaviour of that method in fact queries the DOM property of the element first, before querying the HTML attribute.
        /// It also has special handling for certain attributes such as <c>class</c> &amp; <c>readonly</c> attributes (substituting them with <c>className</c>
        /// and <c>readOnly</c> respectively when querying DOM properties).
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// </remarks>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveAttribute(string attributeName)
            => HaveAttributeValue(attributeName, x => x != null);

        /// <summary>
        /// Creates a filter specification for the presence of a specified HTML class (amongst the class attribute's values).
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this specification makes use of Selenium's <c>GetAttribute</c> method.
        /// The behaviour of that method in fact queries the DOM property of the element first, before querying the HTML attribute.
        /// It also has special handling for certain attributes such as <c>class</c> &amp; <c>readonly</c> attributes (substituting them with <c>className</c>
        /// and <c>readOnly</c> respectively when querying DOM properties).
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// </remarks>
        /// <param name="class">The name of the class to query.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveClass(string @class)
            => HaveAttributeValue(classAttribute, attribValue => HasClass(attribValue, @class));

        /// <summary>
        /// Creates a filter specification for the presence of all the specified HTML classes (amongst the class attribute's values).
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this specification makes use of Selenium's <c>GetAttribute</c> method.
        /// The behaviour of that method in fact queries the DOM property of the element first, before querying the HTML attribute.
        /// It also has special handling for certain attributes such as <c>class</c> &amp; <c>readonly</c> attributes (substituting them with <c>className</c>
        /// and <c>readOnly</c> respectively when querying DOM properties).
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// </remarks>
        /// <param name="classes">The names of the classes to query.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveAllClasses(params string[] classes)
            => HaveAttributeValue(classAttribute, attribValue => classes.All(@class => HasClass(attribValue, @class)));

        /// <summary>
        /// Gets a value indicating whether the <paramref name="attributeValue"/> (representing an HTML <c>class</c> attribute)
        /// contains the specified <paramref name="class"/>.
        /// </summary>
        /// <param name="attributeValue">The HTML class attribute value.</param>
        /// <param name="class">The class for which to search.</param>
        /// <returns><see langword="true"/> if the class is present; otherwise <see langword="false"/>.</returns>
        static bool HasClass(string attributeValue, string @class)
        {
            return Regex.IsMatch(attributeValue,
                                 @"\b" + Regex.Escape(@class) + @"\b",
                                 RegexOptions.CultureInvariant,
                                 // DoS prevention: 20ms is more than enough, given this should be a simple regex.
                                 TimeSpan.FromMilliseconds(20));
        }

        /// <summary>
        /// Creates a filter specification based on whether or not the element is clickable.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's "clickability".</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveClickability(Func<bool,bool> predicate)
            => new FilterSpecification<bool>(new ClickableQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on whether the element is clickable.
        /// </summary>
        /// <param name="value">The desired value for the element's "clickability", defaults to <see langword="true"/> if omitted.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> AreClickable(bool value = true)
            => HaveClickability(x => x == value);

        /// <summary>
        /// Creates a filter specification based on a specified CSS property and a predicate for the property's value.
        /// </summary>
        /// <param name="propertyName">The name of the property to query.</param>
        /// <param name="predicate">The predicate to apply to the property value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveCssProperty(string propertyName, Func<string,bool> predicate)
            => new FilterSpecification<string>(new CssPropertyQuery(propertyName), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on a specified CSS property and a desired value for that property.
        /// </summary>
        /// <param name="propertyName">The name of the property to query.</param>
        /// <param name="value">The value to compare against the property value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveCssProperty(string propertyName, string value)
            => HaveCssProperty(propertyName, x => x == value);

        /// <summary>
        /// Creates a filter specification based on the element's location (its top-left corner).
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's location.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveLocation(Func<Point,bool> predicate)
            => new FilterSpecification<Point>(new LocationQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's location (its top-left corner).
        /// </summary>
        /// <param name="value">The value to compare against the element's location.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveLocation(Point value)
            => HaveLocation(x => x == value);

        /// <summary>
        /// Creates a filter specification based on the element's size (width &amp; height in pixels).
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's size.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveSize(Func<Size,bool> predicate)
            => new FilterSpecification<Size>(new SizeQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's size (width &amp; height in pixels).
        /// </summary>
        /// <param name="value">The value to compare against the element's size.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveSize(Size value)
            => HaveSize(x => x == value);

        /// <summary>
        /// Creates a filter specification based on the element's text content.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's text.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveText(Func<string,bool> predicate)
            => new FilterSpecification<string>(new TextQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's text content.
        /// </summary>
        /// <param name="value">The value to compare against the element's text.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveText(string value)
            => HaveText(x => x == value);

        /// <summary>
        /// Creates a filter specification based on the element's DOM <c>value</c>.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's DOM value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveValue(Func<string,bool> predicate)
            => new FilterSpecification<string>(new ValueQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the element's DOM <c>value</c>.
        /// </summary>
        /// <param name="value">The value to compare against the element's DOM value.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveValue(string value)
            => HaveValue(x => x == value);

        /// <summary>
        /// Creates a filter specification based on whether or not the element is visible.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's visibility.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveVisibility(Func<bool,bool> predicate)
            => new FilterSpecification<bool>(new VisibilityQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on whether or not the element is visible.
        /// </summary>
        /// <param name="value">The desired value for the element's visibility", defaults to <see langword="true"/> if omitted.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> AreVisible(bool value = true)
            => HaveVisibility(x => x == value);

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's selected options.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveSelectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => new FilterSpecification<IReadOnlyList<Option>>(new OptionsQuery(excludeUnselectedOptions: true), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its selected options are precisely those specified by <paramref name="optionTexts"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their display text.
        /// </para>
        /// </remarks>
        /// <param name="optionTexts">The display text of the options to match against.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveSelectedOptionsByText(params string[] optionTexts)
            => HaveSelectedOptions(x => new HashSet<string>(x.Select(o => o.Text)).SetEquals(optionTexts));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its selected options are precisely those specified by <paramref name="optionValues"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their DOM value.
        /// </para>
        /// </remarks>
        /// <param name="optionValues">The DOM values of the options to match against.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveSelectedOptionsByValue(params string[] optionValues)
            => HaveSelectedOptions(x => new HashSet<string>(x.Select(o => o.Value)).SetEquals(optionValues));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's unselected options.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveUnselectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => new FilterSpecification<IReadOnlyList<Option>>(new OptionsQuery(excludeSelectedOptions: true), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its unselected options are precisely those specified by <paramref name="optionTexts"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their display text.
        /// </para>
        /// </remarks>
        /// <param name="optionTexts">The display text of the options to match against.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveUnselectedOptionsByText(params string[] optionTexts)
            => HaveUnselectedOptions(x => new HashSet<string>(x.Select(o => o.Text)).SetEquals(optionTexts));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its unselected options are precisely those specified by <paramref name="optionValues"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their DOM value.
        /// </para>
        /// </remarks>
        /// <param name="optionValues">The DOM values of the options to match against.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveUnselectedOptionsByValue(params string[] optionValues)
            => HaveUnselectedOptions(x => new HashSet<string>(x.Select(o => o.Value)).SetEquals(optionValues));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the element's options.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => new FilterSpecification<IReadOnlyList<Option>>(new OptionsQuery(), Spec.Func(predicate));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its options are precisely those specified by <paramref name="optionTexts"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their display text.
        /// </para>
        /// </remarks>
        /// <param name="optionTexts">The display text of the options to match against.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveOptionsByText(params string[] optionTexts)
            => HaveOptions(x => new HashSet<string>(x.Select(o => o.Text)).SetEquals(optionTexts));

        /// <summary>
        /// Creates a filter specification based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its options are precisely those specified by <paramref name="optionValues"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their DOM value.
        /// </para>
        /// </remarks>
        /// <param name="optionValues">The DOM values of the options to match against.</param>
        /// <returns>A specification function for Selenium elements.</returns>
        public static ISpecificationFunction<SeleniumElement> HaveOptionsByValue(params string[] optionValues)
            => HaveOptions(x => new HashSet<string>(x.Select(o => o.Value)).SetEquals(optionValues));
    }
}