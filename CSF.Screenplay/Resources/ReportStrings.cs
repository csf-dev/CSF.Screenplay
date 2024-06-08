using System.Resources;

namespace CSF.Screenplay.Resources
{
    /// <summary>Provides access to localisable string values which relate to reporting.</summary>
    internal static class ReportStrings
    {
        static readonly ResourceManager resourceManager = new ResourceManager(nameof(ReportStrings), typeof(ReportStrings).Assembly);

        /// <summary>Gets a string which is the 'fallback' report format for performables which do not implement <see cref="ICanReport"/>.</summary>
        internal static string FallbackReportFormat => resourceManager.GetString("FallbackReportFormat");

        /// <summary>Gets a string which is the 'fallback' report format for abilities which do not implement <see cref="ICanReport"/>.</summary>
        internal static string FallbackAbilityReportFormat => resourceManager.GetString("FallbackAbilityReportFormat");

        /// <summary>Gets a string format which indicates that there was an unexpected error whilst formatting an object in a report.</summary>
        internal static string ExceptionFormattingFormat => resourceManager.GetString("ExceptionFormattingFormat");

        /// <summary>Gets a name for an actor who has not been named.</summary>
        internal static string UnnamedActor => resourceManager.GetString("UnnamedActor");
        
    }
}