using System.Resources;

namespace CSF.Screenplay.WebApis.Resources
{
    /// <summary>Provides access to localisable string values which relate to performables.</summary>
    internal static class PerformableReportStrings
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(PerformableReportStrings).FullName, typeof(PerformableReportStrings).Assembly);

        /// <summary>Gets a string which is the report format for <see cref="SendTheHttpRequest"/>.</summary>
        internal static string SendTheHttpRequestFormat => resourceManager.GetString("SendTheHttpRequestFormat");

        /// <summary>Gets a string which is the report format for <see cref="SendTheHttpRequestAndGetTheResponse{TResponse}"/>.</summary>
        internal static string SendTheHttpRequestAndGetTheResponseFormat => resourceManager.GetString("SendTheHttpRequestAndGetTheResponseFormat");

        /// <summary>Gets a string which is the report format for <see cref="SendTheHttpRequestAndGetJsonResponse{TResponse}"/>.</summary>
        internal static string SendTheHttpRequestAndGetJsonResponseFormat => resourceManager.GetString("SendTheHttpRequestAndGetJsonResponseFormat");

        /// <summary>Gets a string which is the report format for <see cref="Performables.ReadTheStopwatch"/>.</summary>
        internal static string ReadTheStopwatchFormat => resourceManager.GetString("ReadTheStopwatchFormat");
    }
}