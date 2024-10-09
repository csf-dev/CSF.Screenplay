using System.Resources;

namespace CSF.Screenplay.Resources
{
    /// <summary>Provides access to localisable string values which relate to abilities.</summary>
    internal static class AbilityReportStrings
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(AbilityReportStrings).FullName, typeof(AbilityReportStrings).Assembly);

        /// <summary>Gets a string which is the report format for <see cref="Abilities.UseAStopwatch"/>.</summary>
        internal static string UseAStopwatchFormat => resourceManager.GetString("UseAStopwatchFormat");
    }
}