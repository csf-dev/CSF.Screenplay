using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class Click
  {
    public static IPerformable On(ITarget target)
    {
      return new Actions.TargettedAction(target, new Actions.Click());
    }

    public static IPerformable On(IWebElement element)
    {
      return new Actions.TargettedAction(element, new Actions.Click());
    }
  }
}
