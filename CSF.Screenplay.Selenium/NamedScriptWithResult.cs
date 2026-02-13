namespace CSF.Screenplay.Selenium
{
    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts no parameters, but which is expected to return a result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<TResult> : NamedScriptBasis
    {
        /// <summary>
        /// Initialises a new instance of <see cref="NamedScriptWithResult{TResult}"/>.
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript</param>
        /// <param name="name">The human-readable name of the script</param>
        /// <exception cref="System.ArgumentException">If either parameter is <see langword="null"/> or whitespace-only.</exception>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 1 parameter, but which is expected to return a result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<T1,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 2 parameters, but which is expected to return a result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<T1,T2,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 3 parameters, but which is expected to return a result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<T1,T2,T3,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 4 parameters, but which is expected to return a result.
    /// </summary>
    /// <remarks>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.ParamNote']/*"/>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript']/*"/>
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter</typeparam>
    /// <typeparam name="T2">The type of the second parameter</typeparam>
    /// <typeparam name="T3">The type of the third parameter</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter</typeparam>
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<T1,T2,T3,T4,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 5 parameters, but which is expected to return a result.
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
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<T1,T2,T3,T4,T5,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 6 parameters, but which is expected to return a result.
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
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
    public class NamedScriptWithResult<T1,T2,T3,T4,T5,T6,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }

    /// <summary>
    /// A model representing a pre-written piece of executable JavaScript, which accepts 7 parameters, but which is expected to return a result.
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
    /// <typeparam name="TResult">The expected result/return type from the script</typeparam>
    /// <include file="NamedScriptsCommonDocs.xml" path="doc/members/member[@name='M:CSF.Screenplay.Selenium.NamedScript.AllVoidTypes']/*"/>
    /// <seealso cref="NamedScriptWithResult{TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}"/>
    /// <seealso cref="NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}"/>
    public class NamedScriptWithResult<T1,T2,T3,T4,T5,T6,T7,TResult> : NamedScriptBasis
    {
        /// <inheritdoc cref="NamedScriptWithResult{TResult}(string, string)"/>
        public NamedScriptWithResult(string scriptBody, string name) : base(scriptBody, name) {}
    }
}