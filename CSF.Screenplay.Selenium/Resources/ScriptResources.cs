using System.Resources;

namespace CSF.Screenplay.Selenium.Resources
{
    /// <summary>
    /// A resources class, providing access to pre-written/stock JavaScript strings.
    /// </summary>
    static class ScriptResources
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(ScriptResources).FullName, typeof(ScriptResources).Assembly);

        /// <summary>Gets a short JavaScript for <see cref="Actions.ClearLocalStorage"/>.</summary>
        internal static string ClearLocalStorage => resourceManager.GetString("ClearLocalStorage");
    }
}