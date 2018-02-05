using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds an action representing an actor entering text into a page element.
  /// </summary>
  public class Enter
  {
    readonly string val;
    readonly DateTime? date;

    /// <summary>
    /// The actor enters the text into a given <see cref="ITarget"/>.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="target">Target.</param>
    public IPerformable Into(ITarget target)
    {
      if(date.HasValue)
        return new Tasks.EnterTheDate(date.Value, target);

      return new Actions.TargettedAction(target, new Actions.Enter(val));
    }

    /// <summary>
    /// Indicates the text that the actor is to enter.
    /// </summary>
    /// <returns>A builder instance accepting further configuration.</returns>
    /// <param name="val">The text to be entered.</param>
    public static Enter TheText(string val)
    {
      return new Enter(val);
    }

    /// <summary>
    /// Indicates a date that the actor is to enter.
    /// </summary>
    /// <returns>A builder instance accepting further configuration.</returns>
    /// <param name="date">The date to be entered.</param>
    public static Enter TheDate(DateTime date)
    {
      return new Enter(date);
    }

    Enter(string val)
    {
      if(val == null)
        throw new ArgumentNullException(nameof(val));
      
      this.val = val;
    }

    Enter(DateTime date)
    {
      this.date = date;
    }
  }
}
