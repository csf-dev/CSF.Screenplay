using System;
using CSF;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.ActionBuilders
{
  public class Deselect
  {
    readonly int index;
    readonly string val;
    readonly SelectStrategy strategy;

    public IPerformable From(ITarget target)
    {
      switch(strategy)
      {
      case SelectStrategy.Index:
        return new Actions.DeselectByIndex(target, index);
      case SelectStrategy.Text:
        return new Actions.DeselectByText(target, val);
      case SelectStrategy.Value:
        return new Actions.DeselectByValue(target, val);
      default:
        throw new InvalidOperationException("Unexpected selection strategy.");
      }
    }

    public IPerformable From(IWebElement element)
    {
      switch(strategy)
      {
      case SelectStrategy.Index:
        return new Actions.DeselectByIndex(element, index);
      case SelectStrategy.Text:
        return new Actions.DeselectByText(element, val);
      case SelectStrategy.Value:
        return new Actions.DeselectByValue(element, val);
      default:
        throw new InvalidOperationException("Unexpected selection strategy.");
      }
    }

    public static IPerformable EverythingFrom(ITarget target)
    {
      return new Actions.DeselectAll(target);
    }

    public static IPerformable EverythingFrom(IWebElement element)
    {
      return new Actions.DeselectAll(element);
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

    Deselect(SelectStrategy strategy, int index = 0, string val = null)
    {
      strategy.RequireDefinedValue(nameof(strategy));

      this.strategy = strategy;
      this.index = index;
      this.val = val;
    }
  }
}
