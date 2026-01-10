using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tasks
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
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();

      if(browseTheWeb.FlagsDriver.HasFlag(Flags.HtmlElements.InputTypeDate.RequiresEntryUsingLocaleFormat))
        actor.Perform(EnterTheDateInLocaleFormat());
      else if(browseTheWeb.FlagsDriver.HasFlag(Flags.HtmlElements.InputTypeDate.RequiresInputViaJavaScriptWorkaround))
        actor.Perform(EnterTheDateViaAJavaScriptWorkaround());
      else
        actor.Perform(EnterTheDateInIsoFormat());
    }

    IPerformable EnterTheDateInLocaleFormat()
      => new EnterTheDateIntoAnHtml5InputTypeDate(date, target);

    IPerformable EnterTheDateInIsoFormat()
      => new EnterTheDateAsAnIsoFormattedString(date, target);

    IPerformable EnterTheDateViaAJavaScriptWorkaround()
      => new SetTheDateUsingAJavaScriptWorkaround(date, target);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Tasks.EnterTheDate"/> class.
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
