using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A partial Screenplay Action which de-selects an option from an HTML <c>&lt;select&gt;</c> element based
    /// upon that option's displayed text.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.DeselectTheOption(string)"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to deselect an option from the HTML element indicated by <xref href="SeleniumElementsAndTargetsArticle?text=the+target"/>.
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
    /// This example deselects the option "French Fries" from the element with class <c>optional_extras</c>, which is a
    /// descendent of an element with the ID <c>confirm_purchase</c>.
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
    ///     await actor.PerformAsync(DeselectTheOption("French Fries").From(optionalExtras), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.DeselectTheOption(string)"/>
    public class DeselectByText : ISingleElementPerformable
    {
        readonly string text;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} deselects '{Text}' from {Element}", actor.Name, text, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().DeselectByText(text);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeselectByText"/> class with the specified text.
        /// </summary>
        /// <param name="text">The human-readable text of the option to deselect.</param>
        public DeselectByText(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}