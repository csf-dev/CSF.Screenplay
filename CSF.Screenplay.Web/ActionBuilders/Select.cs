using System;
using CSF;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.ActionBuilders
{
  public class Select
  {
    readonly int index;
    readonly string val;
    readonly SelectStrategy strategy;

    public IPerformable From(ITarget target)
    {
      switch(strategy)
      {
      case SelectStrategy.Index:
        return new Actions.SelectByIndex(target, index);
      case SelectStrategy.Text:
        return new Actions.SelectByText(target, val);
      case SelectStrategy.Value:
        return new Actions.SelectByValue(target, val);
      default:
        throw new InvalidOperationException("Unexpected selection strategy.");
      }
    }

    public IPerformable From(IWebElement element)
    {
      switch(strategy)
      {
      case SelectStrategy.Index:
        return new Actions.SelectByIndex(element, index);
      case SelectStrategy.Text:
        return new Actions.SelectByText(element, val);
      case SelectStrategy.Value:
        return new Actions.SelectByValue(element, val);
      default:
        throw new InvalidOperationException("Unexpected selection strategy.");
      }
    }

    public static Select ItemNumber(int number)
    {
      return new Select(SelectStrategy.Index, index: number - 1);
    }

    public static Select Item(string text)
    {
      return new Select(SelectStrategy.Text, val: text);
    }

    public static Select ItemValued(string value)
    {
      return new Select(SelectStrategy.Value, val: value);
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
