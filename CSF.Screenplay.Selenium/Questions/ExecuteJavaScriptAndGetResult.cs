using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// A question which executes some arbitrary JavaScript in the web browser and returns the result returned
    /// by that script.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The best way to use this action is via the builder method
    /// <see cref="PerformableBuilder.ExecuteAScript{TResult}(NamedScriptWithResult{TResult})"/> or one of its
    /// same-named overloads.
    /// The <see cref="NamedScriptWithResult{TResult}"/> class, and its counterparts with additional generic
    /// type parameters, provide a manner in which scripts to be executed by this action may be stored within
    /// the application or test logic, providing type safety for their parameters and return value.
    /// </para>
    /// <para>
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, sends the body of the
    /// script and its parameter values to the web browser and executes it. This is roughly equivalent to a user
    /// opening the developer tools Console, typing the script and its parameters there, and pressing enter,
    /// then reading the result which is returned by the console.
    /// This action returns the from the script back to the <see cref="IPerformance"/>. If the result is unimportant
    /// or you wish to execute a script which does not return a result then use <see cref="ExecuteJavaScript"/> instead.
    /// </para>
    /// <para>
    /// Within the script body, any parameters are accessible via the <c>arguments</c> object.  That object exposes the
    /// parameter values by their zero-based index (position) in the parameters list.  So, a script which takes three
    /// parameters will expose these to the script as <c>arguments[0]</c>, <c>arguments[1]</c> and
    /// <c>arguments[2]</c>, respectively for the first, second and third parameter values.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In this example, the script will return the integer value 10 to the performance.
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// var addFour = new NamedScriptWithResult&lt;int, int&gt;("4 + arguments[0]", "add four");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformableWithResult&lt;int&gt;
    /// public async ValueTask&lt;int&gt; PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     return await actor.PerformAsync(ExecuteAScript(addFour, 6), cancellationToken);
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="TResult">The expected type of the result from the script</typeparam>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{TResult}(NamedScriptWithResult{TResult})"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, TResult}(NamedScriptWithResult{T1, TResult}, T1)"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, T2, TResult}(NamedScriptWithResult{T1, T2, TResult}, T1, T2)"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, T2, T3, TResult}(NamedScriptWithResult{T1, T2, T3, TResult}, T1, T2, T3)"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, T2, T3, T4, TResult}(NamedScriptWithResult{T1, T2, T3, T4, TResult}, T1, T2, T3, T4)"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, T2, T3, T4, T5, TResult}(NamedScriptWithResult{T1, T2, T3, T4, T5, TResult}, T1, T2, T3, T4, T5)"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, T2, T3, T4, T5, T6, TResult}(NamedScriptWithResult{T1, T2, T3, T4, T5, T6, TResult}, T1, T2, T3, T4, T5, T6)"/>
    /// <seealso cref="PerformableBuilder.ExecuteAScript{T1, T2, T3, T4, T5, T6, T7, TResult}(NamedScriptWithResult{T1, T2, T3, T4, T5, T6, T7, TResult}, T1, T2, T3, T4, T5, T6, T7)"/>
    public class ExecuteJavaScriptAndGetResult<TResult> : IPerformableWithResult<TResult>, ICanReport
    {
        readonly string script;
        readonly string scriptName;
        readonly object[] arguments;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} executes a script named '{ScriptName}' in the browser", actor, scriptName);

        /// <inheritdoc/>
        public ValueTask<TResult> PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<BrowseTheWeb>();
            var executor = ability.GetJavaScriptExecutor();
            return new ValueTask<TResult>((TResult) executor.ExecuteScript(script, arguments));
        }

        /// <summary>
        /// Initialises a new instance of <see cref="ExecuteJavaScriptAndGetResult{TResult}"/>.
        /// </summary>
        /// <param name="script">The JavaScript to be executed.</param>
        /// <param name="scriptName">A human-readable name for the script, which may be displayed in a Screenplay report.</param>
        /// <param name="arguments">A collection of arguments/parameters to the script.</param>
        /// <exception cref="System.ArgumentException">If either <paramref name="script"/> or <paramref name="scriptName"/> are
        /// <see langword="null"/> or whitespace-only.</exception>
        public ExecuteJavaScriptAndGetResult(string script, string scriptName, params object[] arguments)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new System.ArgumentException($"'{nameof(script)}' cannot be null or whitespace.", nameof(script));

            this.script = script;
            this.scriptName = !string.IsNullOrWhiteSpace(scriptName) ? scriptName : "an unnamed script";
            this.arguments = arguments;
        }
    }
}