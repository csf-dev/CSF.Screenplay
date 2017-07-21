using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheText
  {
    readonly ITarget target;
    readonly IWebElement element;

    public static Performables.IQuestion<string> Of(ITarget target)
    {
      return Question.Create(target, new TextQuery());
    }

    public static Performables.IQuestion<string> Of(IWebElement element)
    {
      return Question.Create(element, new TextQuery());
    }

    public static Performables.IQuestion<IReadOnlyList<string>> OfAll(ITarget target)
    {
      return Question.CreateMulti(target, new TextQuery());
    }

    public static Performables.IQuestion<IReadOnlyList<string>> Of(IReadOnlyList<IWebElement> elements)
    {
      return Question.CreateMulti(elements, new TextQuery());
    }

    public static TheText From(ITarget target)
    {
      return new TheText(target);
    }

    public static TheText From(IWebElement element)
    {
      return new TheText(element);
    }

    public Performables.IQuestion<T> As<T>()
    {
      if(target != null)
        return Question.Create(target, new TextQuery<T>());

      return Question.Create(element, new TextQuery<T>());
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
