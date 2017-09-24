using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Builders;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tasks
{
  /// <summary>
  /// A task which enters a date as a plain ISO-formatted <c>yyyy-MM-dd</c> string into a plain
  /// text control.  This may be used as a fallback for web browsers which do not fully support HTML 5 input
  /// type="date" elements.
  /// </summary>
  public class EnterTheDateAsAnIsoFormattedString : Performable
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
      actor.Perform(Clear.TheContentsOf(target));
      actor.Perform(Enter.TheText(date.ToString("yyyy-MM-dd")).Into(target));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Tasks.EnterTheDateAsAnIsoFormattedString"/> class.
    /// </summary>
    /// <param name="date">Date.</param>
    /// <param name="target">Target.</param>
    public EnterTheDateAsAnIsoFormattedString(DateTime date, ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.date = date;
      this.target = target;
    }
  }
}
