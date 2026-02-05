using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Builds actions for setting values in Selenium web elements.
    /// </summary>
    public class SetTheValueBuilder: IGetsPerformable, SetTheValueBuilder.IChoosesValue
    {
        readonly ITarget target;
        object value;

        /// <inheritdoc/>
        public SetTheValueBuilder To(object value)
        {
            this.value = value;
            return this;
        }

        /// <summary>
        /// Gets a performable which, whilst setting the value programatically, attempts to simulate setting it interactively in the browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method to work around limitations in the web browser/WebDriver, as it is an imperfect solution.
        /// When this method is used, it configures the performable to use JavaScript which will not only update the value of the
        /// specified element.  It will additionally trigger HTML/JavaScript events upon the element such as <c>focus</c>, 
        /// <c>input</c>, <c>change</c> and <c>blur</c> in an attempt to simulate an interactive change.
        /// </para>
        /// <para>
        /// The reason for this is that client web page functionality may be listening to these events and would be exercised if
        /// the element value were updated by a human being interacting with the web page.  However when JS is used to update the value,
        /// those events are not fired.  This method fires the events artificially.
        /// </para>
        /// </remarks>
        /// <returns>A performable</returns>
        public IPerformable AsIfSetInteractively() => SingleElementPerformableAdapter.From(new SetTheElementValue(value, true), target);

        IPerformable IGetsPerformable.GetPerformable()
            => SingleElementPerformableAdapter.From(new SetTheElementValue(value, false), target);

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTheValueBuilder"/> class with the specified target.
        /// </summary>
        /// <param name="target">The target web element for which to build value-setting actions.</param>
        public SetTheValueBuilder(ITarget target)
        {
            this.target = target ?? throw new System.ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// An object from which a consumer may choose a value to set into the element.
        /// </summary>
        public interface IChoosesValue
        {
            /// <summary>
            /// Gets a builder for a performable which sets the target element to the value specified in this method.
            /// </summary>
            /// <remarks>
            /// <para>
            /// If you are using this method to update the value of an element because you are working around a limitation in a web browser/
            /// WebDriver, then strongly consider following it with <see cref="AsIfSetInteractively"/>.
            /// </para>
            /// </remarks>
            /// <param name="value">The new value for the element</param>
            /// <returns>A builder for a performable</returns>
            SetTheValueBuilder To(object value);
        }
    }
}