using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheLocation
  {
    public static IQuestion<Position> Of(ITarget target)
    {
      return new Questions.GetLocation(target);
    }

    public static IQuestion<Position> Of(IWebElement element)
    {
      return new Questions.GetLocation(element);
    }
  }
}
