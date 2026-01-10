
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium
{
    public static partial class PerformableBuilder
    {
        /// <summary>
        /// Gets a a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript(NamedScript script)
            => new ExecuteJavaScript(script.ScriptBody, script.Name);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1>(NamedScript<T1> script, T1 p1)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1,T2>(NamedScript<T1,T2> script, T1 p1, T2 p2)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1, p2);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1,T2,T3>(NamedScript<T1,T2,T3> script, T1 p1, T2 p2, T3 p3)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1, p2, p3);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1,T2,T3,T4>(NamedScript<T1,T2,T3,T4> script, T1 p1, T2 p2, T3 p3, T4 p4)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1, p2, p3, p4);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <param name="p5">The value for the fifth script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="T5">The type of script parameter 5</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1,T2,T3,T4,T5>(NamedScript<T1,T2,T3,T4,T5> script, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1, p2, p3, p4, p5);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <param name="p5">The value for the fifth script parameter.</param>
        /// <param name="p6">The value for the sixth script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="T5">The type of script parameter 5</typeparam>
        /// <typeparam name="T6">The type of script parameter 6</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1,T2,T3,T4,T5,T6>(NamedScript<T1,T2,T3,T4,T5,T6> script, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1, p2, p3, p4, p5, p6);


        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <param name="p5">The value for the fifth script parameter.</param>
        /// <param name="p6">The value for the sixth script parameter.</param>
        /// <param name="p7">The value for the seventh script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="T5">The type of script parameter 5</typeparam>
        /// <typeparam name="T6">The type of script parameter 6</typeparam>
        /// <typeparam name="T7">The type of script parameter 7</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScript ExecuteAScript<T1,T2,T3,T4,T5,T6,T7>(NamedScript<T1,T2,T3,T4,T5,T6,T7> script, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7)
            => new ExecuteJavaScript(script.ScriptBody, script.Name, p1, p2, p3, p4, p5, p6, p7);

        /// <summary>
        /// Gets a a performable action which executes some JavaScript in the web browser, and gets the result returned by the script.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<TResult>(NamedScriptWithResult<TResult> script)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,TResult>(NamedScriptWithResult<T1,TResult> script, T1 p1)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,T2,TResult>(NamedScriptWithResult<T1,T2,TResult> script, T1 p1, T2 p2)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1, p2);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,T2,T3,TResult>(NamedScriptWithResult<T1,T2,T3,TResult> script, T1 p1, T2 p2, T3 p3)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1, p2, p3);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,T2,T3,T4,TResult>(NamedScriptWithResult<T1,T2,T3,T4,TResult> script, T1 p1, T2 p2, T3 p3, T4 p4)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1, p2, p3, p4);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <param name="p5">The value for the fifth script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="T5">The type of script parameter 5</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,T2,T3,T4,T5,TResult>(NamedScriptWithResult<T1,T2,T3,T4,T5,TResult> script, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1, p2, p3, p4, p5);

        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <param name="p5">The value for the fifth script parameter.</param>
        /// <param name="p6">The value for the sixth script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="T5">The type of script parameter 5</typeparam>
        /// <typeparam name="T6">The type of script parameter 6</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,T2,T3,T4,T5,T6,TResult>(NamedScriptWithResult<T1,T2,T3,T4,T5,T6,TResult> script, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1, p2, p3, p4, p5, p6);


        /// <summary>
        /// Gets a performable action which executes some JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the recommended way to execute JavaScript in the browser.  Use of the <see cref="NamedScript"/>, <see cref="NamedScriptWithResult{TResult}"/>
        /// and their other related generic types provides a simple type-safe mechanism of executing scripts.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="script">A named JavaScript.</param>
        /// <param name="p1">The value for the first script parameter.</param>
        /// <param name="p2">The value for the second script parameter.</param>
        /// <param name="p3">The value for the third script parameter.</param>
        /// <param name="p4">The value for the fourth script parameter.</param>
        /// <param name="p5">The value for the fifth script parameter.</param>
        /// <param name="p6">The value for the sixth script parameter.</param>
        /// <param name="p7">The value for the seventh script parameter.</param>
        /// <typeparam name="T1">The type of script parameter 1</typeparam>
        /// <typeparam name="T2">The type of script parameter 2</typeparam>
        /// <typeparam name="T3">The type of script parameter 3</typeparam>
        /// <typeparam name="T4">The type of script parameter 4</typeparam>
        /// <typeparam name="T5">The type of script parameter 5</typeparam>
        /// <typeparam name="T6">The type of script parameter 6</typeparam>
        /// <typeparam name="T7">The type of script parameter 7</typeparam>
        /// <typeparam name="TResult">The expected return type of the script</typeparam>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptAndGetResult<TResult> ExecuteAScript<T1,T2,T3,T4,T5,T6,T7,TResult>(NamedScriptWithResult<T1,T2,T3,T4,T5,T6,T7,TResult> script, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7)
            => new ExecuteJavaScriptAndGetResult<TResult>(script.ScriptBody, script.Name, p1, p2, p3, p4, p5, p6, p7);

        /// <summary>
        /// Gets a builder for a performable action which executes some arbitrary JavaScript in the web browser.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may be used for building an arbitrary script, but where possible it is recommended to use one of the overloads of
        /// <see cref="ExecuteAScript"/>.  These methods accept either a <see cref="NamedScript"/>, a <see cref="NamedScriptWithResult{TResult}"/>,
        /// or one of their other related generic types.  These overloads provide type safety for parameters, return types, and ensure
        /// that the name of the script is pre-specified at design time.
        /// </para>
        /// <para>
        /// Scripts executed with this method are not expected to return a result.  If they do, their result is discarded and unused.
        /// </para>
        /// </remarks>
        /// <param name="scriptBody">The body of the JavaScript to execute.</param>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptBuilder ExecuteCustomScript(string scriptBody) => new ExecuteJavaScriptBuilder(scriptBody);

        /// <summary>
        /// Gets a builder for a performable action which executes some JavaScript in the web browser and returns a result from that script.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This action is for executing a script which is expected to return a result.
        /// This method may be used for building an arbitrary script, but where possible it is recommended to use one of the overloads of this
        /// method which accepts either a <see cref="NamedScript"/>, a <see cref="NamedScriptWithResult{TResult}"/>, or one of their other related
        /// generic types.  These overloads provide type safety for parameters, return types, and ensure that the name of the script is
        /// pre-specified at design time.
        /// </para>
        /// </remarks>
        /// <param name="scriptBody">The body of the JavaScript to execute.</param>
        /// <returns>A builder object</returns>
        public static ExecuteJavaScriptBuilderWithResult<TResult> ExecuteCustomScriptWithResult<TResult>(string scriptBody)
            => new ExecuteJavaScriptBuilderWithResult<TResult>(scriptBody);
    }
}