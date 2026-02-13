using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A partial Screenplay Action which clears the contents of an HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this action via the builder method <see cref="PerformableBuilder.ClearTheContentsOf(ITarget)"/>.
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, instructs the web browser
    /// to clear the contents of the HTML element indicated by <xref href="SeleniumElementsAndTargetsArticle?text=the+target"/>.
    /// </para>
    /// <para>
    /// The element to be cleared is typically an HTML <c>&lt;input&gt;</c> or <c>&lt;textarea&gt;</c> element.
    /// It could be any element which supports Webdriver's <c>Clear()</c> method.
    /// For input or textarea elements, this action is equivalent to the user selecting all text and pressing the delete key.
    /// </para>
    /// <include file="SingleElementPerformableDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Actions.ISingleElementPerformable']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example clears the contents of the element with ID <c>username</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget username = new ElementId("username", "the username field");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(ClearTheContentsOf(username), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.ClearTheContentsOf(ITarget)"/>
    public class ClearTheContents : ISingleElementPerformable
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} clears the contents of {Element}", actor.Name, element.Value);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
        {
            element.Value.WebElement.Clear();
            return default;
        }
    }
}