using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An action which starts the stopwatch.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable requires the actor has the ability <see cref="UseAStopwatch"/>.
    /// Use this performable via the builder method <see cref="StopwatchBuilder.StartTheStopwatch"/>.
    /// </para>
    /// </remarks>
    public class StartTheStopwatch : IPerformable, ICanReport
    {
        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor) => string.Format(PerformableReportStrings.StartTheStopwatchFormat, DefaultStrings.FormatValue(actor));

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<UseAStopwatch>();
            ability.Stopwatch.Start();
            return default;
        }
    }
}