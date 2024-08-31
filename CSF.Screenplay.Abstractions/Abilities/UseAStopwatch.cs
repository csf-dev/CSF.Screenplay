using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Abilities
{
    /// <summary>
    /// An ability that enables an actor to make use of a <see cref="System.Diagnostics.Stopwatch"/> to accurately
    /// measure the passage of time.
    /// </summary>
    public class UseAStopwatch : ICanReport
    {
        /// <summary>
        /// Gets the stopwatch granted to the actor by this ability.
        /// </summary>
        public System.Diagnostics.Stopwatch Stopwatch { get; } = new System.Diagnostics.Stopwatch();

        /// <inheritdoc/>
        public string GetReportFragment(IHasName actor) => string.Format(AbilityReportStrings.UseAStopwatchFormat, DefaultStrings.FormatValue(actor));
    }
}