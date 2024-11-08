using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Questions;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds actions relating to getting HTML elements from the document.
  /// </summary>
  public class Get
  {
    /// <summary>
    /// Gets a reference to a single HTML element.
    /// </summary>
    /// <returns>The element.</returns>
    /// <param name="target">A target which identifies the element.</param>
    public static IQuestion<IWebElementAdapter> TheElement(ILocatorBasedTarget target)
      => new GetElement(target);
  }
}
