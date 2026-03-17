using System.Resources;

namespace CSF.Screenplay.Selenium.Resources
{
    /// <summary>
    /// Message strings, stored in the assembly as resources.
    /// </summary>
    static class MessageResources
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(MessageResources).FullName, typeof(MessageResources).Assembly);

        /// <summary>Gets human-readable message indicating a nonexistent element.</summary>
        internal static string NonExistentElement =>  resourceManager.GetString("NonExistentElement");

        /// <summary>Gets a human-readable message indicating an unknown HTML element.</summary>
        internal static string UnknownElement =>  resourceManager.GetString("UnknownElement");
    }
}