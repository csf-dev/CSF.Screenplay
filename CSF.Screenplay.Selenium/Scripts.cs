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
    /// See <xref href="NamedScriptsArticle?text=the+named+scripts+article"/> for more information about defining
    /// scripts with human-readable names and type safety.
    /// </para>
    /// </remarks>
    public static class Scripts
    {
        /// <summary>
        /// Gets a <see cref="NamedScript"/> which clears the Local Storage of the browser, for the current domain.
        /// </summary>
        public static NamedScript ClearLocalStorage
            => new NamedScript(Resources.ScriptResources.ClearLocalStorage, "clear the local storage");

        /// <summary>
        /// Gets a <see cref="NamedScriptWithResult{TResult}"/> which gets the value of <c>document.readyState</c> for the
        /// current page.
        /// </summary>
        /// <remarks>
        /// <para>You may use this script to determine whether the page has finished loading.</para>
        /// </remarks>
        public static NamedScriptWithResult<string> GetTheDocumentReadyState
            => new NamedScriptWithResult<string>(Resources.ScriptResources.GetDocReadyState, "get the readiness of the current page");

        /// <summary>
        /// Gets a <see cref="NamedScript{T1, T2}"/> which sets the <c>value</c> of a specified HTML element.
        /// </summary>
        public static NamedScript<OpenQA.Selenium.IWebElement, object> SetElementValue
            => new NamedScript<OpenQA.Selenium.IWebElement, object>(Resources.ScriptResources.SetElementValue, "set the element's value");

        /// <summary>
        /// Gets a <see cref="NamedScript{T1, T2}"/> which sets the <c>value</c> of a specified HTML element, simulating setting it interactively.
        /// </summary>
        public static NamedScript<OpenQA.Selenium.IWebElement, object> SetElementValueSimulatedInteractively
            => new NamedScript<OpenQA.Selenium.IWebElement, object>(Resources.ScriptResources.SetElementValueSimulatedInteractively, "simulate setting the element's value interactively");

        /// <summary>
        /// Gets a <see cref="NamedScriptWithResult{T1, TResult}"/> which gets the shadow root contained within the specified shadow host.
        /// </summary>
        public static NamedScriptWithResult<OpenQA.Selenium.IWebElement, OpenQA.Selenium.ISearchContext> GetShadowRoot
            => new NamedScriptWithResult<OpenQA.Selenium.IWebElement, OpenQA.Selenium.ISearchContext>(Resources.ScriptResources.GetShadowRoot, "get a shadow root");
    }
}