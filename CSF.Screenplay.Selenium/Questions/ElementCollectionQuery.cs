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
    /// An implementation of <see cref="IElementCollectionPerformableWithResult{TResult}"/> which gets the value from an <see cref="IQuery{T}"/>
    /// upon each item within a collection of Selenium elements and returns their results.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the query.</typeparam>
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