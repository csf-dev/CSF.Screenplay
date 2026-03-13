using System;
using System.Text.Json.Serialization;

namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Model represents anything which may be reported-upon within an <see cref="IPerformance"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This base model has subclasses for each of the specific types of event which may be reported-upon.
    /// </para>
    /// </remarks>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
    [JsonDerivedType(typeof(ActorCreatedReport), nameof(ActorCreatedReport))]
    [JsonDerivedType(typeof(ActorGainedAbilityReport), nameof(ActorGainedAbilityReport))]
    [JsonDerivedType(typeof(ActorSpotlitReport), nameof(ActorSpotlitReport))]
    [JsonDerivedType(typeof(SpotlightTurnedOffReport), nameof(SpotlightTurnedOffReport))]
    [JsonDerivedType(typeof(PerformableReport), nameof(PerformableReport))]
    public abstract class ReportableModelBase
    {
        /// <summary>
        /// Gets or sets the human-readable text of the report.
        /// </summary>
        public string Report { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the <see cref="Actor"/> who is associated with this report.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Almost all reportables involve an actor, it is rare for this value to be unset (IE: <see langword="null" />).
        /// </para>
        /// </remarks>
        public string ActorName { get; set; }

        /// <summary>
        /// Gets or sets the relative time at which this reportable event occurred.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For many types of reportable items (derived from this type), only the start time is recorded, because it is expected that the
        /// activity upon which is being reported takes a trivial amount of time.
        /// For <see cref="PerformableReport"/> instances, an end time is also recorded, as these are expected to take an appreciable amount of time.
        /// </para>
        /// <para>
        /// This property is expressed as an amount of time since the Screenplay began.  The beginning of the Screenplay is recorded in the
        /// report metadata, at <see cref="ReportMetadata.Timestamp"/>.
        /// </para>
        /// <para>
        /// Recall that it is quite normal for performances and thus reportable actions to occur in parallel.
        /// Do not be alarmed if it appears that unrelated performances are interleaved with regard to their timings.
        /// </para>
        /// </remarks>
        /// <seealso cref="ReportMetadata.Timestamp"/>
        /// <seealso cref="PerformanceReport.Started"/>
        /// <seealso cref="PerformableReport.Ended"/>
        public TimeSpan Started { get; set; }
    }
}