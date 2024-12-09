using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Adapter class which allows a <see cref="IElementCollectionPerformableWithResult{TResult}"/> to be used as an <see cref="IPerformableWithResult{TResult}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This adapter class is provided as a convenience to perform some of the boilerplate logic required to scaffold
    /// the parameters required by <see cref="IElementCollectionPerformableWithResult{TResult}"/>.
    /// </para>
    /// <para>
    /// This method also caches the <see cref="SeleniumElementCollection"/> which is returned by the <see cref="ITarget"/>, avoiding the need
    /// to fetch it using the WebDriver more than once.  As such, instances of this adapter (like all performables) should not be re-used.
    /// </para>
    /// </remarks>
    public class ElementCollectionPerformableWithResultAdapter<TResult> : IPerformableWithResult<IReadOnlyList<TResult>>, ICanReport
    {
        readonly IElementCollectionPerformableWithResult<TResult> performable;
        readonly ITarget target;
        Lazy<SeleniumElementCollection> lazyElements;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
        {
            lazyElements = lazyElements ?? actor.GetLazyElements(target);
            return performable.GetReportFragment(actor, lazyElements, formatter);
        }

        /// <inheritdoc/>
        public ValueTask<IReadOnlyList<TResult>> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            lazyElements = lazyElements ?? actor.GetLazyElements(target);
            return performable.PerformAsAsync(actor, actor.GetAbility<BrowseTheWeb>().WebDriver, lazyElements, cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementCollectionPerformableWithResultAdapter{TResult}"/> class with the specified performable and target.
        /// </summary>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">A target which describes the elements for the performable.</param>
        public ElementCollectionPerformableWithResultAdapter(IElementCollectionPerformableWithResult<TResult> performable, ITarget target)
        {
            this.performable = performable ?? throw new ArgumentNullException(nameof(performable));
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}