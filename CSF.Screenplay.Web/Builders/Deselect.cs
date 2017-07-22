using System;
using CSF;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds an action representing an actor deselecting options from an HTML <c>select</c> element.
  /// </summary>
  public class Deselect
  {
    readonly int index;
    readonly string val;
    readonly SelectStrategy strategy;

    /// <summary>
    /// Performs the deselection using a given target.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="target">Target.</param>
    public IPerformable From(ITarget target)
    {
      return new Actions.TargettedAction(target, GetActionDriver());
    }

    /// <summary>
    /// Performs the deselection using a given web element.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="element">Element.</param>
    public IPerformable From(IWebElement element)
    {
      return new Actions.TargettedAction(element, GetActionDriver());
    }

    /// <summary>
    /// Deselects all of the options from the given target.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="target">Target.</param>
    public static IPerformable EverythingFrom(ITarget target)
    {
      return new Actions.TargettedAction(target, new Actions.DeselectAll());
    }

    /// <summary>
    /// Deselects all of the options from the given web element.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="element">Element.</param>
    public static IPerformable EverythingFrom(IWebElement element)
    {
      return new Actions.TargettedAction(element, new Actions.DeselectAll());
    }

    /// <summary>
    /// Deselects the numbered item (the first item is item number 1).
    /// </summary>
    /// <returns>A deselect builder instance accepting further configuration.</returns>
    /// <param name="number">The item number (starting at 1).</param>
    public static Deselect ItemNumber(int number)
    {
      return new Deselect(SelectStrategy.Index, index: number - 1);
    }

    /// <summary>
    /// Deselects the named item (based on its displayed text).
    /// </summary>
    /// <returns>A deselect builder instance accepting further configuration.</returns>
    /// <param name="text">The text of the item to deselect.</param>
    public static Deselect Item(string text)
    {
      return new Deselect(SelectStrategy.Text, val: text);
    }

    /// <summary>
    /// Deselects the named item (based on its underlying value).
    /// </summary>
    /// <returns>A deselect builder instance accepting further configuration.</returns>
    /// <param name="value">The value of the item to deselect.</param>
    public static Deselect ItemValued(string value)
    {
      return new Deselect(SelectStrategy.Value, val: value);
    }

    Actions.SelectActionDriver GetActionDriver()
    {
      switch(strategy)
      {
        case SelectStrategy.Index:
          return new Actions.DeselectByIndex(index);
        case SelectStrategy.Text:
          return new Actions.DeselectByText(val);
        case SelectStrategy.Value:
          return new Actions.DeselectByValue(val);
        default:
          throw new InvalidOperationException("Unexpected selection strategy.");
      }
    }

    Deselect(SelectStrategy strategy, int index = 0, string val = null)
    {
      strategy.RequireDefinedValue(nameof(strategy));

      this.strategy = strategy;
      this.index = index;
      this.val = val;
    }
  }
}
