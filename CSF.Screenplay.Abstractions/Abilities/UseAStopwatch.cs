using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Abilities
{
    /// <summary>
    /// An ability that enables an actor to make use of a <see cref="System.Diagnostics.Stopwatch"/> to accurately
    /// measure the passage of time.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this ability with the actions which exposed by
    /// <see cref="CSF.Screenplay.Performables.StopwatchBuilder"/>.
    /// This ability wraps a <see cref="System.Diagnostics.Stopwatch"/> instance, allowing the actor
    /// to control &amp; read it from the related actions.
    /// </para>
    /// </remarks>
    public class UseAStopwatch : ICanReport
    {
        /// <summary>
        /// Gets the stopwatch granted to the actor by this ability.
        /// </summary>
        public System.Diagnostics.Stopwatch Stopwatch { get; } = new System.Diagnostics.Stopwatch();

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter)
            => formatter.Format(AbilityReportStrings.UseAStopwatchFormat, actor);
    }
}
