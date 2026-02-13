using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks
{
    /// <summary>
    /// A Screenplay task which uses JavaScript to directly set the value of an HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this task via the builder method <see cref="SetTheValueOf(ITarget)"/>, optionally also using the builder method
    /// <see cref="SetTheValueBuilder.AsIfSetInteractively"/>.  When <c>AsIfSetInteractively</c> is used then additional JavaScript
    /// will be executed in an attempt to simulate a human user interactively setting the value.
    /// </para>
    /// <para>
    /// The rationale for this task is that sometimes, due to limitations of the WebDriver implementation, it is not possible to
    /// interact with an HTML element in the same way that a human user would.  An example of this is web browsers which are affected
    /// by <see cref="BrowserQuirks.CannotSetInputTypeDateWithSendKeys"/>. When these limitations are encountered, the only recourse
    /// is to work around them with JavaScript.
    /// </para>
    /// <para>
    /// If this task is not instructed to simulate setting the value interactively then this task does no more than use JavaScript to
    /// set the <c>value</c> of the element.  However, if it is simulating setting the value interactively then a number of HTML/JavaScript
    /// events are manually invoked, to give UI behaviour an opportunity to respond.  These events (and the order in which they are
    /// triggered) are: <c>focus</c>, <c>input</c>, <c>change</c> and then <c>blur</c>.  The element value is actually updated between
    /// the focus and input events.
    /// </para>
    /// <para>
    /// Use this task judiciously and sparingly. It is best to interact with the web browser/WebDriver in the same manner in which a human
    /// user would, particularly when using Screenplay/Selenium for testing. This task is provided just to work around difficulties/limitations;
    /// it is not intended to be the standard way to update elements on a web page.
    /// </para>
    /// <include file="..\Actions\SingleElementPerformableDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Actions.ISingleElementPerformable']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example sets the value of an element that has id <c>impossible_input</c> to "I worked around it!" and triggers events
    /// which simulate a user changing the value interactively.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget impossibleInput = new ElementId("impossible_input", "the input field which a WebDriver cannot reach");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(SetTheValueOf(impossibleInput).To("I worked around it!").AsIfSetInteractively(), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="SetTheValueOf"/>
    /// <seealso cref="SetTheValueBuilder"/>
    /// <seealso cref="ExecuteAScript"/>
    /// <seealso cref="Actions.ExecuteJavaScript"/>
    public class SetTheElementValue : Actions.ISingleElementPerformable
    {
        readonly object value;
        readonly bool simulateInteractiveSet;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
        {
            return simulateInteractiveSet
                ? formatter.Format("{Actor} uses JavaScript to simulate setting the value of {Element} to {Value} interactively", actor, element.Value, value)
                : formatter.Format("{Actor} uses JavaScript to set the value of {Element} to {Value}", actor, element.Value, value);
        }

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            return simulateInteractiveSet
                ? actor.PerformAsync(ExecuteAScript(Scripts.SetElementValueSimulatedInteractively, element.Value.WebElement, value), cancellationToken)
                : actor.PerformAsync(ExecuteAScript(Scripts.SetElementValue, element.Value.WebElement, value), cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetTheElementValue"/> class.
        /// </summary>
        /// <param name="value">The value to set on the element.</param>
        /// <param name="simulateInteractiveSet">If <see langword="true"/> then the JavaScript will fire UI events to simulate having set the element interactively</param>
        public SetTheElementValue(object value, bool simulateInteractiveSet)
        {
            this.value = value;
            this.simulateInteractiveSet = simulateInteractiveSet;
        }
    }
}