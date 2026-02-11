using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A partial Screenplay Action which selects an option from an HTML <c>&lt;select&gt;</c> element based
    /// upon that option's displayed text.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.SelectTheOption(string)"/>.
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
    /// This example selects the option "French Fries" from the element with class <c>optional_extras</c>,
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
    ///     await actor.PerformAsync(SelectTheOption("French Fries").From(optionalExtras), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class SelectByText : ISingleElementPerformable
    {
        readonly string text;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} selects '{Text}' from {Element}", actor.Name, text, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.AsSelectElement().SelectByText(text);
            return default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectByText"/> class with the specified text.
        /// </summary>
        /// <param name="text">The human-readable text of the option to select.</param>
        public SelectByText(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}