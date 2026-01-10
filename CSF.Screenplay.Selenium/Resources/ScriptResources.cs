using System.Resources;

namespace CSF.Screenplay.Selenium.Resources
{
    /// <summary>
    /// A resources class, providing access to pre-written/stock JavaScript strings.
    /// </summary>
    static class ScriptResources
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(ScriptResources));

        /// <summary>
        /// Gets a short JavaScript which clears the browser's local storage.
        /// </summary>
        public static string ClearLocalStorage => resourceManager.GetString("ClearLocalStorage");
    }
}