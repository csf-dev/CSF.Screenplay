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
    public class QuestionQueryBuilder
    {
        readonly ITarget target;

        /// <summary>
        /// Gets a performable question which reads the value of the specified attribute from the element.
        /// </summary>
        /// <param name="attributeName">The name of the attribute from which to read the value</param>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<string> Attribute(string attributeName) => FromQuery(new AttributeQuery(attributeName));

        /// <summary>
        /// Gets a performable question which checks the clickability of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<bool> Clickability() => FromQuery(new ClickableQuery());

        /// <summary>
        /// Gets a performable question which reads the value of the specified CSS property from the element.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property from which to read the value</param>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<string> CssProperty(string propertyName) => FromQuery(new CssPropertyQuery(propertyName));

        /// <summary>
        /// Gets a performable question which reads the pixel location (top-left corner) of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<Point> Location() => FromQuery(new LocationQuery());

        /// <summary>
        /// Gets a performable question which reads the selected options from a <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
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
        public IPerformableWithResult<IReadOnlyList<Option>> AllOptions() => FromQuery(new OptionsQuery());

        /// <summary>
        /// Gets a performable question which reads the pixel size (width and height) of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<Size> Size() => FromQuery(new SizeQuery());

        /// <summary>
        /// Gets a performable question which reads the text content of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<string> Text() => FromQuery(new TextQuery());

        /// <summary>
        /// Gets a performable question which reads the value of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<string> Value() => FromQuery(new ValueQuery());

        /// <summary>
        /// Gets a performable question which checks the visibility of the element.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<bool> Visibility() => FromQuery(new VisibilityQuery(), true);

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