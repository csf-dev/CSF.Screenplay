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
    }
}