using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheSize
  {
    public static IQuestion<Size> Of(ITarget target)
    {
      return Questions.Question.Create(target, new SizeQuery());
    }

    public static IQuestion<Size> Of(IWebElement element)
    {
      return Questions.Question.Create(element, new SizeQuery());
    }

    public static IQuestion<IReadOnlyList<Size>> OfAll(ITarget target)
    {
      return Questions.Question.CreateMulti(target, new SizeQuery());
    }

    public static IQuestion<IReadOnlyList<Size>> Of(IReadOnlyList<IWebElement> elements)
    {
      return Questions.Question.CreateMulti(elements, new SizeQuery());
    }
  }
}
