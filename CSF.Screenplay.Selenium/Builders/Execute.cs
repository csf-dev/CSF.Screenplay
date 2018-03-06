using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.StoredScripts;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds an action which executes JavaScript on the page.
  /// </summary>
  public class Execute
  {
    readonly string script;
    readonly IProvidesScript scriptProvider;
    object[] parameters;

    /// <summary>
    /// Gets a builder instance for a given piece of script.
    /// </summary>
    /// <returns>A JavaScript execution builder.</returns>
    /// <param name="script">Script.</param>
    public static Execute TheJavaScript(string script) => new Execute(script);

    /// <summary>
    /// Gets a builder instance for a given script provider type.
    /// </summary>
    /// <returns>A JavaScript execution builder.</returns>
    /// <typeparam name="TProvider">The JavaScript provider type.</typeparam>
    public static Execute TheJavaScript<TProvider>() where TProvider : IProvidesScript,new()
      => new Execute(new TProvider());

    /// <summary>
    /// Gets a builder instance for a given script provider.
    /// </summary>
    /// <param name="provider">A script provider.</param>
    /// <returns>A JavaScript execution builder.</returns>
    public static Execute TheJavaScript(IProvidesScript provider) => new Execute(provider);

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
    public IPerformableJavaScriptWithResult AndGetTheResult()
    {
      if(scriptProvider != null)
        return new ExecuteJavaScriptProviderAndGetResult(scriptProvider, parameters);
      
      return new ExecuteJavaScriptAndGetResult(script, parameters);
    }

    /// <summary>
    /// Gets the action; any result from the script will be discarded.
    /// </summary>
    /// <returns>The action.</returns>
    public IPerformableJavaScript AndIgnoreTheResult()
    {
      if(scriptProvider != null)
        return new ExecuteJavaScriptProvider(scriptProvider, parameters);

      return new ExecuteJavaScript(script, parameters);
    }

    Execute(string script)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));

      this.script = script;
      parameters = new object[0];
    }

    Execute(IProvidesScript scriptProvider)
    {
      if(scriptProvider == null)
        throw new ArgumentNullException(nameof(scriptProvider));
      
      this.scriptProvider = scriptProvider;
      parameters = new object[0];
    }
  }
}
