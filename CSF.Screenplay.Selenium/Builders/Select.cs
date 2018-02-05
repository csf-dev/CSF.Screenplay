using System;
using CSF;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds an action representing an actor selecting options from an HTML <c>select</c> element.
  /// </summary>
  public class Select
  {
    readonly int index;
    readonly string val;
    readonly SelectStrategy strategy;

    /// <summary>
    /// Performs the selection using a given target.
    /// </summary>
    /// <returns>A performable action instance.</returns>
    /// <param name="target">Target.</param>
    public IPerformable From(ITarget target)
    {
      return new Actions.TargettedAction(target, GetActionDriver());
    }

    /// <summary>
    /// Selects the numbered item (the first item is item number 1).
    /// </summary>
    /// <returns>A select builder instance accepting further configuration.</returns>
    /// <param name="number">The item number (starting at 1).</param>
    public static Select ItemNumber(int number)
    {
      return new Select(SelectStrategy.Index, index: number - 1);
    }

    /// <summary>
    /// Selects an item by its displayed human-readable text.
    /// </summary>
    /// <returns>A select builder instance accepting further configuration.</returns>
    /// <param name="text">The text of the item to select.</param>
    public static Select Item(string text)
    {
      return new Select(SelectStrategy.Text, val: text);
    }

    /// <summary>
    /// Selects an item by its underlying value.
    /// </summary>
    /// <returns>A select builder instance accepting further configuration.</returns>
    /// <param name="value">The value of the item to select.</param>
    public static Select ItemValued(string value)
    {
      return new Select(SelectStrategy.Value, val: value);
    }

    Actions.SelectActionDriver GetActionDriver()
    {
      switch(strategy)
      {
        case SelectStrategy.Index:
          return new Actions.SelectByIndex(index);
        case SelectStrategy.Text:
          return new Actions.SelectByText(val);
        case SelectStrategy.Value:
          return new Actions.SelectByValue(val);
        default:
          throw new InvalidOperationException("Unexpected selection strategy.");
      }
    }

    Select(SelectStrategy strategy, int index = 0, string val = null)
    {
      strategy.RequireDefinedValue(nameof(strategy));

      this.strategy = strategy;
      this.index = index;
      this.val = val;
    }
  }
}
