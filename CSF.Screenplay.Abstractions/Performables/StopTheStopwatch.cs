using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An action which stops the stopwatch.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable requires the actor has the ability <see cref="UseAStopwatch"/>.
    /// Use this performable via the builder method <see cref="StopwatchBuilder.StopTheStopwatch"/>.
    /// </para>
    /// </remarks>
    public class StopTheStopwatch : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format(PerformableReportStrings.StopTheStopwatchFormat, DefaultStrings.FormatValue(actor));

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<UseAStopwatch>();
            ability.Stopwatch.Stop();
            return default;
        }
    }
}