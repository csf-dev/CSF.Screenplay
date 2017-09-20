using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Resources;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Web.Waits
{
  /// <summary>
  /// A wait operation which waits until the page 'ready state' indicates that the page is loaded.
  /// </summary>
  public class WaitUntilThePageLoads : Performable
  {
    const string COMPLETE = "complete";

    readonly TimeSpan timeout;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} waits until the page has loaded";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      var action = GetAction();
      var wait = new WebDriverWait(ability.WebDriver, timeout);

      wait.Until(driver => Equals(action.PerformWith(driver), COMPLETE));
    }

    ExecuteJavaScriptAndGetResult GetAction()
    {
      var script = Javascripts.GetDocumentReadyState;
      return Execute.TheJavaScript(script).AndGetTheResult();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Waits.WaitUntilThePageLoads"/> class.
    /// </summary>
    /// <param name="timeout">Timeout.</param>
    public WaitUntilThePageLoads(TimeSpan timeout)
    {
      this.timeout = timeout;
    }
  }
}
