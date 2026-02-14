using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Specifications;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides methods to build WebDriver predicate functions for a target element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Predicates, built by the members of this class, are primarily used for two purposes:
    /// </para>
    /// <list type="bullet">
    /// <item><description>The <see cref="Wait"/> action, to specify the condition under which the wait should end.</description></item>
    /// <item><description>The <see cref="Questions.FilterElements"/> question, to specify the criteria by which to match elements.</description></item>
    /// </list>
    /// <para>
    /// For more information about interrogating the state of targets, see <xref href="SeleniumQueriesArticle?the+Queries+documentation+article"/>;
    /// queries are the foundation upon which this predicate functionality is based.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// This first example shows how a <see cref="Wait"/> action may be written, to wait until the text of a status display reads "finished".
    /// Note the section <c>.Has.Text("finished")</c>, which demonstrates use of the functionality of this class.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget status = new CssSelector("#footer .status", "the status display");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(WaitUntil(status.Has().Text("finished"), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// <para>
    /// The second example, below, shows the usage of <see cref="Questions.FilterElements"/>, matching only elements
    /// which are larger than 50×50 pixels in both dimensions.  Imagine a bubble chart, in which we are looking for bubbles
    /// larger than a certain size.
    /// Note the section <c>.ForThoseWhich(q => q.Size(s => s.Height &gt; 50 &amp;&amp; s.Width &gt; 50))</c>, which demonstrates
    /// use of the functionality of this class.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget bubbles = new CssSelector(".bubble_chart .bubble", "the items in the bubble chart");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;SeleniumElementCollection&gt;
    /// public async ValueTask&lt;SeleniumElementCollection&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     var allBubbles = await actor.PerformAsync(FindElementsOnThePage().WhichMatch(bubbles), cancellationToken);
    ///     var largeBubbles = await actor.PerformAsync(Filter(allBubbles)
    ///                                                 .ForThoseWhich(q => q.Size(s => s.Height &gt; 50 &amp;&amp; s.Width &gt; 50)),
    ///                                                cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="Wait"/>
    /// <seealso cref="Questions.FilterElements"/>
    /// <seealso cref="TargetExtensions.AllHave(ITarget)"/>
    /// <seealso cref="TargetExtensions.AreAll(ITarget)"/>
    /// <seealso cref="TargetExtensions.Has(ITarget)"/>
    /// <seealso cref="TargetExtensions.Is(ITarget)"/>
    /// <seealso cref="FilterElementsBuilder.ForThoseWhich(Func{QueryPredicatePrototypeBuilder, IBuildsElementPredicates})"/>
    public class QueryPredicatePrototypeBuilder
    {
        readonly ITarget target;
        readonly bool multiElement;

        /// <summary>
        /// Creates a query predicate based on a specified HTML attribute and a predicate for the attribute's value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The underlying implementation of this method makes use of the <c>GetAttribute</c> method of Selenium's
        /// <see cref="OpenQA.Selenium.IWebElement"/> type to query the current attribute value.  The get-attribute behaviour
        /// queries the corresponding DOM property of the element first, before querying the HTML attribute.
        /// Thus, if the DOM has been updated since the element was rendered onto the page, the current <em>live value</em>
        /// of the element will be used.
        /// Selenium's get-attribute functionality also has special handling for certain attributes such as <c>class</c>
        /// &amp; <c>readonly</c>.  When inspecting the DOM properties, these names are transparently normalised to
        /// <c>className</c> and <c>readOnly</c> respectively.
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// Note that the last behaviour, described above, means that this method may be used to match elements which <em>do not</em> have the specified attribute,
        /// by using a predicate which checks for <see langword="null"/>.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <param name="predicate">The predicate to apply to the attribute value.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> AttributeValue(string attributeName, Func<string,bool> predicate)
            => CreatePrototype(new AttributeQuery(attributeName), Spec.Func(predicate), t => $"{t.Name} has attribute '{attributeName}' matching a predicate");

        /// <summary>
        /// Creates a query predicate for the presence of a specified HTML attribute, with a specified value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The underlying implementation of this method makes use of the <c>GetAttribute</c> method of Selenium's
        /// <see cref="OpenQA.Selenium.IWebElement"/> type to query the current attribute value.  The get-attribute behaviour
        /// queries the corresponding DOM property of the element first, before querying the HTML attribute.
        /// Thus, if the DOM has been updated since the element was rendered onto the page, the current <em>live value</em>
        /// of the element will be used.
        /// Selenium's get-attribute functionality also has special handling for certain attributes such as <c>class</c>
        /// &amp; <c>readonly</c>.  When inspecting the DOM properties, these names are transparently normalised to
        /// <c>className</c> and <c>readOnly</c> respectively.
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// Note that the last behaviour, described above, means that this method may be used to match elements which <em>do not</em> have the specified attribute,
        /// by using <see langword="null"/> as the desired value.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <param name="value">The value of the attribute to query.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> AttributeValue(string attributeName, string value)
            => CreatePrototype(new AttributeQuery(attributeName), Spec.Func<string>(x => x == value), t => $"{t.Name} has attribute '{attributeName}' with value '{value}'");

        /// <summary>
        /// Creates a query predicate for the presence of a specified HTML attribute.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The underlying implementation of this method makes use of the <c>GetAttribute</c> method of Selenium's
        /// <see cref="OpenQA.Selenium.IWebElement"/> type to query the current attribute value.  The get-attribute behaviour
        /// queries the corresponding DOM property of the element first, before querying the HTML attribute.
        /// Thus, if the DOM has been updated since the element was rendered onto the page, the current <em>live value</em>
        /// of the element will be used.
        /// Selenium's get-attribute functionality also has special handling for certain attributes such as <c>class</c>
        /// &amp; <c>readonly</c>.  When inspecting the DOM properties, these names are transparently normalised to
        /// <c>className</c> and <c>readOnly</c> respectively.
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// This method specifically targets that last behaviour, described above.  It verifies that the response for attribute's
        /// value is not <see langword="null"/>.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="attributeName">The name of the attribute to query.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> Attribute(string attributeName)
            => CreatePrototype(new AttributeQuery(attributeName), Spec.Func<string>(x => x != null), t => $"{t.Name} has the attribute '{attributeName}'");

        /// <summary>
        /// Creates a query predicate for the presence of a specified HTML class (amongst the class attribute's values).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The underlying implementation of this method makes use of the <c>GetAttribute</c> method of Selenium's
        /// <see cref="OpenQA.Selenium.IWebElement"/> type to query the current attribute value.  The get-attribute behaviour
        /// queries the corresponding DOM property of the element first, before querying the HTML attribute.
        /// Thus, if the DOM has been updated since the element was rendered onto the page, the current <em>live value</em>
        /// of the element will be used.
        /// Selenium's get-attribute functionality also has special handling for certain attributes such as <c>class</c>
        /// &amp; <c>readonly</c>.  When inspecting the DOM properties, these names are transparently normalised to
        /// <c>className</c> and <c>readOnly</c> respectively.
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// This method inspects the <c>class</c> attribute and determines whether the specified class is present amongst the element's
        /// class list.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="class">The name of the class to query.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> Class(string @class)
            => CreatePrototype(new AttributeQuery(AttributeQuery.ClassAttribute), Spec.Func<string>(x => HasClass(x, @class)), t => $"{t.Name} has the class '{@class}'");

        /// <summary>
        /// Creates a query predicate for the absence of a specified HTML class (amongst the class attribute's values).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The underlying implementation of this method makes use of the <c>GetAttribute</c> method of Selenium's
        /// <see cref="OpenQA.Selenium.IWebElement"/> type to query the current attribute value.  The get-attribute behaviour
        /// queries the corresponding DOM property of the element first, before querying the HTML attribute.
        /// Thus, if the DOM has been updated since the element was rendered onto the page, the current <em>live value</em>
        /// of the element will be used.
        /// Selenium's get-attribute functionality also has special handling for certain attributes such as <c>class</c>
        /// &amp; <c>readonly</c>.  When inspecting the DOM properties, these names are transparently normalised to
        /// <c>className</c> and <c>readOnly</c> respectively.
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// This method inspects the <c>class</c> attribute and determines whether the specified class is not present amongst the element's
        /// class list.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="class">The name of the class to query.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> NotClass(string @class)
            => CreatePrototype(new AttributeQuery(AttributeQuery.ClassAttribute), Spec.Func<string>(x => !HasClass(x, @class)), t => $"{t.Name} does not have the class '{@class}'");

        /// <summary>
        /// Creates a query predicate for the presence of all the specified HTML classes (amongst the class attribute's values).
        /// </summary>
        /// <remarks>
        /// <para>
        /// The underlying implementation of this method makes use of the <c>GetAttribute</c> method of Selenium's
        /// <see cref="OpenQA.Selenium.IWebElement"/> type to query the current attribute value.  The get-attribute behaviour
        /// queries the corresponding DOM property of the element first, before querying the HTML attribute.
        /// Thus, if the DOM has been updated since the element was rendered onto the page, the current <em>live value</em>
        /// of the element will be used.
        /// Selenium's get-attribute functionality also has special handling for certain attributes such as <c>class</c>
        /// &amp; <c>readonly</c>.  When inspecting the DOM properties, these names are transparently normalised to
        /// <c>className</c> and <c>readOnly</c> respectively.
        /// Finally, 'boolean' attribute values are returned with either the string <c>true</c> if present or <see langword="null"/> if not.
        /// </para>
        /// <para>
        /// This method inspects the <c>class</c> attribute and determines whether every one of the specified classes are present
        /// in the element's class list.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="classes">The names of the classes to query.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> AllClasses(params string[] classes)
            => CreatePrototype(new AttributeQuery(AttributeQuery.ClassAttribute),
                               Spec.Func<string>(x => classes.All(@class => HasClass(x, @class))),
                               t => $"{t.Name} has all the classes {string.Join(", ", classes.Select(c => $"'{c}'"))}");

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
        /// Creates a query predicate based on the element's clickability.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An element is considered 'clickable' if it is both visible in the web browser and it is not disabled.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's "clickability".</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> Clickability(Func<bool,bool> predicate)
            => CreatePrototype(new ClickableQuery(), Spec.Func<bool>(x => predicate(x)), t => $"{t.Name} has clickability matching a predicate");

        /// <summary>
        /// Creates a query predicate based on whether the element is clickable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An element is considered 'clickable' if it is both visible in the web browser and it is not disabled.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's "clickability".</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> Clickability(bool value)
            => CreatePrototype(new ClickableQuery(), Spec.Func<bool>(x => x == value), t => $"{t.Name} {(value ? "is clickable" : "is not clickable")}");

        /// <summary>
        /// Creates a query predicate for only clickable elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An element is considered 'clickable' if it is both visible in the web browser and it is not disabled.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> Clickable() => Clickability(true);

        /// <summary>
        /// Creates a query predicate for only elements which are not clickable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An element is considered 'clickable' if it is both visible in the web browser and it is not disabled.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> NotClickable() => Clickability(false);

        /// <summary>
        /// Creates a query predicate based on a specified CSS property and a predicate for the property's value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this is not limited to reading the styling of an element/elements as it was defined in
        /// a stylesheet.  It reads the <em>live styling</em> of the element as it is when the question executes.
        /// Thus, if JavaScript or a class change has altered the styling of the element since it was first rendered
        /// on-screen, its up-to-date styling information will be inspected.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="propertyName">The name of the property to query.</param>
        /// <param name="predicate">The predicate to apply to the property value.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> CssProperty(string propertyName, Func<string,bool> predicate)
            => CreatePrototype(new CssPropertyQuery(propertyName), Spec.Func(predicate), t => $"{t.Name} has the CSS property '{propertyName}' matching a predicate");

        /// <summary>
        /// Creates a query predicate based on a specified CSS property and a desired value for that property.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this is not limited to reading the styling of an element/elements as it was defined in
        /// a stylesheet.  It reads the <em>live styling</em> of the element as it is when the question executes.
        /// Thus, if JavaScript or a class change has altered the styling of the element since it was first rendered
        /// on-screen, its up-to-date styling information will be inspected.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="propertyName">The name of the property to query.</param>
        /// <param name="value">The value to compare against the property value.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> CssProperty(string propertyName, string value)
            => CreatePrototype(new CssPropertyQuery(propertyName), Spec.Func<string>(x => x == value), t => $"{t.Name} has the CSS property '{propertyName}' with value '{value}'");

        /// <summary>
        /// Creates a query predicate based on the element's location (its top-left corner).
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's location.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Point}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<Point> Location(Func<Point,bool> predicate)
            => CreatePrototype(new LocationQuery(), Spec.Func(predicate), t => $"{t.Name} has a page location matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the element's location (its top-left corner).
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's location.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Point}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<Point> Location(Point value)
            => CreatePrototype(new LocationQuery(), Spec.Func<Point>(x => x == value), t => $"{t.Name} has a page location equal to {value}");

        /// <summary>
        /// Creates a query predicate based on the element's size (width &amp; height in pixels).
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's size.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Size}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<Size> Size(Func<Size,bool> predicate)
            => CreatePrototype(new SizeQuery(), Spec.Func(predicate), t => $"{t.Name} has a pixel size matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the element's size (width &amp; height in pixels).
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's size.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Size}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<Size> Size(Size value)
            => CreatePrototype(new SizeQuery(), Spec.Func<Size>(x => x == value), t => $"{t.Name} has a pixel size equal to {value}");

        /// <summary>
        /// Creates a query predicate based on the element's text content.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When reading text from the web browser, this predicate will trim leading/trailing whitespace from that text before comparing it.
        /// This is because some browsers (Safari)
        /// include whitespace at the beginning/end of text read from the browser, which isn't visible to the end user.  This is typically the 
        /// space which is inherent in the markup, but which browsers ignore when actually displaying content.
        /// </para>
        /// <para>
        /// By trimming whitespace, the Selenium extension for Screenplay ensures that behaviour is consistent
        /// cross-browser.
        /// If this causes an issue and you would like the leading/trailing whitespace included the use
        /// <see cref="TextWithoutTrimmingWhitespace(string)"/> instead.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's text.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> Text(Func<string,bool> predicate)
            => CreatePrototype(new TextQuery(), Spec.Func(predicate), t => $"{t.Name} has text matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the element's text content.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When reading text from the web browser, this predicate will trim leading/trailing whitespace from that text before comparing it.
        /// This is because some browsers (Safari)
        /// include whitespace at the beginning/end of text read from the browser, which isn't visible to the end user.  This is typically the 
        /// space which is inherent in the markup, but which browsers ignore when actually displaying content.
        /// </para>
        /// <para>
        /// By trimming whitespace, the Selenium extension for Screenplay ensures that behaviour is consistent
        /// cross-browser.
        /// If this causes an issue and you would like the leading/trailing whitespace included the use
        /// <see cref="TextWithoutTrimmingWhitespace(string)"/> instead.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's text.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> Text(string value)
            => CreatePrototype(new TextQuery(), Spec.Func<string>(x => x == value), t => $"{t.Name} has text equal to '{value}'");

        /// <summary>
        /// Creates a query predicate based on the element's text content.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When reading text from the web browser, this predicate will preserve any leading/trailing whitespace in the text
        /// without trimming it. Be aware that some browsers (Safari) may include leading/trailing whitespace when reading text,
        /// which other WebDriver implementations do not.  This can result in inconsistent results when
        /// operating cross-browser.  If cross-browser consistency is important then consider using <see cref="Text(Func{string, bool})"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's text.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> TextWithoutTrimmingWhitespace(Func<string,bool> predicate)
            => CreatePrototype(new TextQuery(false), Spec.Func(predicate), t => $"{t.Name} has text matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the element's text content, without trimming leading/trailing whitespace.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When reading text from the web browser, this predicate will preserve any leading/trailing whitespace in the text
        /// without trimming it. Be aware that some browsers (Safari) may include leading/trailing whitespace when reading text,
        /// which other WebDriver implementations do not.  This can result in inconsistent results when
        /// operating cross-browser.  If cross-browser consistency is important then consider using <see cref="Text(string)"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's text.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> TextWithoutTrimmingWhitespace(string value)
            => CreatePrototype(new TextQuery(false), Spec.Func<string>(x => x == value), t => $"{t.Name} has text equal to '{value}'");

        /// <summary>
        /// Creates a query predicate based on the element's DOM <c>value</c>.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's DOM value.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> Value(Func<string,bool> predicate)
            => CreatePrototype(new ValueQuery(), Spec.Func(predicate), t => $"{t.Name} has a DOM value matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the element's DOM <c>value</c>.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's DOM value.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{String}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<string> Value(string value)
            => CreatePrototype(new ValueQuery(), Spec.Func<string>(x => x == value), t => $"{t.Name} has a DOM value equal to '{value}'");

        /// <summary>
        /// Creates a query predicate based on whether the element is visible.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's visibility.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> Visibility(Func<bool,bool> predicate)
            => CreatePrototype(new VisibilityQuery(), Spec.Func(predicate), t => $"{t.Name} has visibility matching a predicate");

        /// <summary>
        /// Creates a query predicate based on whether the element is visible.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="value">The value to compare against the element's visibility.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> Visibility(bool value)
            => CreatePrototype(new VisibilityQuery(), Spec.Func<bool>(x => x == value), t => $"{t.Name} {(value ? "is visible" : "is not visible")}");

        /// <summary>
        /// Creates a query predicate for elements that are visible.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> Visible() => Visibility(true);

        /// <summary>
        /// Creates a query predicate for elements that are not visible.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <returns>A <see cref="QueryPredicatePrototype{Boolean}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<bool> NotVisible() => Visibility(false);

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's selected options.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> SelectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => CreatePrototype(new OptionsQuery(excludeSelectedOptions: true), Spec.Func(predicate), t => $"{t.Name} has selected options matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its selected options are precisely those specified by <paramref name="optionTexts"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their display text.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="optionTexts">The display text of the options to match against.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> SelectedOptionsWithText(params string[] optionTexts)
            => CreatePrototype(new OptionsQuery(excludeUnselectedOptions: true),
                               Spec.Func<IReadOnlyList<Option>>(x => new HashSet<string>(x.Select(o => o.Text)).SetEquals(optionTexts)),
                               t => $"{t.Name} has selected options with the text values {string.Join(", ", optionTexts.Select(ot => $"'{ot}'"))}");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its selected options are precisely those specified by <paramref name="optionValues"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their DOM value.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="optionValues">The DOM values of the options to match against.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> SelectedOptionsWithValue(params string[] optionValues)
            => CreatePrototype(new OptionsQuery(excludeUnselectedOptions: true),
                               Spec.Func<IReadOnlyList<Option>>(x => new HashSet<string>(x.Select(o => o.Value)).SetEquals(optionValues)),
                               t => $"{t.Name} has selected options with the DOM values {string.Join(", ", optionValues.Select(ov => $"'{ov}'"))}");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's unselected options.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> UnselectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => CreatePrototype(new OptionsQuery(excludeSelectedOptions : true),
                               Spec.Func(predicate),
                               t => $"{t.Name} has unselected options matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its unselected options are precisely those specified by <paramref name="optionTexts"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their display text.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="optionTexts">The display text of the options to match against.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> UnselectedOptionsWithText(params string[] optionTexts)
            => CreatePrototype(new OptionsQuery(excludeSelectedOptions: true),
                               Spec.Func<IReadOnlyList<Option>>(x => new HashSet<string>(x.Select(o => o.Text)).SetEquals(optionTexts)),
                               t => $"{t.Name} has unselected options with the text values {string.Join(", ", optionTexts.Select(ot => $"'{ot}'"))}");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, which are not selected.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its unselected options are precisely those specified by <paramref name="optionValues"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their DOM value.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="optionValues">The DOM values of the options to match against.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> UnselectedOptionsWithValue(params string[] optionValues)
            => CreatePrototype(new OptionsQuery(excludeSelectedOptions: true),
                               Spec.Func<IReadOnlyList<Option>>(x => new HashSet<string>(x.Select(o => o.Value)).SetEquals(optionValues)),
                               t => $"{t.Name} has unselected options with the values {string.Join(", ", optionValues.Select(ov => $"'{ov}'"))}");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="predicate">The predicate to apply to the element's options.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> Options(Func<IReadOnlyList<Option>,bool> predicate)
            => CreatePrototype(new OptionsQuery(), Spec.Func(predicate), t => $"{t.Name} has available options matching a predicate");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its options are precisely those specified by <paramref name="optionTexts"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their display text.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="optionTexts">The display text of the options to match against.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> OptionsWithText(params string[] optionTexts)
            => CreatePrototype(new OptionsQuery(),
                               Spec.Func<IReadOnlyList<Option>>(x => new HashSet<string>(x.Select(o => o.Text)).SetEquals(optionTexts)),
                               t => $"{t.Name} has available options with the text values {string.Join(", ", optionTexts.Select(ot => $"'{ot}'"))}");

        /// <summary>
        /// Creates a query predicate based on the <c>&lt;option&gt;</c> elements, which are children of the current element, regardless of their selected state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This specification will match the element if its options are precisely those specified by <paramref name="optionValues"/>.
        /// The order of the options does not matter, but they must match exactly.
        /// This method identifies the options by their DOM value.
        /// </para>
        /// </remarks>
        /// <example><para>See the remarks for the class <see cref="QueryPredicatePrototypeBuilder"/> for two examples of
        /// usage. The methods of this type all follow the same broad pattern to create predicates.</para></example>
        /// <param name="optionValues">The DOM values of the options to match against.</param>
        /// <returns>A <see cref="QueryPredicatePrototype{IReadOnlyList}"/>, which may be converted to a full predicate.</returns>
        public QueryPredicatePrototype<IReadOnlyList<Option>> OptionsWithValue(params string[] optionValues)
            => CreatePrototype(new OptionsQuery(),
                               Spec.Func<IReadOnlyList<Option>>(x => new HashSet<string>(x.Select(o => o.Value)).SetEquals(optionValues)),
                               t => $"{t.Name} has available options with the values {string.Join(", ", optionValues.Select(ov => $"'{ov}'"))}");

        QueryPredicatePrototype<T> CreatePrototype<T>(IQuery<T> query,
                                                      ISpecificationFunction<T> specification,
                                                      Func<ITarget, string> summaryCreator)
        {
            return new QueryPredicatePrototype<T>(specification, query, target, summaryCreator, multiElement);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryPredicatePrototypeBuilder"/> class for creating instances of
        /// <see cref="QueryPredicatePrototype{TQueryable}"/> which are suitable for use as either <see cref="WaitUntilPredicate{Boolean}"/>
        /// or <see cref="ISpecificationFunction{SeleniumElement}"/>.
        /// </summary>
        /// <param name="target">The target element for the queries.</param>
        /// <param name="multiElement">If set to <see langword="true"/> then this builder will get a predicate which works
        /// for targets which represent a collection of HTML elements; otherwise it will get a predicate for a single HTML
        /// element.</param>
        public QueryPredicatePrototypeBuilder(ITarget target, bool multiElement)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.multiElement = multiElement;
        }
    }
}