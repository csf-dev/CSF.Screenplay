using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Queries;
using System.Drawing;
using System.Collections.Generic;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides methods to build performable questions for a Selenium element, which are based upon
    /// querying/interrogating values from that element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is used with the Screenplay questions <see cref="SingleElementQuery{TResult}"/> and/or
    /// <see cref="ElementCollectionQuery{TResult}"/>. The builder methods for these questions,
    /// <see cref="PerformableBuilder.ReadFromTheElement(ITarget)"/> and
    /// <see cref="PerformableBuilder.ReadFromTheCollectionOfElements(ITarget)"/> respectively, return an
    /// instance of this builder type. From this type, the developer should select what it is that they wish
    /// to read from the element(s).
    /// </para>
    /// <para>
    /// For more information about queries and their usage, see <xref href="SeleniumQueriesArticle?the+Queries+documentation+article"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// All the usages of <see cref="SingleElementQuery{TResult}"/> or <see cref="ElementCollectionQuery{TResult}"/>
    /// follow the same pattern, as demonstrated below. This example reads the <c>background-color</c> from a list
    /// item.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget warningItem = new CssSelector("li.warning", "the warning item");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;string&gt;
    /// public async ValueTask&lt;string&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     var bgColor = await actor.PerformAsync(ReadFromTheElement(warningItem).TheCssProperty("background-color"), cancellationToken);
    ///     // ... other performance logic
    ///     return bgColor;
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="SingleElementQuery{TResult}"/>
    /// <seealso cref="ElementCollectionQuery{TResult}"/>
    public class QuestionQueryBuilder
    {
        readonly ITarget target;

        /// <summary>
        /// Gets a performable question which reads the value of the specified attribute from the element.
        /// </summary>
        /// <param name="attributeName">The name of the attribute from which to read the value</param>
        /// <returns>A performable question</returns>
        /// <example>
        /// <para>
        /// This example shows how to use the <see cref="SingleElementQuery{TResult}"/> question to read the
        /// <c>title</c> attribute from a button.
        /// </para>
        /// <code>
        /// using CSF.Screenplay.Selenium.Elements;
        /// using static CSF.Screenplay.Selenium.PerformableBuilder;
        /// 
        /// readonly ITarget cancelButton = new CssSelector("button.cancel", "the cancel button");
        /// 
        /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;string&gt;
        /// public async ValueTask&lt;string&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        /// {
        ///     // ... other performance logic
        ///     var title = await actor.PerformAsync(ReadFromTheElement(cancelButton).TheAttribute("title"), cancellationToken);
        ///     // ... other performance logic
        ///     return title;
        /// }
        /// </code>
        /// </example>
        public IPerformableWithResult<string> TheAttribute(string attributeName) => FromQuery(new AttributeQuery(attributeName));

        /// <summary>
        /// Gets a performable question which checks the clickability of the element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An element is considered 'clickable' if it is both visible in the web browser and it is not disabled.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<bool> TheClickability() => FromQuery(new ClickableQuery());

        /// <summary>
        /// Gets a performable question which reads the value of the specified CSS property from the element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this is not limited to reading the styling of an element/elements as it was defined in
        /// a stylesheet.  It reads the <em>live styling</em> of the element as it is when the question executes.
        /// Thus, if JavaScript or a class change has altered the styling of the element since it was first rendered
        /// on-screen, its up-to-date styling information will be returned by this query.
        /// </para>
        /// </remarks>
        /// <param name="propertyName">The name of the CSS property from which to read the value</param>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<string> TheCssProperty(string propertyName) => FromQuery(new CssPropertyQuery(propertyName));

        /// <summary>
        /// Gets a performable question which reads the pixel location (top-left corner) of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<Point> TheLocation() => FromQuery(new LocationQuery());

        /// <summary>
        /// Gets a performable question which reads the selected options from a <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<IReadOnlyList<Option>> SelectedOptions() => FromQuery(new OptionsQuery(excludeUnselectedOptions : true));

        /// <summary>
        /// Gets a performable question which reads the unselected options from a <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<IReadOnlyList<Option>> UnselectedOptions() => FromQuery(new OptionsQuery(excludeSelectedOptions : true));

        /// <summary>
        /// Gets a performable question which reads all of the available options (whether selected or not) from a <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<IReadOnlyList<Option>> AllOptions() => FromQuery(new OptionsQuery());

        /// <summary>
        /// Gets a performable question which reads the pixel size (width and height) of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<Size> TheSize() => FromQuery(new SizeQuery());

        /// <summary>
        /// Gets a performable question which reads the text content of the element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When reading text from the web browser, this query will trim leading/trailing whitespace.
        /// This is because some browsers (Safari)
        /// include whitespace at the beginning/end of text read from the browser, which isn't visible to the end user.  This is typically the 
        /// space which is inherent in the markup, but which browsers ignore when actually displaying content.
        /// </para>
        /// <para>
        /// Trimming it by default ensures that Screenplay reproduces functionality reliably cross-browser.
        /// If this causes an issue and you would like the leading/trailing whitespace included then use
        /// <see cref="TheTextWithoutTrimmingWhitespace"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<string> TheText() => FromQuery(new TextQuery());

        /// <summary>
        /// Gets a performable question which reads the text content of the element, preserving leading/trailing whitespace if present.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Unlike <see cref="TheText"/>, this method will not trim any leading/trailing whitespace.
        /// Be aware that some browsers (Safari) may include leading/trailing whitespace when reading text,
        /// which other WebDriver implementations do not.  This can result in inconsistent results when
        /// operating cross-browser.  If cross-browser consistency is important then consider using <see cref="TheText"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<string> TheTextWithoutTrimmingWhitespace() => FromQuery(new TextQuery(false));

        /// <summary>
        /// Gets a performable question which reads the value of the element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This query is only meaningful for elements which may have a value.
        /// Examples of these include <c>&lt;input&gt;</c>, <c>&lt;textarea&gt;</c> or <c>&lt;select&gt;</c> elements.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<string> TheValue() => FromQuery(new ValueQuery());

        /// <summary>
        /// Gets a performable question which checks the visibility of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        /// <example><para>See the class <see cref="QuestionQueryBuilder"/> for an example; all the methods to this
        /// class follow the same pattern.</para></example>
        public IPerformableWithResult<bool> TheVisibility() => FromQuery(new VisibilityQuery(), true);

        IPerformableWithResult<T> FromQuery<T>(IQuery<T> query, bool doNotThrowOnMissingElement = false)
            => SingleElementPerformableWithResultAdapter.From(SingleElementQuery.From(query), target, doNotThrowOnMissingElement);

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionQueryBuilder"/> class with the specified Selenium element.
        /// </summary>
        /// <param name="target">The Selenium element to be used by the query builder.</param>
        public QuestionQueryBuilder(ITarget target)
        {
            this.target = target ?? throw new System.ArgumentNullException(nameof(target));
        }
    }
}