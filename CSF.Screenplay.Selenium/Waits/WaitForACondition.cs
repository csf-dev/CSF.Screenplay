using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Stopwatch;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSF.Screenplay.Selenium.Waits
{
  /// <summary>
  /// Wait action waits until a condition is true.
  /// </summary>
  public class WaitForACondition : Performable
  {
    readonly Func<IWebDriver,bool> condition;
    readonly string conditionName;
    readonly TimeSpan? timeout;
    readonly IFormatsDurations durationFormatter;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      var formattedTime = durationFormatter.GetDuration(GetDuration.ToWait(timeout, actor));
      return $"{actor.Name} waits for {conditionName} or until {formattedTime} has passed";
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      var wait = new WebDriverWait(ability.WebDriver, GetDuration.ToWait(timeout, actor));

      try
      {
        wait.Until(condition);
      }
      catch(WebDriverTimeoutException ex)
      {
        throw new GivenUpWaitingException("Given up waiting", ex);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Waits.WaitForACondition"/> class.
    /// </summary>
    /// <param name="condition">Condition.</param>
    /// <param name="conditionName">Condition name.</param>
    /// <param name="timeout">Timeout.</param>
    public WaitForACondition(Func<IWebDriver,bool> condition, string conditionName, TimeSpan? timeout)
    {
      if(conditionName == null)
        throw new ArgumentNullException(nameof(conditionName));
      if(condition == null)
        throw new ArgumentNullException(nameof(condition));
      
      this.conditionName = conditionName;
      this.condition = condition;
      this.timeout = timeout;

      durationFormatter = new TimeSpanFormattingStrategy();
    }
  }
}
