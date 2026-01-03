using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CSF.Screenplay.Selenium.Actions;
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
        readonly bool multiElement;

        /// <summary>
        /// Gets a WebDriver predicate function which matches the value of the specified attribute from the target.
        /// </summary>
        /// <param name="attributeName">The name of the attribute from which to read the value</param>
        /// <param name="predicate">The predicate by which to match the attribute value</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Attribute(string attributeName, Func<string,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new AttributeQuery(attributeName), predicate), $"{target.Name} has attribute '{attributeName}' matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the clickability of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the clickability</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Clickability(Func<bool,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new ClickableQuery(), predicate), $"{target.Name} has clickability matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the target is clickable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Clickability(clickable => clickable)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Clickable()
            => WaitUntilPredicate.From(FromQuery(new ClickableQuery(), clickable => clickable), $"{target.Name} is clickable");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the target is not clickable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Clickability(clickable => !clickable)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> NotClickable()
            => WaitUntilPredicate.From(FromQuery(new ClickableQuery(), clickable => !clickable), $"{target.Name} is not clickable");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the value of the specified CSS property from the target.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property from which to read the value</param>
        /// <param name="predicate">The predicate by which to match the CSS property value</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> CssProperty(string propertyName, Func<string,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new CssPropertyQuery(propertyName), predicate), $"{target.Name} has CSS property '{propertyName}' matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the pixel location (top-left corner) of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the location</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Location(Func<Point,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new LocationQuery(), predicate));

        /// <summary>
        /// Gets a WebDriver predicate function which matches the selected options of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the selected options</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> SelectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new OptionsQuery(excludeUnselectedOptions: true), predicate), $"{target.Name} has selected options matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the unselected options of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the unselected options</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> UnselectedOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new OptionsQuery(excludeSelectedOptions: true), predicate), $"{target.Name} has unselected options matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which matches all options of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match all options</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> AllOptions(Func<IReadOnlyList<Option>,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new OptionsQuery(), predicate), $"{target.Name}'s available options match a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the pixel size of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the size</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Size(Func<Size,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new SizeQuery(), predicate), $"{target.Name} has size matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the text content of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the text</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Text(Func<string,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new TextQuery(), predicate), $"{target.Name} has text matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the text content of the target is empty.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Text(text => text == string.Empty)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Empty()
            => WaitUntilPredicate.From(FromQuery(new TextQuery(), text => text == string.Empty), $"{target.Name} has empty text");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the text content of the target is not empty.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Text(text => text != string.Empty)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> NotEmpty()
            => WaitUntilPredicate.From(FromQuery(new TextQuery(), text => text != string.Empty), $"{target.Name} has non-empty text");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the text content of the target
        /// equals the expected text.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Text(text => text == expected)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> TextEqualTo(string expected)
            => WaitUntilPredicate.From(FromQuery(new TextQuery(), text => text == expected), $"{target.Name} has text equal to '{expected}'");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the DOM value of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the value</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Value(Func<string,bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new ValueQuery(), predicate), $"{target.Name} has a DOM value matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the DOM value of the target is empty.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Value(value => string.IsNullOrEmpty(value))</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> AnEmptyValue()
            => WaitUntilPredicate.From(FromQuery(new ValueQuery(), value => string.IsNullOrEmpty(value)), $"{target.Name} has an empty DOM value");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the DOM value of the target is not empty.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Value(value => !string.IsNullOrEmpty(value))</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> AnyNonEmptyValue()
            => WaitUntilPredicate.From(FromQuery(new ValueQuery(), value => !string.IsNullOrEmpty(value)), $"{target.Name} has a non-empty DOM value");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the DOM value of the target
        /// equals the expected value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Value(value => value == expected)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> ValueEqualTo(string expected)
            => WaitUntilPredicate.From(FromQuery(new ValueQuery(), value => value == expected), $"{target.Name} has a DOM value equal to '{expected}'");

        /// <summary>
        /// Gets a WebDriver predicate function which matches the visibility of the target.
        /// </summary>
        /// <param name="predicate">The predicate by which to match the visibility</param>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Visibility(Func<bool, bool> predicate)
            => WaitUntilPredicate.From(FromQuery(new VisibilityQuery(), predicate), $"{target.Name} has visibility matching a predicate");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the target is visible.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Visibility(visible => visible)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> Visibile()
            => WaitUntilPredicate.From(FromQuery(new VisibilityQuery(), visible => visible), $"{target.Name} is visible");

        /// <summary>
        /// Gets a WebDriver predicate function which returns <see langword="true"/> if the target is not visible.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a shorthand for <c>Visibility(visible => !visible)</c>.
        /// </para>
        /// </remarks>
        /// <returns>A WebDriver predicate function</returns>
        public WaitUntilPredicate<bool> NotVisibile()
            => WaitUntilPredicate.From(FromQuery(new VisibilityQuery(), visible => !visible), $"{target.Name} is not visible");

        Func<IWebDriver, bool> FromQuery<T>(IQuery<T> query, Func<T, bool> predicate)
        {
            if(multiElement)
                return driver => target.GetElements(driver).All(element => predicate(query.GetValue(element)));
            return driver => predicate(query.GetValue(target.GetElement(driver)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateQueryBuilder"/> class.
        /// </summary>
        /// <param name="target">The target element for the queries.</param>
        /// <param name="multiElement">If set to <see langword="true"/> then this builder will get a predicate which works
        /// for targets which represent a collection of HTML elements; otherwise it will get a predicate for a single HTML
        /// element.</param>
        public PredicateQueryBuilder(ITarget target, bool multiElement)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.multiElement = multiElement;
        }
    }
}