using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheAttribute
  {
    readonly string name;

    public IQuestion<string> From(ITarget target)
    {
      return new Questions.GetAttribute(target, name);
    }

    public IQuestion<string> From(IWebElement element)
    {
      return new Questions.GetAttribute(element, name);
    }

    public static TheAttribute Named(string name)
    {
      return new TheAttribute(name);
    }

    TheAttribute(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }
  }
}
