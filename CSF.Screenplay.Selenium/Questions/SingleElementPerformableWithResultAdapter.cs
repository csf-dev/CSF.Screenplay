using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// Adapter class which allows a <see cref="ISingleElementPerformableWithResult{TResult}"/> to be used as an <see cref="IPerformableWithResult{TResult}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This adapter class is provided as a convenience to perform some of the boilerplate logic required to scaffold
    /// the parameters required by <see cref="ISingleElementPerformableWithResult{TResult}"/>.
    /// </para>
    /// <para>
    /// This method also caches the <see cref="SeleniumElement"/> which is returned by the <see cref="ITarget"/>, avoiding the need
    /// to fetch it using the WebDriver more than once.  As such, instances of this adapter (like all performables) should not be re-used.
    /// </para>
    /// </remarks>
    public class SingleElementPerformableWithResultAdapter<TResult> : IPerformableWithResult<TResult>, ICanReport
    {
        readonly ISingleElementPerformableWithResult<TResult> performable;
        readonly ITarget target;
        Lazy<SeleniumElement> lazyElement;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
        {
            lazyElement = lazyElement ?? actor.GetLazyElement(target);
            return performable.GetReportFragment(actor, lazyElement, formatter);
        }

        /// <inheritdoc/>
        public ValueTask<TResult> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            lazyElement = lazyElement ?? actor.GetLazyElement(target);
            return performable.PerformAsAsync(actor, actor.GetAbility<BrowseTheWeb>().WebDriver, lazyElement, cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleElementPerformableWithResultAdapter{TResult}"/> class with the specified performable and target.
        /// </summary>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">The target element for the performable.</param>
        public SingleElementPerformableWithResultAdapter(ISingleElementPerformableWithResult<TResult> performable, ITarget target)
        {
            this.performable = performable ?? throw new ArgumentNullException(nameof(performable));
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}