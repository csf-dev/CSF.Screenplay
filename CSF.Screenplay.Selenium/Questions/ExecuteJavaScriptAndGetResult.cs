using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Questions
{
    /// <summary>
    /// An action which executes some arbitrary JavaScript in the web browser and gets a result.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable expects a result from the script.
    /// If the script does not return a result then use <see cref="ExecuteJavaScript"/> instead.
    /// </para>
    /// </remarks>
    /// <typeparam name="TResult">The expected type of the result from the script</typeparam>
    public class ExecuteJavaScriptAndGetResult<TResult> : IPerformableWithResult<TResult>, ICanReport
    {
        readonly string script;
        readonly string scriptName;
        readonly object[] arguments;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} executes the script named '{ScriptName}' in the browser", actor, scriptName);

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