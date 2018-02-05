using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tasks
{
  /// <summary>
  /// A class which sets a date value by directly manipulating the value attribute via JavaScript.
  /// This is a fall-back position for setting dates, where no other mechanism works.
  /// </summary>
  public class EnterADateBySettingTheValue : Performable
  {
    readonly DateTime date;
    readonly ElementId target;

    ITarget Target => target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} enters the date {date.ToString("yyyy-MM-dd")} into {Target.GetName()} by setting the value directly via JavaScript";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var result = (bool) actor.Perform(Execute.TheJavaScript(Resources.Javascripts.SetValueById)
                                        .WithTheParameters(target.IdentifierValue, date.ToString("yyyy-MM-dd"))
                                        .AndGetTheResult());
      if(!result)
        throw new TargetNotFoundException($"No element with the id '{target.IdentifierValue}' could be found by JavaScript");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.EnterADateBySettingTheValue"/> class.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="target">Target.</param>
    public EnterADateBySettingTheValue(DateTime date, ElementId target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
      this.date = date;
    }
  }
}
