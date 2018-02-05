using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds a question in which the actor gets information about the browsing window.
  /// </summary>
  public class TheWindow
  {
    /// <summary>
    /// Gets a question in which the actor reads the window title.
    /// </summary>
    public static IQuestion<string> Title()
    {
      return new Questions.GetWindowTitle();
    }
  }
}
