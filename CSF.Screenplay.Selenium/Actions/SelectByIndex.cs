using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A partial Screenplay Action which selects an option from an HTML <c>&lt;select&gt;</c> element based
    /// upon that option's zero-based index (position).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.SelectTheOption(int)"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to select an option from the HTML element indicated by <xref href="SeleniumElementsAndTargetsArticle?text=the+target"/>.
    /// </para>
    /// <para>
    /// This action has differing behaviour depending whether the <c>&lt;select&gt;</c> element has the
    /// <c>multiple</c> attribute or not.
    /// </para>
    /// <list type="bullet">
    /// <item><description>
    /// For an element which permits multiple-selection, this action adds the chosen option to the option(s) which
    /// are already selected.
    /// </description></item>
    /// <item><description>
    /// For an element which does not permit multiple-selection, this action replaces the option which is currently
    /// selected with the chosen option.
    /// </description></item>
    /// </list>
    /// <include file="SingleElementPerformableDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Actions.ISingleElementPerformable']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example selects the second option (index 1) from the element with class <c>optional_extras</c>,
    /// which is a descendent of an element with the ID <c>confirm_purchase</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget optionalExtras = new CssSelector("#confirm_purchase .optional_extras", "the list of optional extras");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(SelectTheOption(1).From(optionalExtras), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class SelectByIndex : ISingleElementPerformable
    {
        readonly int index;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} selects option {Index} (zero-based) from {Element}", actor.Name, index, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().SelectByIndex(index);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectByIndex"/> class with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the option to select.</param>
        public SelectByIndex(int index)
        {
            if(index < 0) throw new ArgumentOutOfRangeException(nameof(index), "The index must be zero or greater.");
            this.index = index;
        }
    }
}