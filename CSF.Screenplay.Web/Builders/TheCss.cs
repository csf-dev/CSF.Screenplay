using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheCss
  {
    readonly string property;

    public IQuestion<string> From(ITarget target)
    {
      return new Questions.GetCssValue(target, property);
    }

    public IQuestion<string> From(IWebElement element)
    {
      return new Questions.GetCssValue(element, property);
    }

    public static TheCss Property(string name)
    {
      return new TheCss(name);
    }

    TheCss(string property)
    {
      if(property == null)
        throw new ArgumentNullException(nameof(property));

      this.property = property;
    }
  }
}
