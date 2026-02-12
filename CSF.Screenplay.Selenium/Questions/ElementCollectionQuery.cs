using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which reads or observes the state from a collection of HTML elements and returns the results.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the query.</typeparam>
    /// <remarks>
    /// <para>
    /// Use this question via the builder method <see cref="PerformableBuilder.ReadFromTheCollectionOfElements(ITarget)"/>.
    /// The builder will then guide you through inspecting the state of
    /// <xref href="SeleniumElementsAndTargetsArticle?text=the+targets"/>. The corresponding state information from
    /// each of the elements which are found by the target  will be returned as the result of this question.
    /// Crucially, to decide which piece of information to retrieve from the target elements, you will build and use
    /// <xref href="SeleniumQueriesArticle?text=a+Query"/>.
    /// </para>
    /// <include file="ElementQueryDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.Questions.IElementCollectionPerformableWithResult`1']/*" />
    /// </remarks>
    /// <example>
    /// <para>
    /// This example gets the text of every list item which has the class <c>todo</c>.
    /// </para>
    /// <code>
    /// using CSF.Screenplay.Selenium.Elements;
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// readonly ITarget todoItems = new CssSelector("li.todo", "the todo items");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;IReadOnlyList&lt;string&gt;&gt;
    /// public async ValueTask&lt;IReadOnlyList&lt;string&gt;&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     var texts = await actor.PerformAsync(ReadFromTheCollectionOfElements(todoItems).Text(), cancellationToken);
    ///     // ... other performance logic
    ///     return texts;
    /// }
    /// </code>
    /// </example>
    public class ElementCollectionQuery<TResult> : IElementCollectionPerformableWithResult<TResult>
    {
        readonly IQuery<TResult> query;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, Lazy<SeleniumElementCollection> elements, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} reads {Query} from {Element}", actor, query, elements.Value);

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<TResult>> PerformAsAsync(ICanPerform actor, IWebDriver webDriver, Lazy<SeleniumElementCollection> elements, CancellationToken cancellationToken = default)
            => new ValueTask<IReadOnlyList<TResult>>(elements.Value.Select(query.GetValue).ToList());

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementCollectionQuery{TResult}"/> class with the specified query.
        /// </summary>
        /// <param name="query">The query to be used for retrieving the value from each Selenium element.</param>
        public ElementCollectionQuery(IQuery<TResult> query)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
        }
    }
}