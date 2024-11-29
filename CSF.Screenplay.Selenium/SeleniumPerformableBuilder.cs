using System.Collections.Generic;
using System.Threading;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Tasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Builder type for creating performables which interact with Selenium WebDriver via Screenplay.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Consume this class from your own Screenplay logic with <c>using static CSF.Screenplay.Selenium.SeleniumPerformableBuilder;</c>.
    /// </para>
    /// </remarks>
    public static class SeleniumPerformableBuilder
    {
#region general actions

        /// <summary>
        /// Gets a performable action which opens a URL.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the specified Uri is a relative Uri, then this task will use the actor's <see cref="UseABaseUri"/> ability (if present)
        /// to transform the relative Uri into an absolute one.  The specified Uri will be used directly if it is already absolute.
        /// </para>
        /// </remarks>
        /// <param name="uri">The uri at which to open the web browser.</param>
        /// <returns>A performable</returns>
        public static IPerformable OpenTheUrl(NamedUri uri) => new OpenUrlRespectingBase(uri);

        /// <summary>
        /// Gets a performable question which takes a screenshot of the current web page and returns it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to take a screenshot and save it as an asset file, please consider <see cref="TakeAndSaveAScreenshot(string)"/>
        /// instead of this method.
        /// </para>
        /// <para>
        /// This method will raise an exception if the WebDriver is not capable of taking screenshots. If you want to avoid this, consider
        /// using <see cref="TakeAScreenshotIfSupported"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public static IPerformableWithResult<Screenshot> TakeAScreenshot() => new TakeScreenshot();

        /// <summary>
        /// Gets a performable question which takes a screenshot of the current web page and returns it, if the WebDriver is capable of doing so.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to take a screenshot and save it as an asset file, please consider <see cref="TakeAndSaveAScreenshot(string)"/>
        /// instead of this method.
        /// </para>
        /// <para>
        /// This method will not raise an exception if the WebDriver is not capable of taking screenshots.  Instead, it will return a value task
        /// of <see langword="null" />. If you wish to use a performable which will raise an exception if the WebDriver is not capable of taking
        /// screenshots, consider using <see cref="TakeAScreenshot"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public static IPerformableWithResult<Screenshot> TakeAScreenshotIfSupported() => new TakeScreenshot(false);

        /// <summary>
        /// Gets a performable action which saves a screenshot to a file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method returns a builder object which may be used to specify a short name for the screenshot.
        /// This allows it to be quickly identified in the report.
        /// </para>
        /// <para>
        /// If you only want to take a screenshot and save it as an asset file, please consider <see cref="TakeAndSaveAScreenshot(string)"/>
        /// instead of this method.
        /// </para>
        /// </remarks>
        /// <param name="screenshot">The Selenium screenshot instance</param>
        /// <returns>A performable</returns>
        public static SaveScreenshotBuilder SaveTheScreenshot(Screenshot screenshot) => new SaveScreenshotBuilder(screenshot);

        /// <summary>
        /// Gets a performable taek which takes a screenshot and saves it to a file, using an optional short name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may be used to specify a short name for the screenshot.
        /// This allows it to be quickly identified in the report.
        /// </para>
        /// <para>
        /// This method will raise an exception if the WebDriver is not capable of taking screenshots. If you want to avoid this, consider
        /// using <see cref="TakeAndSaveAScreenshotIfSupported"/> instead.
        /// </para>
        /// </remarks>
        /// <param name="name">A short name to identify the Screenshot when it is saved as a file</param>
        /// <returns>A performable</returns>
        public static IPerformable TakeAndSaveAScreenshot(string name = null) => new TakeAndSaveScreenshot(name);

        /// <summary>
        /// Gets a performable taek which takes a screenshot and saves it to a file, using an optional short name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may be used to specify a short name for the screenshot.
        /// This allows it to be quickly identified in the report.
        /// </para>
        /// <para>
        /// This method will not raise an exception if the WebDriver is not capable of taking screenshots.  Instead, it will do nothing.
        /// If you wish to use a performable which will raise an exception if the WebDriver is not capable of taking
        /// screenshots, consider using <see cref="TakeAndSaveAScreenshot"/> instead.
        /// </para>
        /// </remarks>
        /// <param name="name">A short name to identify the Screenshot when it is saved as a file</param>
        /// <returns>A performable</returns>
        public static IPerformable TakeAndSaveAScreenshotIfSupported(string name = null) => new TakeAndSaveScreenshot(name, false);

        /// <summary>
        /// Gets a performable action which clears all cookies for the current domain.
        /// </summary>
        /// <returns>A performable action</returns>
        public static IPerformable ClearAllDomainCookies() => new ClearCookies();

        /// <summary>
        /// Gets a performable action which deletes a single named cookie.
        /// </summary>
        /// <returns>A performable action</returns>
        public static IPerformable DeleteTheCookieNamed(string cookieName) => new DeleteTheCookie(cookieName);

        /// <summary>
        /// Gets a performable action which clears the local storage for the current domain.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method gets a performable which will throw an exception if the WebDriver does not support HTML5 Web Storage.
        /// To avoid this, consider using <see cref="ClearLocalStorageIfSupported"/> instead.
        /// </para>
        /// </remarks>
        /// <returns>A performable action</returns>
        public static IPerformable ClearLocalStorage() => new ClearLocalStorage();

        /// <summary>
        /// Gets a performable action which clears the local storage for the current domain if the WebDriver supports it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method gets a performable which will do nothing if the WebDriver does not support HTML5 Web Storage.
        /// </para>
        /// </remarks>
        /// <returns>A performable action</returns>
        public static IPerformable ClearLocalStorageIfSupported() => new ClearLocalStorage(false);

#endregion

#region element actions

        /// <summary>
        /// Gets a performable action which represents an actor clicking on a specified target element.
        /// </summary>
        /// <param name="target">The target element on which to click.</param>
        /// <returns>A performable action</returns>
        public static IPerformable ClickOn(ITarget target) => SingleElementPerformableAdapter.From(new Click(), target);

        /// <summary>
        /// Gets a builder for creating a performable action which represents an actor typing text into a target element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This may be used to send more than normal/printable text to the specified element. Special/nonprintable keys may be sent by
        /// using the <c>OpenQA.Selenium.Keys</c> class.
        /// </para>
        /// <para>
        /// For convenience, especially when using the Selenium <c>Keys</c> class mentioned above, this method accepts a <c>params</c> array
        /// of strings. If an array of strings is passed, they will be concatenated together before being sent to the element as a single string.
        /// The array/params syntax is used to allow a consumer to pass multiple strings (perhaps each only one character) as a single argument,
        /// without needing to manually concatenate them.
        /// </para>
        /// </remarks>
        /// <param name="text">The text/keys for the actor to type.</param>
        /// <returns>A builder with which the user may select a target element.</returns>
        public static SendKeysBuilder EnterTheText(params string[] text) => new SendKeysBuilder(string.Join(string.Empty, text));

        /// <summary>
        /// Gets a performable which represents an actor deselecting everything from a <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As might be expected, the <paramref name="target"/> parameter must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="target">A target which represents an HTML <c>&lt;select&gt;</c> element</param>
        /// <returns>A performable action</returns>
        public static IPerformable DeselectEverythingFrom(ITarget target) => SingleElementPerformableAdapter.From(new DeselectAll(), target);

        /// <summary>
        /// Gets a builder which will create a performable which represents an actor deselecting a specified option from a
        /// <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload deselects the option by its zero-based index.
        /// As might be expected, the target which is specified in the builder must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="optionIndex">The zero-based index of an option to deselect</param>
        /// <returns>A builder by which a target element is chosen</returns>
        public static FromTargetActionBuilder DeselectTheOption(int optionIndex)
            => new FromTargetActionBuilder(new DeselectByIndex(optionIndex));

        /// <summary>
        /// Gets a builder which will create a performable which represents an actor deselecting a specified option from a
        /// <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload deselects the option by its displayed text.
        /// As might be expected, the target which is specified in the builder must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="optionText">The text of the option to deselect</param>
        /// <returns>A builder by which a target element is chosen</returns>
        public static FromTargetActionBuilder DeselectTheOption(string optionText)
            => new FromTargetActionBuilder(new DeselectByText(optionText));

        /// <summary>
        /// Gets a builder which will create a performable which represents an actor deselecting a specified option from a
        /// <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload deselects the option by its underlying <c>value</c> attribute, instead of its displayed text.
        /// As might be expected, the target which is specified in the builder must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="optionValue">The underlying value of the option to deselect</param>
        /// <returns>A builder by which a target element is chosen</returns>
        public static FromTargetActionBuilder DeselectTheOptionWithValue(string optionValue)
            => new FromTargetActionBuilder(new DeselectByValue(optionValue));

        /// <summary>
        /// Gets a builder which will create a performable which represents an actor selecting a specified option from a
        /// <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload selects the option by its zero-based index.
        /// As might be expected, the target which is specified in the builder must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="optionIndex">The zero-based index of an option to select</param>
        /// <returns>A builder by which a target element is chosen</returns>
        public static FromTargetActionBuilder SelectTheOption(int optionIndex)
            => new FromTargetActionBuilder(new SelectByIndex(optionIndex));

        /// <summary>
        /// Gets a builder which will create a performable which represents an actor selecting a specified option from a
        /// <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload selects the option by its displayed text.
        /// As might be expected, the target which is specified in the builder must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="optionText">The text of the option to select</param>
        /// <returns>A builder by which a target element is chosen</returns>
        public static FromTargetActionBuilder SelectTheOption(string optionText)
            => new FromTargetActionBuilder(new SelectByText(optionText));

        /// <summary>
        /// Gets a builder which will create a performable which represents an actor selecting a specified option from a
        /// <c>&lt;select&gt;</c> element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This overload selects the option by its underlying <c>value</c> attribute, instead of its displayed text.
        /// As might be expected, the target which is specified in the builder must represent a <c>&lt;select&gt;</c> element or the resulting
        /// performable will raise an exception.
        /// </para>
        /// </remarks>
        /// <param name="optionValue">The underlying value of the option to select</param>
        /// <returns>A builder by which a target element is chosen</returns>
        public static FromTargetActionBuilder SelectTheOptionWithValue(string optionValue)
            => new FromTargetActionBuilder(new SelectByValue(optionValue));

        /// <summary>
        /// Gets a performable action which clears the contents of the specified target element.
        /// </summary>
        /// <param name="target">The target element whose contents will be cleared.</param>
        /// <returns>A performable action</returns>
        public static IPerformable ClearTheContentsOf(ITarget target) => SingleElementPerformableAdapter.From(new ClearTheContents(), target);

#endregion

#region element questions

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a collection of elements within a specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to find elements within the <c>&lt;body&gt;</c> element of the page, consider using <see cref="FindElementsOnThePage"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <param name="target">The target within which to find HTML elements</param>
        /// <returns>A builder, which may be used to configure/get a question that finds elements</returns>
        public static FindElementsBuilder FindElementsWithin(ITarget target) => new FindElementsBuilder(target);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a collection of elements within the body of the page.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want to find elements which are descendents of a specified target, consider using <see cref="FindElementsWithin(ITarget)"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <returns>A builder, which may be used to configure/get a question that finds elements</returns>
        public static FindElementsBuilder FindElementsOnThePage() => new FindElementsBuilder(CssSelector.BodyElement);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a single element within a specified target.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you only want to find an element within the <c>&lt;body&gt;</c> element of the page, consider using <see cref="FindAnElementOnThePage"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <param name="target">The target within which to find HTML elements</param>
        /// <returns>A builder, which may be used to configure/get a question that finds an element</returns>
        public static FindElementBuilder FindAnElementWithin(ITarget target) => new FindElementBuilder(target);

        /// <summary>
        /// Gets a builder which may be used to create a performable action which finds a single element within the body of the page.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If you want to find an element which is a descendent of a specified target, consider using <see cref="FindAnElementWithin(ITarget)"/>
        /// instead.
        /// </para>
        /// </remarks>
        /// <returns>A builder, which may be used to configure/get a question that finds an element</returns>
        public static FindElementBuilder FindAnElementOnThePage() => new FindElementBuilder(CssSelector.BodyElement);

        /// <summary>
        /// Gets a builder which may be used to create a performable question which filters a collection of elements for those which match a specification.
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
        /// <param name="elements">The collection of elements to filter.</param>
        /// <returns>A builder with which consuming logic must provide a specification.</returns>
        /// <seealso cref="FilterElementsBuilder"/>
        /// <seealso cref="FilterSpecificationBuilder"/>
        public static FilterElementsBuilder FilterTheElements(IReadOnlyCollection<SeleniumElement> elements)
            => new FilterElementsBuilder(elements);

        /// <summary>
        /// Gets a builder which may be used to create a performable question which reads a piece of information from a single element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question makes use of an <see cref="IQuery{TResult}"/> to interrogate a single element and return a value.
        /// </para>
        /// </remarks>
        /// <param name="element">The element to interrogate for a value.</param>
        /// <returns>A builder which chooses the query</returns>
        public static QuestionQueryBuilder ReadFromTheSingleElements(ITarget element) => new QuestionQueryBuilder(element);

        /// <summary>
        /// Gets a builder which may be used to create a performable question which reads a collection of the same information from a collection of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This question makes use of an <see cref="IQuery{TResult}"/> to interrogate each element element in the collection and return a series of
        /// corresponding values.
        /// </para>
        /// </remarks>
        /// <param name="element">The elements to interrogate for values.</param>
        /// <returns>A builder which chooses the query</returns>
        public static QuestionMultiQueryBuilder ReadFromTheCollectionOfElements(ITarget element) => new QuestionMultiQueryBuilder(element);

#endregion
    }
}