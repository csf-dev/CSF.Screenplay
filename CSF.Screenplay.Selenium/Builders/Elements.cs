using System;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds a question which gets a collection of elements which match some criteria.
  /// </summary>
  public class Elements
  {
    /// <summary>
    /// Gets a builder instance for elements which are anywhere on the current page.
    /// </summary>
    /// <returns>The builder instance.</returns>
    public static ElementsInThePageBody InThePageBody()
    {
      return new ElementsInThePageBody();
    }

    /// <summary>
    /// Gets a builder instance for elements which are children of the given target.
    /// </summary>
    /// <returns>The builder instance.</returns>
    /// <param name="target">Target.</param>
    public static ElementsWithin In(ITarget target)
    {
      return new ElementsWithin(target);
    }
  }
}
