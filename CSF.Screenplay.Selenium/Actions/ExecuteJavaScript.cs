using System.Threading;
using System.Threading.Tasks;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Actions
{
    /// <summary>
    /// An action which sends a JavaScript string to a web browser and executes it.
    /// This action ignores the result (if any) from that script.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The best way to use this action is via the builder method
    /// <see cref="PerformableBuilder.ExecuteAScript(NamedScript)"/> or one of its same-named overloads.
    /// The <see cref="NamedScript"/> class, and its counterparts with generic type parameters, provide a manner
    /// in which scripts to be executed by this action may be stored within the application or test logic, providing
    /// type safety for their parameters.
    /// </para>
    /// <para>
    /// Performing this action, as an actor which has the <see cref="BrowseTheWeb"/> ability, sends the body of the
    /// script and its parameter values to the web browser and executes it. This is roughly equivalent to a user
    /// opening the developer tools Console, typing the script and its parameters there, and pressing enter.
    /// This action does not return any result from the script, so it is useful only when the script to execute
    /// does not return a result, or when the intent is to ignore the result.  If the result is important then use
    /// <see cref="ExecuteJavaScriptAndGetResult{TResult}"/> instead.
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
    /// In this example, the script will write "I can count to 5" into the developer console.
    /// </para>
    /// <code>
    /// using static CSF.Screenplay.Selenium.PerformableBuilder;
    /// 
    /// var iCanCount = new NamedScript&lt;int&gt;("console.log('I can count to ' + arguments[0])", "my counting script");
    /// 
    /// // Within the logic of a custom task, deriving from IPerformable
    /// public async ValueTask PerformAsAsync(ICanPerform actor, CancellationToken cancellationToken = default)
    /// {
    ///     // ... other performance logic
    ///     await actor.PerformAsync(ExecuteAScript(iCanCount, 5), cancellationToken);
    ///     // ... other performance logic
    /// }
    /// </code>
    /// </example>
    public class ExecuteJavaScript : IPerformable, ICanReport
    {
        readonly string script;
        readonly string scriptName;
        readonly object[] arguments;

        /// <inheritdoc/>
        public ReportFragment GetReportFragment(Actor actor, IFormatsReportFragment formatter)
            => formatter.Format("{Actor} executes a script named '{ScriptName}' in the browser", actor, scriptName);

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