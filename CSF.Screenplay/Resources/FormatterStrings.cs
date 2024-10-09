using System.Resources;

namespace CSF.Screenplay.Resources
{
    /// <summary>Provides access to localisable string values which relate to formatting.</summary>
    internal static class FormatterStrings
    {
        static readonly ResourceManager resourceManager = new ResourceManager(typeof(FormatterStrings).FullName, typeof(FormatterStrings).Assembly);

        /// <summary>Gets a string which is used to indicate a <see langword="null" /> value.</summary>
        internal static string NullValue => resourceManager.GetString("NullValue");
    }
}