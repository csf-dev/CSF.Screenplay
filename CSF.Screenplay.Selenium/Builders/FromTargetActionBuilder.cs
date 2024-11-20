using System;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// A builder class for creating performable actions which act upon a specified target element.
    /// </summary>
    public class FromTargetActionBuilder
    {
        readonly ISingleElementPerformable action;

        /// <summary>
        /// Creates a performable action from the specified target.
        /// </summary>
        /// <param name="target">The target element.</param>
        /// <returns>A performable action.</returns>
        public IPerformable From(ITarget target) => SingleElementPerformableAdapter.From(action, target);

        internal FromTargetActionBuilder(ISingleElementPerformable action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }
    }
}