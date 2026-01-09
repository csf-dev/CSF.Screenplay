using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An action which resets the stopwatch.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable requires the actor has the ability <see cref="UseAStopwatch"/>.
    /// Use this performable via the builder method <see cref="StopwatchBuilder.ResetTheStopwatch"/>.
    /// </para>
    /// </remarks>
    public class ResetTheStopwatch : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format(PerformableReportStrings.ResetTheStopwatchFormat, actor);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<UseAStopwatch>();
            ability.Stopwatch.Reset();
            return default;
        }
    }
}