using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class TheSize
  {
    public static IQuestion<Size> Of(ITarget target)
    {
      return new Questions.GetSize(target);
    }

    public static IQuestion<Size> Of(IWebElement element)
    {
      return new Questions.GetSize(element);
    }
  }
}
