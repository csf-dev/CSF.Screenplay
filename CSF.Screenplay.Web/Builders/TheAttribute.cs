using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheAttribute
  {
    readonly string name;

    public IQuestion<string> From(ITarget target)
    {
      return Question.Create(target, new AttributeQuery(name));
    }

    public IQuestion<string> From(IWebElement element)
    {
      return Question.Create(element, new AttributeQuery(name));
    }

    public IQuestion<IReadOnlyList<string>> FromAllOf(ITarget target)
    {
      return Question.CreateMulti(target, new AttributeQuery(name));
    }

    public IQuestion<IReadOnlyList<string>> From(IReadOnlyList<IWebElement> elements)
    {
      return Question.CreateMulti(elements, new AttributeQuery(name));
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
