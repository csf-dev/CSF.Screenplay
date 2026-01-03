using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// An implementation of <see cref="ISingleElementPerformableWithResult{TResult}"/> which gets the value from an <see cref="IQuery{T}"/>
    /// upon a single Selenium element and returns its result.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the query.</typeparam>
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