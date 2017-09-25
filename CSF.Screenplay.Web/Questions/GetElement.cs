using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// An action which gets a reference to a single element in the document.
  /// </summary>
  public class GetElement : Performable<IWebElementAdapter>, IQuestion<IWebElementAdapter>
  {
    readonly ILocatorBasedTarget target;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor) => $"{actor.Name} gets {target.GetName()}";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override IWebElementAdapter PerformAs(IPerformer actor)
    {
      var ability = actor.GetAbility<BrowseTheWeb>();
      return target.GetWebElementAdapter(ability);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FindElementsOnPage"/> class.
    /// </summary>
    /// <param name="target">The element to get.</param>
    public GetElement(ILocatorBasedTarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      
      this.target = target;
    }
  }
}
