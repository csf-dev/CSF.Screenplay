using System.Resources;

namespace CSF.Screenplay.Resources
{
    /// <summary>Provides access to localisable string values which relate to performables.</summary>
    internal static class PerformableReportStrings
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(PerformableReportStrings).FullName, typeof(PerformableReportStrings).Assembly);

        /// <summary>Gets a string which is the report format for <see cref="Performables.StopTheStopwatch"/>.</summary>
        internal static string StopTheStopwatchFormat => resourceManager.GetString("StopTheStopwatchFormat");

        /// <summary>Gets a string which is the report format for <see cref="Performables.StartTheStopwatch"/>.</summary>
        internal static string StartTheStopwatchFormat => resourceManager.GetString("StartTheStopwatchFormat");

        /// <summary>Gets a string which is the report format for <see cref="Performables.ResetTheStopwatch"/>.</summary>
        internal static string ResetTheStopwatchFormat => resourceManager.GetString("ResetTheStopwatchFormat");

        /// <summary>Gets a string which is the report format for <see cref="Performables.ReadTheStopwatch"/>.</summary>
        internal static string ReadTheStopwatchFormat => resourceManager.GetString("ReadTheStopwatchFormat");
    }
}