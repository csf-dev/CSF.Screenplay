using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which reads or observes the state of a single HTML element and returns the result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the query.</typeparam>
    /// <remarks>
    /// <para>
    /// Use this question via the builder method <see cref="PerformableBuilder.ReadFromTheElement(ITarget)"/>.
    /// The builder will then guide you through inspecting the state of
    /// <xref href="SeleniumElementsAndTargetsArticle?text=the+target"/>. That state information will be returned
    /// as the result of this question.
    /// Crucially, to decide which piece of information to retrieve from the target, you will build and use
    /// <xref href="SeleniumQueriesArticle?text=a+Query"/>.
    /// </para>
    /// <include file="ElementQueryDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Questions.ISingleElementPerformableWithResult`1']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example gets the background color of a list item which has the class <c>warning</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget warningItem = new CssSelector("li.warning", "the warning item");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;string&gt;
    /// public async ValueTask&lt;string&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     var bgColor = await actor.PerformAsync(ReadFromTheElement(warningItem).TheCssProperty("background-color"), cancellationToken);
    ///     // ... other performance logic
    ///     return bgColor;
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="PerformableBuilder.ReadFromTheElement(ITarget)"/>
    public class SingleElementQuery<TResult> : ISingleElementPerformableWithResult<TResult>
    {
        readonly IQuery<TResult> query;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElement> element, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} reads {Query} from {Element}", actor, query, element.Value);

        /// <inheritdoc/>
        public ValueTask<TResult> PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElement> element, CancellationToken cancellationToken = default)
            => new ValueTask<TResult>(query.GetValue(element.Value));

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleElementQuery{TResult}"/> class with the specified query.
        /// </summary>
        /// <param name="query">The query to be used for retrieving the value from the Selenium element.</param>
        public SingleElementQuery(IQuery<TResult> query)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
        }
    }
}