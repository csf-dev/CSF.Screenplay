namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// A repository of named JavaScripts which are distributed with Screenplay's Selenium integration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Developers who use Screenplay &amp; Selenium and who need to develop their own custom JavaScripts are encouraged
    /// to devise their own classes similar to this one.
    /// This provides a rapid and type-safe mechanism of retrieving and reusing well-known/predefined JavaScripts.
    /// </para>
    /// </remarks>
    public static class Scripts
    {
        /// <summary>
        /// Gets a <see cref="NamedScript"/> which clears the Local Storage of the browser, for the current domain.
        /// </summary>
        /// <returns>A named script.</returns>
        public static NamedScript ClearLocalStorage
            => new NamedScript(Resources.ScriptResources.ClearLocalStorage, "clear the local storage");
    }
}