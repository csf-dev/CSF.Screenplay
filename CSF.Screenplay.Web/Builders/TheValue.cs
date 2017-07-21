using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheValue
  {
    readonly ITarget target;
    readonly IWebElement element;

    public static Performables.IQuestion<string> Of(ITarget target)
    {
      return Question.Create(target, new ValueQuery());
    }

    public static Performables.IQuestion<string> Of(IWebElement element)
    {
      return Question.Create(element, new ValueQuery());
    }

    public static Performables.IQuestion<IReadOnlyList<string>> OfAll(ITarget target)
    {
      return Question.CreateMulti(target, new ValueQuery());
    }

    public static Performables.IQuestion<IReadOnlyList<string>> Of(IReadOnlyList<IWebElement> elements)
    {
      return Question.CreateMulti(elements, new ValueQuery());
    }

    public static TheValue From(ITarget target)
    {
      return new TheValue(target);
    }

    public static TheValue From(IWebElement element)
    {
      return new TheValue(element);
    }

    public Performables.IQuestion<T> As<T>()
    {
      if(target != null)
        return Question.Create(target, new ValueQuery<T>());

      return Question.Create(element, new ValueQuery<T>());
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
