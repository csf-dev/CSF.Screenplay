using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheLocation
  {
    public static IQuestion<Position> Of(ITarget target)
    {
      return Questions.Question.Create(target, new LocationQuery());
    }

    public static IQuestion<Position> Of(IWebElement element)
    {
      return Questions.Question.Create(element, new LocationQuery());
    }

    public static IQuestion<IReadOnlyList<Position>> OfAll(ITarget target)
    {
      return Questions.Question.CreateMulti(target, new LocationQuery());
    }

    public static IQuestion<IReadOnlyList<Position>> Of(IReadOnlyList<IWebElement> elements)
    {
      return Questions.Question.CreateMulti(elements, new LocationQuery());
    }
  }
}
