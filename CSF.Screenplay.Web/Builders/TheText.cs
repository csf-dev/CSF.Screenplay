using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheText
  {
    readonly ITarget target;
    readonly IWebElement element;

    public static IPerformable<string> Of(ITarget target)
    {
      return new Questions.GetText(target);
    }

    public static IPerformable<string> Of(IWebElement element)
    {
      return new Questions.GetText(element);
    }

    public static TheText From(ITarget target)
    {
      return new TheText(target);
    }

    public static TheText From(IWebElement element)
    {
      return new TheText(element);
    }

    public IPerformable<T> As<T>()
    {
      if(target != null)
        return new Questions.GetConvertedText<T>(target);

      return new Questions.GetConvertedText<T>(element);
    }

    TheText(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      this.target = target;
    }

    TheText(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));
      this.element = element;
    }
  }
}
