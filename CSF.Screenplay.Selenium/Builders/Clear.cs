using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Actions;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds an action which clears contents from editable areas of the page.
  /// </summary>
  public class Clear
  {
    /// <summary>
    /// Clears the contents of a user-editable HTML element, such as an input element.
    /// </summary>
    /// <returns>The contents of.</returns>
    /// <param name="target">Target.</param>
    public static IPerformable TheContentsOf(ITarget target)
      => new TargettedAction(target, new ClearTheContents());
  }
}
