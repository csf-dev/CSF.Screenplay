using System;
using CSF;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class Deselect
  {
    readonly int index;
    readonly string val;
    readonly SelectStrategy strategy;

    public IPerformable From(ITarget target)
    {
      return new Actions.TargettedAction(target, GetActionDriver());
    }

    public IPerformable From(IWebElement element)
    {
      return new Actions.TargettedAction(element, GetActionDriver());
    }

    public static IPerformable EverythingFrom(ITarget target)
    {
      return new Actions.TargettedAction(target, new Actions.DeselectAll());
    }

    public static IPerformable EverythingFrom(IWebElement element)
    {
      return new Actions.TargettedAction(element, new Actions.DeselectAll());
    }

    public static Deselect ItemNumber(int number)
    {
      return new Deselect(SelectStrategy.Index, index: number - 1);
    }

    public static Deselect Item(string text)
    {
      return new Deselect(SelectStrategy.Text, val: text);
    }

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
