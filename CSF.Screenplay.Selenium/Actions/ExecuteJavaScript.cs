using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which executes some arbitrary JavaScript in the web browser.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This performable does not expect or return any result from the script.
    /// If the script returns a result then use <see cref="ExecuteJavaScriptAndGetResult{TResult}"/> instead.
    /// </para>
    /// </remarks>
    public class ExecuteJavaScript : IPerformable, ICanReport
    {
        readonly string script;
        readonly string scriptName;
        readonly object[] arguments;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} executes {ScriptName} as JavaScript in the browser", actor, scriptName);

        /// <inheritdoc/>
        public ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
        {
            var ability = actor.GetAbility<BrowseTheWeb>();
            var executor = ability.GetJavaScriptExecutor();
            executor.ExecuteScript(script, arguments);
            return default;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="ExecuteJavaScript"/>.
        /// </summary>
        /// <param name="script">The JavaScript to be executed.</param>
        /// <param name="scriptName">A human-readable name for the script, which may be displayed in a Screenplay report.</param>
        /// <param name="arguments">A collection of arguments/parameters to the script.</param>
        /// <exception cref="System.ArgumentException">If either <paramref name="script"/> or <paramref name="scriptName"/> are
        /// <see langword="null"/> or whitespace-only.</exception>
        public ExecuteJavaScript(string script, string scriptName, params object[] arguments)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new System.ArgumentException($"'{nameof(script)}' cannot be null or whitespace.", nameof(script));

            this.script = script;
            this.scriptName = !string.IsNullOrWhiteSpace(scriptName) ? scriptName : "an unnamed script";
            this.arguments = arguments;
        }
    }
}