using System.IO;
using System.Reflection;
using System.Resources;

namespace CSF.Screenplay.Selenium.Resources
{
    /// <summary>
    /// A library of pre-written JavaScripts, stored in the assembly as resource strings.
    /// </summary>
    static class ScriptResources
    {
        static readonly Assembly thisAssembly = typeof(ScriptResources).Assembly;
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(ScriptResources).FullName, thisAssembly);

        /// <summary>Gets a short JavaScript for <see cref="Actions.ClearLocalStorage"/>.</summary>
        internal static string ClearLocalStorage => resourceManager.GetString("ClearLocalStorage");

        /// <summary>Gets a short JavaScript that returns the value of <c>document.readyState</c>.</summary>
        internal static string GetDocReadyState => resourceManager.GetString("GetDocReadyState");

        /// <summary>Gets a short JavaScript which sets the <c>value</c> of an HTML element.</summary>
        internal static string SetElementValue => resourceManager.GetString("SetElementValue");

        /// <summary>Gets a short JavaScript which sets the <c>value</c> of an HTML element in a way that simulates updating the element interactively.</summary>
        internal static string SetElementValueSimulatedInteractively => resourceManager.GetString("SetElementValueSimulatedInteractively");

        /// <summary>Gets a short JavaScript which gets a Shadow Root node from a Shadow Host element.</summary>
        internal static string GetShadowRoot => resourceManager.GetString("GetShadowRoot");

        /// <summary>Gets a JavaScript which begins capturing logs sent to the browser console.</summary>
        /// <remarks>
        /// <para>
        /// Note that the <see cref="GetLogs"/> script cannot read or see any logs which were written before this script was executed.
        /// </para>
        /// </remarks>
        internal static string CaptureLogs
        {
            get
            {
                using (var stream = thisAssembly.GetManifestResourceStream("CSF.Screenplay.Selenium.Resources.CaptureLogs.js"))
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }

        /// <summary>Gets a JavaScript which reads logs since either <see cref="CaptureLogs"/> or this method was last called, whichever was more recent.</summary>
        internal static string GetLogs
        {
            get
            {
                using (var stream = thisAssembly.GetManifestResourceStream("CSF.Screenplay.Selenium.Resources.GetLogs.js"))
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
    }
}