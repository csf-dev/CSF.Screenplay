namespace CSF.Screenplay.Selenium
{

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts no parameters and which returns no result.
    /// </summary>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript : NamedScriptBasis
    {
        /// <summary>
        /// Initialises a new instance of <see cref="NamedScript"/>.
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript</param>
        /// <param name="name">The human-readable name of the script</param>
        /// <exception cref="System.ArgumentException">If either parameter is <see langword="null"/> or whitespace-only.</exception>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 1 parameter and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1, T2}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 2 parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <seealso cref="NamedScript"/>
    /// <seealso cref="NamedScript{T1}"/>
    /// <seealso cref="NamedScript{T1, T2, T3}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6}"/>
    /// <seealso cref="NamedScript{T1, T2, T3, T4, T5, T6, T7}"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1,T2> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 3 parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
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
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1,T2,T3> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 4 parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
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
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1,T2,T3,T4> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 5 parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
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
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1,T2,T3,T4,T5> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 6 parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
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
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1,T2,T3,T4,T5,T6> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 7 parameters and which returns no result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
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
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllResultTypes']/*"/>
    public class NamedScript<T1,T2,T3,T4,T5,T6,T7> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScript(string, string)"/>
        public NamedScript(string scriptBody, string name) : base(scriptBody, name) {}
    }
}