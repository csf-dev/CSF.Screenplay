using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheCss
  {
    readonly string property;

    public IQuestion<string> From(ITarget target)
    {
      return Questions.Question.Create(target, new CssQuery(property));
    }

    public IQuestion<string> From(IWebElement element)
    {
      return Questions.Question.Create(element, new CssQuery(property));
    }

    public IQuestion<IReadOnlyList<string>> FromAllOf(ITarget target)
    {
      return Questions.Question.CreateMulti(target, new CssQuery(property));
    }

    public IQuestion<IReadOnlyList<string>> From(IReadOnlyList<IWebElement> elements)
    {
      return Questions.Question.CreateMulti(elements, new CssQuery(property));
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
