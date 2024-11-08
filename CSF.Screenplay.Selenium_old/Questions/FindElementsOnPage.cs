using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.ElementMatching;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Questions
{
  /// <summary>
  /// Finds all of the elements which match a set of criteria, from the entire current page.
  /// </summary>
  public class FindElementsOnPage : Performable<ElementCollection>, IQuestion<ElementCollection>
  {
    static readonly ITarget thePage;
    readonly IPerformable<ElementCollection> findWithinBody;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
    {
      return findWithinBody.GetReport(actor);
    }

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override ElementCollection PerformAs(IPerformer actor)
    {
      return findWithinBody.PerformAs(actor);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FindElementsOnPage"/> class.
    /// </summary>
    /// <param name="target">An optional target which the found elements must match.</param>
    /// <param name="matcher">An optional matcher providing criteria for the matched elements.</param>
    /// <param name="elementGroupName">An optional logical/human-readable name for the matched elements.</param>
    public FindElementsOnPage(ILocatorBasedTarget target = null, IMatcher matcher = null, string elementGroupName = null)
    {
      findWithinBody = new FindElements(thePage, target, matcher, elementGroupName);
    }

    /// <summary>
    /// Initializes the <see cref="FindElementsOnPage"/> class.
    /// </summary>
    static FindElementsOnPage()
    {
      thePage = new CssSelector("body", "the page");
    }
  }
}
