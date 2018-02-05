using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Builders
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
