namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// A basis class for named scripts, not for use in applications of Screenplay.
    /// </summary>
    public abstract class NamedScriptBasis : IHasName
    {
        /// <summary>
        /// Gets the body of the JavaScript.
        /// </summary>
        public string ScriptBody { get; }

        /// <summary>
        /// Gets a human-readable name for this script, as would be displayed in a report.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initialises a new instance of <see cref="NamedScriptBasis"/>
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript</param>
        /// <param name="name">The human-readable name of the script</param>
        /// <exception cref="System.ArgumentException">If either parameter is <see langword="null"/> or whitespace-only.</exception>
        protected internal NamedScriptBasis(string scriptBody, string name)
        {
            if (string.IsNullOrWhiteSpace(scriptBody))
                throw new System.ArgumentException($"'{nameof(scriptBody)}' cannot be null or whitespace.", nameof(scriptBody));
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            ScriptBody = scriptBody;
            Name = name;
        }
    }
}