using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheSelectedOptions
  {
    public static IPerformable<IReadOnlyList<Option>> In(ITarget target)
    {
      return new Questions.GetSelectedOptions(target);
    }

    public static IPerformable<IReadOnlyList<Option>> In(IWebElement element)
    {
      return new Questions.GetSelectedOptions(element);
    }
  }
}
