using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds an action which executes JavaScript on the page.
  /// </summary>
  public class Execute
  {
    readonly string script;
    object[] parameters;

    /// <summary>
    /// Gets a builder instance for a given piece of script.
    /// </summary>
    /// <returns>The java script.</returns>
    /// <param name="script">Script.</param>
    public static Execute TheJavaScript(string script) => new Execute(script);

    /// <summary>
    /// Indicates that the given parameters are to be passed to the script, via the <c>arguments</c> biult-in
    /// JavaScript keyword.
    /// </summary>
    /// <returns>The parameters.</returns>
    /// <param name="parameters">Parameters.</param>
    public Execute WithTheParameters(params object[] parameters)
    {
      this.parameters = parameters;
      return this;
    }

    /// <summary>
    /// Gets the action; a result from the script will be returned.
    /// </summary>
    /// <returns>The action.</returns>
    public ExecuteJavaScriptAndGetResult AndGetTheResult() => new ExecuteJavaScriptAndGetResult(script, parameters);

    /// <summary>
    /// Gets the action; any result from the script will be discarded.
    /// </summary>
    /// <returns>The action.</returns>
    public ExecuteJavaScript AndIgnoreTheResult() => new ExecuteJavaScript(script, parameters);

    Execute(string script)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));

      this.script = script;
      parameters = new object[0];
    }
  }
}
