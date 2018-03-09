using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.ScriptResources;
using CSF.Screenplay.Selenium.ScriptTasks;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Tasks
{
  /// <summary>
  /// A class which sets a date value by directly manipulating the value attribute via JavaScript.
  /// This is a fall-back position for setting dates, where no other mechanism works.
  /// </summary>
  public class EnterADateBySettingTheValue : Performable
  {
    readonly DateTime date;
    readonly ITarget target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    => $"{actor.Name} enters the date {date.ToString("yyyy-MM-dd")} into {target.GetName()} by setting the value directly via JavaScript";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var webElement = GetTheWebElement(actor);
      var dateValue = GetTheDateValue();

      var setTheDateWithJavaScript = new SetElementValueWithScript(webElement, dateValue);

      actor.Perform(setTheDateWithJavaScript);
    }

    IWebElement GetTheWebElement(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
      var webElementAdapter = target.GetWebElementAdapter(browseTheWeb);

      if(webElementAdapter == null)
        throw new TargetNotFoundException($"{target.GetName()} was not found");

      return webElementAdapter.GetUnderlyingElement();
    }

    string GetTheDateValue() => date.ToString("yyyy-MM-dd");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.EnterADateBySettingTheValue"/> class.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="target">Target.</param>
    public EnterADateBySettingTheValue(DateTime date, ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
      this.date = date;
    }
  }
}
