using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Actions
{
  /// <summary>
  /// Executes some JavaScript but does not return any result.
  /// </summary>
  /// <remarks>
  /// <para>
  /// In fact, this action just wraps an instance of <see cref="ExecuteJavaScriptAndGetResult"/> but discards
  /// the result.
  /// </para>
  /// </remarks>
  public class ExecuteJavaScript : Performable
  {
    readonly ExecuteJavaScriptAndGetResult innerScriptAction;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} executes some JavaScript in the browser";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
      => ((IPerformable<object>) innerScriptAction).PerformAs(actor);

    /// <summary>
    /// Performs the current action using a given <c>IWebDriver</c>.
    /// </summary>
    /// <param name="driver">The web driver.</param>
    public void PerformWith(IWebDriver driver) => innerScriptAction.PerformWith(driver);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Actions.ExecuteJavaScript"/> class.
    /// </summary>
    /// <param name="script">Script.</param>
    /// <param name="parameters">Parameters.</param>
    public ExecuteJavaScript(string script, params object[] parameters)
    {
      innerScriptAction = new ExecuteJavaScriptAndGetResult(script, parameters);
    }
  }
}
