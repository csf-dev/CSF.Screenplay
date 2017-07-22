using System;
using CSF;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class Select
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
