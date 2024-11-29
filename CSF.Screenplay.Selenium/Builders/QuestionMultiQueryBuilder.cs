using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Queries;
using System.Drawing;
using System.Collections.Generic;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides methods to build performable questions for a collection of Selenium elements, which are based upon
    /// querying/interrogating values from those elements.
    /// </summary>
    public class QuestionMultiQueryBuilder
    {
        readonly ITarget elements;

        /// <summary>
        /// Gets a performable question which reads the value of the specified attribute from the elements.
        /// </summary>
        /// <param name="attributeName">The name of the attribute from which to read the value</param>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<string>> Attribute(string attributeName) => FromQuery(new AttributeQuery(attributeName));

        /// <summary>
        /// Gets a performable question which checks the clickability of the elements.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<bool>> Clickability() => FromQuery(new ClickableQuery());

        /// <summary>
        /// Gets a performable question which reads the value of the specified CSS property from the elements.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property from which to read the value</param>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<string>> CssProperty(string propertyName) => FromQuery(new CssPropertyQuery(propertyName));

        /// <summary>
        /// Gets a performable question which reads the pixel location (top-left corner) of the elements.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<Point>> Location() => FromQuery(new LocationQuery());

        /// <summary>
        /// Gets a performable question which reads the selected options from a collection of <c>&lt;select&gt;</c> elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<IReadOnlyList<Option>>> SelectedOptions() => FromQuery(new OptionsQuery(excludeUnselectedOptions : true));

        /// <summary>
        /// Gets a performable question which reads the unselected options from a collection of <c>&lt;select&gt;</c> elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<IReadOnlyList<Option>>> UnselectedOptions() => FromQuery(new OptionsQuery(excludeSelectedOptions : true));

        /// <summary>
        /// Gets a performable question which reads all of the available options (whether selected or not) from a collection of <c>&lt;select&gt;</c> elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question is only valid for <c>&lt;select&gt;</c> elements.  For any other type of element, the behaviour is undefined.
        /// </para>
        /// </remarks>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<IReadOnlyList<Option>>> AllOptions() => FromQuery(new OptionsQuery());

        /// <summary>
        /// Gets a performable question which reads the pixel size (width and height) of the elements.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<Size>> Size() => FromQuery(new SizeQuery());

        /// <summary>
        /// Gets a performable question which reads the text content of the elements.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<string>> Text() => FromQuery(new TextQuery());

        /// <summary>
        /// Gets a performable question which reads the value of the elements.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<string>> Value() => FromQuery(new ValueQuery());

        /// <summary>
        /// Gets a performable question which checks the visibility of the elements.
        /// </summary>
        /// <returns>A performable question</returns>
        public IPerformableWithResult<IReadOnlyList<bool>> Visibility() => FromQuery(new VisibilityQuery());

        IPerformableWithResult<IReadOnlyList<T>> FromQuery<T>(IQuery<T> query)
            => ElementCollectionPerformableWithResultAdapter.From(ElementCollectionQuery.From(query), elements);

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionMultiQueryBuilder"/> class with the specified Selenium elements.
        /// </summary>
        /// <param name="elements">The Selenium elements to be used by the query builder.</param>
        public QuestionMultiQueryBuilder(ITarget elements)
        {
            this.elements = elements ?? throw new System.ArgumentNullException(nameof(elements));
        }
    }
}