using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheOptions
  {
    public static IPerformable<IReadOnlyList<Option>> In(ITarget target)
    {
      return new Questions.GetAllOptions(target);
    }

    public static IPerformable<IReadOnlyList<Option>> In(IWebElement element)
    {
      return new Questions.GetAllOptions(element);
    }
  }
}
