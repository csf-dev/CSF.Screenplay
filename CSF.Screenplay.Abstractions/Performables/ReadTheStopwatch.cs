using System;
using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// An action which reads the current value of the stopwatch.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable requires the actor has the ability <see cref="UseAStopwatch"/>.
    /// Use this performable via the builder method <see cref="StopwatchBuilder.ReadTheStopwatch"/>.
    /// </para>
    /// </remarks>
    public class ReadTheStopwatch : IPerformableWithResult<TimeSpan>, ICanReport
    {
        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor) => string.Format(PerformableReportStrings.ReadTheStopwatchFormat, DefaultStrings.FormatValue(actor));

        /// <inheritdoc/>
        public ValueTask<TimeSpan> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<UseAStopwatch>();
            return new ValueTask<TimeSpan>(ability.Stopwatch.Elapsed);
        }
    }
}