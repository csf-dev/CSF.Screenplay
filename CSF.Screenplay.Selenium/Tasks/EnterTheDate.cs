﻿using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tasks
{
  /// <summary>
  /// A task which enters a date value into an input element (of type "date") in a cross-browser manner.
  /// </summary>
  public class EnterTheDate : Performable
  {
    readonly DateTime date;
    readonly ITarget target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    => $"{actor.Name} enters the date {date.ToString("yyyy-MM-dd")} into {target.GetName()}";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      var useLocaleFormat = ability.GetCapability(Capabilities.EnterDatesInLocaleFormat);
      var useIsoFormat = ability.GetCapability(Capabilities.EnterDatesAsIsoStrings);

      if(useLocaleFormat)
      {
        actor.Perform(EnterTheDateInLocaleFormat());
      }
      else if(useIsoFormat)
      {
        actor.Perform(EnterTheDateInIsoFormat());
      }
      else
      {
        var idTarget = target as ElementId;
        if(idTarget == null)
        {
          string message = $"In order to use the {nameof(EnterADateBySettingTheValue)} task, the target must be an {nameof(ElementId)}, but got {target.GetType().Name} instead.";
          throw new InvalidTargetException(message);
        }
        actor.Perform(EnterTheDateBySettingTheValue(idTarget));
      }
    }

    IPerformable EnterTheDateInLocaleFormat()
      => new EnterTheDateIntoAnHtml5InputTypeDate(date, target);

    IPerformable EnterTheDateInIsoFormat()
      => new EnterTheDateAsAnIsoFormattedString(date, target);

    IPerformable EnterTheDateBySettingTheValue(ElementId id)
      => new EnterADateBySettingTheValue(date, id);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Tasks.EnterTheDate"/> class.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="target">Target.</param>
    public EnterTheDate(DateTime date, ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.date = date;
      this.target = target;
    }
  }
}
