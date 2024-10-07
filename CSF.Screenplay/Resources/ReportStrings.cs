using System.Resources;

namespace CSF.Screenplay.Resources
{
    /// <summary>Provides access to localisable string values which relate to formatting.</summary>
    internal static class ReportStrings
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(ReportStrings).FullName, typeof(ReportStrings).Assembly);

        /// <summary>Gets a format string which indicates a new actor has been created and joined the performance.</summary>
        internal static string ActorCreatedFormat => resourceManager.GetString("ActorCreatedFormat");

        /// <summary>Gets a format string which indicates that an actor has gained an ability.</summary>
        internal static string ActorGainedAbilityFormat => resourceManager.GetString("ActorGainedAbilityFormat");

        /// <summary>Gets a format string which indicates that an actor has been placed into the spotlight.</summary>
        internal static string ActorSpotlitFormat => resourceManager.GetString("ActorSpotlitFormat");

        /// <summary>Gets a string which indicates that the spotlight has been turned off.</summary>
        internal static string SpotlightTurnedOff => resourceManager.GetString("SpotlightTurnedOff");

        /// <summary>Gets a fallback report format string for an actor performing a performable which does not implement <see cref="ICanReport"/>.</summary>
        internal static string FallbackReportFormat => resourceManager.GetString("FallbackReportFormat");
    }
}