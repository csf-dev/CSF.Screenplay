using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Builders
{
    /// <summary>
    /// Base type for a builder which facilitates the execution of JavaScript in the web browser.
    /// </summary>
    public abstract class ExecuteJavaScriptBuilderBase
    {
        /// <summary>
        /// Gets the body of the script to be executed.
        /// </summary>
        protected string ScriptBody { get; }

        /// <summary>
        /// Gets the human-readable name of the script.
        /// </summary>
        protected string Name { get; private set; }

        /// <summary>
        /// Gets a collection of the arguments to be provided to the script.
        /// </summary>
        protected object[] Arguments { get; private set; }

        /// <summary>
        /// Sets a human-readable name for the script, which may be displayed in Screenplay reports.
        /// </summary>
        /// <param name="name">The human-readable name for the script</param>
        /// <returns>This same builder object, so calls may be chained</returns>
        /// <exception cref="ArgumentException">If the name is <see langword="null"/> or a whitepace-only string</exception>
        /// <exception cref="InvalidOperationException">If the name has already been set</exception>
        public ExecuteJavaScriptBuilderBase WithTheName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            if(Name != null)
                throw new InvalidOperationException("The name has already been set; it may not be set again.");
            Name = name;
            return this;
        }

        /// <summary>
        /// Sets the arguments (parameters) for the script.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is optional; if the script requires no parameters then it is unneccesary to use this method.
        /// </para>
        /// </remarks>
        /// <param name="arguments">A collection of parameter values</param>
        /// <returns>This same builder object, so calls may be chained</returns>
        /// <exception cref="InvalidOperationException">If the arguments have already been set</exception>
        public ExecuteJavaScriptBuilderBase WithTheArguments(params object[] arguments)
        {
            if(Arguments != null)
                throw new InvalidOperationException("The arguments have already been set; they may not be set again.");
            Arguments = arguments;
            return this;
        }

        /// <summary>
        /// Initialises a new instance of this builder.
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript to be executed.</param>
        /// <exception cref="ArgumentException">If <paramref name="scriptBody"/> is <see langword="null"/> or a whitepace-only string</exception>
        protected ExecuteJavaScriptBuilderBase(string scriptBody)
        {
            if (string.IsNullOrWhiteSpace(scriptBody))
                throw new ArgumentException($"'{nameof(scriptBody)}' cannot be null or whitespace.", nameof(scriptBody));

            ScriptBody = scriptBody;
        }
    }

    /// <summary>
    /// A builder type for executing JavaScript in the browser which is not expected to return a result value.
    /// </summary>
    public class ExecuteJavaScriptBuilder : ExecuteJavaScriptBuilderBase, IGetsPerformable
    {
        /// <inheritdoc cref="ExecuteJavaScriptBuilderBase.WithTheName(string)"/>
        public new ExecuteJavaScriptBuilder WithTheName(string name)
            => (ExecuteJavaScriptBuilder) base.WithTheName(name);

        /// <inheritdoc cref="ExecuteJavaScriptBuilderBase.WithTheArguments(object[])"/>
        public new ExecuteJavaScriptBuilder WithTheArguments(params object[] arguments)
            => (ExecuteJavaScriptBuilder) base.WithTheArguments(arguments);

        IPerformable IGetsPerformable.GetPerformable()
            => new ExecuteJavaScript(ScriptBody, Name, Arguments ?? Array.Empty<object>());

        /// <summary>
        /// Initialises a new instance of <see cref="ExecuteJavaScriptBuilder"/>.
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript to be executed.</param>
        /// <exception cref="ArgumentException">If <paramref name="scriptBody"/> is <see langword="null"/> or a whitepace-only string</exception>
        public ExecuteJavaScriptBuilder(string scriptBody) : base(scriptBody) {}
    }

    /// <summary>
    /// A builder type for executing JavaScript in the browser, where the script is expected to return a result value.
    /// </summary>
    public class ExecuteJavaScriptBuilderWithResult<TResult> : ExecuteJavaScriptBuilderBase, IGetsPerformableWithResult<TResult>
    {
        /// <inheritdoc cref="ExecuteJavaScriptBuilderBase.WithTheName(string)"/>
        public new ExecuteJavaScriptBuilderWithResult<TResult> WithTheName(string name)
            => (ExecuteJavaScriptBuilderWithResult<TResult>) base.WithTheName(name);

        /// <inheritdoc cref="ExecuteJavaScriptBuilderBase.WithTheArguments(object[])"/>
        public new ExecuteJavaScriptBuilderWithResult<TResult> WithTheArguments(params object[] arguments)
            => (ExecuteJavaScriptBuilderWithResult<TResult>) base.WithTheArguments(arguments);

        IPerformableWithResult<TResult> IGetsPerformableWithResult<TResult>.GetPerformable()
            => new ExecuteJavaScriptAndGetResult<TResult>(ScriptBody, Name, Arguments ?? Array.Empty<object>());

        /// <summary>
        /// Initialises a new instance of <see cref="ExecuteJavaScriptBuilderWithResult{TResult}"/>.
        /// </summary>
        /// <param name="scriptBody">The body of the JavaScript to be executed.</param>
        /// <exception cref="ArgumentException">If <paramref name="scriptBody"/> is <see langword="null"/> or a whitepace-only string</exception>
        public ExecuteJavaScriptBuilderWithResult(string scriptBody) : base(scriptBody) {}
    }
}