using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Tasks;

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
    /// <returns>A performable action.</returns>
    /// <param name="target">The target from which to clear the value.</param>
    public static IPerformable TheContentsOf(ITarget target)
      => new TargettedAction(target, new ClearTheContents());

    /// <summary>
    /// Clears the contents of an HTML <c>&lt;input type="date"&gt;</c> element.
    /// </summary>
    /// <returns>A performable action.</returns>
    /// <param name="target">The target from which to clear the date.</param>
    public static IPerformable TheDateFrom(ITarget target)
      => new ClearTheDate(target);
  }
}
