namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts no parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Developers are encouraged to represent JavaScripts that they wish to execute in the web browser using this type (and related types, see "See Also").
    /// Where the script accepts parameters and/or returns a result, choose a type which specifies appropriate generic type arguments for those parameters
    /// and returned type.
    /// </para>
    /// <para>
    /// For scripts which are created in this manner, getting an <see cref="IPerformable"/> or <see cref="IPerformableWithResult{TResult}"/> for those
    /// scripts is very easy and type safe.  Use the <c>ExecuteAScript</c> method from the <see cref="PerformableBuilder"/> and the compiler will
    /// use the generic type arguments of <em>this type</em> to select an appropriate overload which provides type-safety for specifying the parameter
    /// values and the return type, as appropriate.
    /// </para>
    /// <para>
    /// Another benefit is that scripts defined and stored in instances of this type are reusable and easily catalogued.  See the <see cref="Scripts"/> helper
    /// class for an example of a catalogue of script objects which may be executed.  Once again, developers are encouraged to follow this pattern in their
    /// own applications/tests which use Screenplay &amp; Selenium.
    /// </para>
    /// </remarks>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript : IHasName
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
    /// <remarks>
    /// <para>
    /// Whilst the generic type parameters of this type have no direct bearing upon its members (such as properties), they are very useful when
    /// combined with the builder methods <see cref="PerformableBuilder.ExecuteAScript(NamedScript)"/> and its many overloads.
    /// Developers are encouraged to represent JavaScripts that they wish to execute in the web browser using this type (and related types, see "See Also").
    /// Where the script accepts parameters and/or returns a result, choose a type which specifies appropriate generic type arguments for those parameters
    /// and returned type.
    /// </para>
    /// <para>
    /// For scripts which are created in this manner, getting an <see cref="IPerformable"/> or <see cref="IPerformableWithResult{TResult}"/> for those
    /// scripts is very easy and type safe.  Use the <c>ExecuteAScript</c> method from the <see cref="PerformableBuilder"/> and the compiler will
    /// use the generic type arguments of <em>this type</em> to select an appropriate overload which provides type-safety for specifying the parameter
    /// values and the return type, as appropriate.
    /// </para>
    /// <para>
    /// Another benefit is that scripts defined and stored in instances of this type are reusable and easily catalogued.  See the <see cref="Scripts"/> helper
    /// class for an example of a catalogue of script objects which may be executed.  Once again, developers are encouraged to follow this pattern in their
    /// own applications/tests which use Screenplay &amp; Selenium.
    /// </para>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 2 parameters and which returns no result.
    /// </summary>
    /// <inheritdoc cref="NamedScript{T1}" path="/remarks"/>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1,T2> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 3 parameters and which returns no result.
    /// </summary>
    /// <inheritdoc cref="NamedScript{T1}" path="/remarks"/>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1,T2,T3> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 4 parameters and which returns no result.
    /// </summary>
    /// <inheritdoc cref="NamedScript{T1}" path="/remarks"/>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1,T2,T3,T4> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 5 parameters and which returns no result.
    /// </summary>
    /// <inheritdoc cref="NamedScript{T1}" path="/remarks"/>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1,T2,T3,T4,T5> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 6 parameters and which returns no result.
    /// </summary>
    /// <inheritdoc cref="NamedScript{T1}" path="/remarks"/>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1,T2,T3,T4,T5,T6> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 7 parameters and which returns no result.
    /// </summary>
    /// <inheritdoc cref="NamedScript{T1}" path="/remarks"/>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScript<T1,T2,T3,T4,T5,T6,T7> : NamedScript
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }
}