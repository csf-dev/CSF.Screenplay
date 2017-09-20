using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// Executes some JavaScript and returns the result.
  /// </summary>
  public class ExecuteJavaScriptAndGetResult : Question<object>
  {
    readonly string script;
    readonly object[] parameters;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} executes some JavaScript in the browser and gets the result";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override object PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var ability = GetJavascriptAbility(actor);
      return ability.ExecuteScript(script, parameters);
    }

    /// <summary>
    /// Performs the current action using a given <c>IWebDriver</c>.
    /// </summary>
    /// <returns>The script result.</returns>
    /// <param name="driver">The web driver.</param>
    public object PerformWith(IWebDriver driver)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var jsDriver = GetJavascriptAbility(driver);
      return jsDriver.ExecuteScript(script, parameters);
    }

    IJavaScriptExecutor GetJavascriptAbility(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      try
      {
        return GetJavascriptAbility(ability.WebDriver);
      }
      catch(ArgumentException ex)
      {
        throw new MissingAbilityException($"{actor.Name} must have a {nameof(BrowseTheWeb)} ability which supports the execution of JavaScript.", ex);
      }
    }

    IJavaScriptExecutor GetJavascriptAbility(IWebDriver driver)
    {
      var jsDriver = driver as IJavaScriptExecutor;

      if(jsDriver == null)
        throw new ArgumentException($"The {nameof(IWebDriver)} must support the execution of JavaScript.", nameof(driver));

      return jsDriver;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Actions.ExecuteJavaScriptAndGetResult"/> class.
    /// </summary>
    /// <param name="script">Script.</param>
    /// <param name="parameters">Parameters.</param>
    public ExecuteJavaScriptAndGetResult(string script, params object[] parameters)
    {
      if(script == null)
        throw new ArgumentNullException(nameof(script));

      this.script = script;
      this.parameters = parameters;
    }
  }
}
