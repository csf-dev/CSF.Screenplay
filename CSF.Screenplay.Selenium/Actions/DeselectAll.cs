using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A partial Screenplay Action which de-selects every option from an HTML <c>&lt;select&gt;</c> element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.DeselectEverythingFrom(ITarget)"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to deselect every option from the HTML element indicated by <xref href="SeleniumElementsAndTargetsArticle?text=the+target"/>.
    /// </para>
    /// <para>
    /// Use of this action makes sense only for <c>&lt;select&gt;</c> elements which have the <c>multiple</c> attribute.
    /// Select elements which do not permit multiple-selection must have precisely one option always selected,
    /// which means that this action will not be effective.
    /// </para>
    /// <include file="SingleElementPerformableDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Actions.ISingleElementPerformable']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example deselects every option from the element with class <c>optional_extras</c>, which is a descendent of an
    /// element with the ID <c>confirm_purchase</c>.
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
    ///     await actor.PerformAsync(DeselectEverythingFrom(optionalExtras), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class DeselectAll : ISingleElementPerformable
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deselects all of the options from {Element}", actor.Name, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().DeselectAll();
            return default;
        }
    }
}