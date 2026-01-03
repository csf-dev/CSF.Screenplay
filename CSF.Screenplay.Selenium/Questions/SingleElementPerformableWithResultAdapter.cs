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
        readonly bool doNotThrowOnMissingElement;
        Lazy<SeleniumElement> lazyElement;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
        {
            lazyElement = lazyElement ?? actor.GetLazyElement(target);
            return performable.GetReportFragment(actor, lazyElement, formatter);
        }

        /// <inheritdoc/>
        public ValueTask<TResult> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            try
            {
            lazyElement = lazyElement ?? actor.GetLazyElement(target);
            return performable.PerformAsAsync(actor, actor.GetAbility<BrowseTheWeb>().WebDriver, lazyElement, cancellationToken);
            }
            catch(TargetNotFoundException) when(doNotThrowOnMissingElement)
            {
                return new ValueTask<TResult>(default(TResult));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleElementPerformableWithResultAdapter{TResult}"/> class with the specified performable and target.
        /// </summary>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">The target element for the performable.</param>
        /// <param name="doNotThrowOnMissingElement">If set to <see langword="true" /> then this performable will not throw an exception when the element does not exist.</param>
        public SingleElementPerformableWithResultAdapter(ISingleElementPerformableWithResult<TResult> performable, ITarget target, bool doNotThrowOnMissingElement = false)
        {
            this.performable = performable ?? throw new ArgumentNullException(nameof(performable));
            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.doNotThrowOnMissingElement = doNotThrowOnMissingElement;
        }
    }
}