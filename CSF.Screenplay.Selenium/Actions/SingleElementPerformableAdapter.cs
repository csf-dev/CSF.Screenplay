using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Elements;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// Adapter class which allows a <see cref="ISingleElementPerformable"/> to be used as an <see cref="IPerformable"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This adapter class is provided as a convenience to perform some of the boilerplate logic required to scaffold
    /// the parameters required by <see cref="ISingleElementPerformable"/>.
    /// </para>
    /// <para>
    /// This method also caches the <see cref="SeleniumElement"/> which is returned by the <see cref="ITarget"/>, avoiding the need
    /// to fetch it using the WebDriver more than once.  As such, instances of this adapter (like all performables) should not be re-used.
    /// </para>
    /// </remarks>
    public class SingleElementPerformableAdapter : IPerformable, ICanReport
    {
        readonly ISingleElementPerformable performable;
        readonly ITarget target;
        Lazy<SeleniumElement> lazyElement;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
        {
            lazyElement = lazyElement ?? actor.GetLazyElement(target);
            return performable.GetReportFragment(actor, lazyElement, formatter);
        }

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            lazyElement = lazyElement ?? actor.GetLazyElement(target);
            return performable.PerformAsAsync(actor, actor.GetAbility<BrowseTheWeb>().WebDriver, lazyElement, cancellationToken);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleElementPerformableAdapter"/> class with the specified performable and target.
        /// </summary>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">The target element for the performable.</param>
        public SingleElementPerformableAdapter(ISingleElementPerformable performable, ITarget target)
        {
            this.performable = performable ?? throw new ArgumentNullException(nameof(performable));
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SingleElementPerformableAdapter"/> class with the specified performable and target.
        /// </summary>
        /// <param name="performable">The performable to be adapted.</param>
        /// <param name="target">The target element for the performable.</param>
        /// <returns>A new instance of <see cref="SingleElementPerformableAdapter"/>.</returns>
        public static IPerformable From(ISingleElementPerformable performable, ITarget target)
            => new SingleElementPerformableAdapter(performable, target);
    }
}