using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using CSF.Screenplay.Web.Questions;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheOptions
  {
    public static IQuestion<IReadOnlyList<Option>> In(ITarget target)
    {
      return Question.Create(target, new OptionsQuery());
    }

    public static IQuestion<IReadOnlyList<Option>> In(IWebElement element)
    {
      return Question.Create(element, new OptionsQuery());
    }

    public static IQuestion<IReadOnlyList<Option>> SelectedIn(ITarget target)
    {
      return Question.Create(target, new SelectedOptionsQuery());
    }

    public static IQuestion<IReadOnlyList<Option>> SelectedIn(IWebElement element)
    {
      return Question.Create(element, new SelectedOptionsQuery());
    }

    public static IQuestion<IReadOnlyList<IReadOnlyList<Option>>> InAllOf(ITarget target)
    {
      return Question.CreateMulti(target, new OptionsQuery());
    }

    public static IQuestion<IReadOnlyList<IReadOnlyList<Option>>> In(IReadOnlyList<IWebElement> elements)
    {
      return Question.CreateMulti(elements, new OptionsQuery());
    }

    public static IQuestion<IReadOnlyList<IReadOnlyList<Option>>> SelectedInAllOf(ITarget target)
    {
      return Question.CreateMulti(target, new SelectedOptionsQuery());
    }

    public static IQuestion<IReadOnlyList<IReadOnlyList<Option>>> SelectedIn(IReadOnlyList<IWebElement> elements)
    {
      return Question.CreateMulti(elements, new SelectedOptionsQuery());
    }
  }
}
