using System;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Elements;
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
        /// </remarks>
        /// <returns>A performable</returns>
        public static IPerformableWithResult<Screenshot> TakeAScreenshot() => new TakeScreenshot();

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
        /// </remarks>
        /// <param name="name">A short name to identify the Screenshot when it is saved as a file</param>
        /// <returns>A performable</returns>
        public static IPerformable TakeAndSaveAScreenshot(string name = null) => new TakeAndSaveScreenshot(name);

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
    }
}