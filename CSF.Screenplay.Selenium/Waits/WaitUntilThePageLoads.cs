using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.ScriptResources;
using CSF.Screenplay.Stopwatch;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Waits
{
  /// <summary>
  /// A wait operation which waits until the page 'ready state' indicates that the page is loaded.
  /// </summary>
  public class WaitUntilThePageLoads : Performable
  {
    const string COMPLETE = "complete";

    readonly IDurationFormatter durationFormatter;
    readonly TimeSpan timeout;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      var timeoutString = durationFormatter.GetDuration(timeout);
      return $"{actor.Name} waits for at most {timeoutString} or until the page has loaded";
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      var action = Execute.JavaScript.WhichGetsTheDocumentReadyState();
      var wait = new WebDriverWait(ability.WebDriver, timeout);

      try
      {
        wait.Until(driver => Equals(action.PerformWith(driver), COMPLETE));
      }
      catch(WebDriverTimeoutException ex)
      {
        throw new GivenUpWaitingException("Given up waiting", ex);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Waits.WaitUntilThePageLoads"/> class.
    /// </summary>
    /// <param name="timeout">Timeout.</param>
    public WaitUntilThePageLoads(TimeSpan timeout)
    {
      this.timeout = timeout;
      durationFormatter = new TimeSpanFormatter();
    }
  }
}
