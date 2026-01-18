using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium
{
    public static partial class PerformableBuilder
    {
        /// <summary>
        /// Gets a performable action which represents an actor clicking on a specified target element.
        /// </summary>
        /// <param name="target">The target element on which to click.</param>
        /// <returns>A performable action</returns>
        public static ClickBuilder ClickOn(ITarget target) => new ClickBuilder(target);

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
    }
}