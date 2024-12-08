using System;
using System.Collections.Generic;
using System.Drawing;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Provides methods to build WebDriver predicate functions for a target element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These are generally used for WebDriver waits, to wait until a target element meets the specified conditions.
    /// </para>
    /// </remarks>
    public class PredicateQueryBuilder
    {
        readonly ITarget target;

        /// <summary>
        /// Gets a WebDriver predicate function which matches the value of the specified attribute from the target.
        /// </summary>
        /// <param name="attributeName">The name of the attribute from which to read the value</param>
        /// <param name="predicate">The predicate by which to match the attribute value</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Attribute(string attributeName, Func<string,bool> predicate) => FromQuery(new AttributeQuery(attributeName), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the clickability of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the clickability</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Clickable(Func<bool,bool> predicate) => FromQuery(new ClickableQuery(), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the value of the specified CSS property from the target.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property from which to read the value</param>
        /// <param name="predicate">The predicate by which to match the CSS property value</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> CssProperty(string propertyName, Func<string,bool> predicate) => FromQuery(new CssPropertyQuery(propertyName), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the pixel location (top-left corner) of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the location</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Location(Func<Point,bool> predicate) => FromQuery(new LocationQuery(), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the selected options of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the selected options</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> SelectedOptions(Func<IReadOnlyList<Option>,bool> predicate) => FromQuery(new OptionsQuery(excludeUnselectedOptions : true), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the unselected options of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the unselected options</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> UnselectedOptions(Func<IReadOnlyList<Option>,bool> predicate) => FromQuery(new OptionsQuery(excludeSelectedOptions : true), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches all options of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match all options</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> AllOptions(Func<IReadOnlyList<Option>,bool> predicate) => FromQuery(new OptionsQuery(), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the pixel size of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the size</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Size(Func<Size,bool> predicate) => FromQuery(new SizeQuery(), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the text content of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the text</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Text(Func<string,bool> predicate) => FromQuery(new TextQuery(), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the DOM value of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the value</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Value(Func<string,bool> predicate) => FromQuery(new ValueQuery(), predicate);

        /// <summary>
        /// Gets a WebDriver predicate function which matches the visibility of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the visibility</param>
        /// <returns>A WebDriver predicate function</returns>
        public Func<IWebDriver,bool> Visibility(Func<bool, bool> predicate) => FromQuery(new VisibilityQuery(), predicate);

        Func<IWebDriver, bool> FromQuery<T>(IQuery<T> query, Func<T, bool> predicate)
            => driver => predicate(query.GetValue(target.GetElement(driver)));

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateQueryBuilder"/> class.
        /// </summary>
        /// <param name="target">The target element for the queries.</param>
        public PredicateQueryBuilder(ITarget target)
        {
            this.target = target ?? throw new System.ArgumentNullException(nameof(target));
        }
    }
}