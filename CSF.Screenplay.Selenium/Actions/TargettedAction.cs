using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Abilities;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Actions
{
  /// <summary>
  /// Base type for an action/interaction which targets a single web element.
  /// </summary>
  public class TargettedAction : Performable
  {
    readonly ITarget target;
    readonly IActionDriver driver;

    /// <summary>
    /// Performs the action as a given actor.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected override void PerformAs(IPerformer actor)
    {
      var ability = GetAbility(actor);
      var ele = GetWebElement(ability);
      driver.PerformAs(actor, ability, ele);
    }

    /// <summary>
    /// Gets a human-readable report of the action.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    protected override string GetReport(INamed actor)
    {
      return driver.GetReport(actor, GetTargetName());
    }

    /// <summary>
    /// Gets the actor's ability to <see cref="BrowseTheWeb"/>.
    /// </summary>
    /// <returns>The web-browsing ability.</returns>
    /// <param name="actor">Actor.</param>
    protected virtual BrowseTheWeb GetAbility(IPerformer actor)
    {
      return actor.GetAbility<BrowseTheWeb>();
    }

    /// <summary>
    /// Gets the Selenium web element for this action.
    /// </summary>
    /// <returns>The web element.</returns>
    /// <param name="ability">Ability.</param>
    protected virtual IWebElementAdapter GetWebElement(BrowseTheWeb ability)
    {
      return target.GetWebElementAdapter(ability);
    }

    /// <summary>
    /// Gets the name of the current target for this action.
    /// </summary>
    /// <returns>The target name.</returns>
    protected virtual string GetTargetName() => target.GetName();

    TargettedAction(IActionDriver driver)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));
      
      this.driver = driver;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TargettedAction"/> class from a target instance.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="driver">A type which drives the actual action itself.</param>
    public TargettedAction(ITarget target, IActionDriver driver) : this(driver)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));

      this.target = target;
    }
  }
}
