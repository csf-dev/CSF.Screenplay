using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// Builds actions for setting values in Selenium web elements.
    /// </summary>
    public class SetTheValueBuilder
    {
        readonly ITarget target;

        /// <summary>
        /// Gets a performable, to set the target element to the value specified in this method.
        /// </summary>
        /// <param name="value">The new value for the element</param>
        /// <returns>A performable</returns>
        public IPerformable To(object value) => SingleElementPerformableAdapter.From(new SetTheElementValue(value), target);

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTheValueBuilder"/> class with the specified target.
        /// </summary>
        /// <param name="target">The target web element for which to build value-setting actions.</param>
        public SetTheValueBuilder(ITarget target)
        {
            this.target = target ?? throw new System.ArgumentNullException(nameof(target));
        }
    }
}