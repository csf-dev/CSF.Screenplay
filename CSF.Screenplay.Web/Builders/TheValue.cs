using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheValue
  {
    readonly ITarget target;
    readonly IWebElement element;

    public static IPerformable<string> Of(ITarget target)
    {
      return new Questions.GetValue(target);
    }

    public static IPerformable<string> Of(IWebElement element)
    {
      return new Questions.GetValue(element);
    }

    public static TheValue From(ITarget target)
    {
      return new TheValue(target);
    }

    public static TheValue From(IWebElement element)
    {
      return new TheValue(element);
    }

    public IPerformable<T> As<T>()
    {
      if(target != null)
        return new Questions.GetConvertedValue<T>(target);

      return new Questions.GetConvertedValue<T>(element);
    }

    TheValue(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      this.target = target;
    }

    TheValue(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));
      this.element = element;
    }
  }
}
