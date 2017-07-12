using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheOptions
  {
    public static IQuestion<IReadOnlyList<Option>> In(ITarget target)
    {
      return new Questions.GetAllOptions(target);
    }

    public static IQuestion<IReadOnlyList<Option>> In(IWebElement element)
    {
      return new Questions.GetAllOptions(element);
    }

    public static IQuestion<IReadOnlyList<Option>> SelectedIn(ITarget target)
    {
      return new Questions.GetSelectedOptions(target);
    }

    public static IQuestion<IReadOnlyList<Option>> SelectedIn(IWebElement element)
    {
      return new Questions.GetSelectedOptions(element);
    }
  }
}
