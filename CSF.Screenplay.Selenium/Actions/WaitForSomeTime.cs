using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A performable action that waits for a specified amount of time.
    /// </summary>
    public class WaitForSomeTime : IPerformable, ICanReport
    {
        readonly TimeSpan duration;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} waits for {Duration}", actor, duration);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
            => new ValueTask(Task.Delay(duration, cancellationToken));

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitForSomeTime"/> class.
        /// </summary>
        /// <param name="duration">The amount of time to wait.</param>
        public WaitForSomeTime(TimeSpan duration)
        {
            this.duration = duration;
        }
    }
}