namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts no parameters and which returns no result.
    /// </summary>
    public class NamedScript : IHasName
    {
        /// <summary>
        /// Gets the body of the JavaScript.
        /// </summary>
        public string ScriptBody { get; }

        /// <summary>
        /// Gets a human-readable name for this script, as may be displayed in a report.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initialises a new instance of <see cref="NamedScript"/>.
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript</param>
        /// <param name="name">The human-readable name of the script</param>
        public NamedScript(string scriptBody, string name)
        {
            if (string.IsNullOrWhiteSpace(scriptBody))
                throw new System.ArgumentException($"'{nameof(scriptBody)}' cannot be null or whitespace.", nameof(scriptBody));
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            ScriptBody = scriptBody;
            Name = name;
        }
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 1 parameter and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    public class NamedScript<T1> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 2 parameters and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    public class NamedScript<T1,T2> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 3 parameters and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    public class NamedScript<T1,T2,T3> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 4 parameters and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    public class NamedScript<T1,T2,T3,T4> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 5 parameters and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter</typeparam>
    public class NamedScript<T1,T2,T3,T4,T5> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 6 parameters and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter</typeparam>
    public class NamedScript<T1,T2,T3,T4,T5,T6> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 7 parameters and which returns no result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter</typeparam>
    public class NamedScript<T1,T2,T3,T4,T5,T6,T7> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }
}