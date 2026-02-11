using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// A Screenplay action which pauses the performance for a specified amount of time.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Create instances of this action via the builder method <see cref="PerformableBuilder.WaitFor(TimeSpan)"/>.
    /// This wait action inserts an arbitrary pause into the progress of the <see cref="IPerformance"/>.
    /// For most kinds of waiting scenario, consider using <see cref="Wait"/> instead of this action.
    /// This kind of wait will always wait for the full time, wheras a <see cref="Wait"/> is configured with a
    /// condition/Predicate.  Once that condition is true it stops waiting and the performance is resumed.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In this example, Screenplay waits for 3 seconds.
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(WaitFor(TimeSpan.FromSeconds(3), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class WaitForSomeTime : IPerformable, ICanReport
    {
        readonly TimeSpan duration;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
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