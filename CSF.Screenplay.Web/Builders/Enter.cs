using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class Enter
  {
    readonly string val;

    public IPerformable Into(ITarget target)
    {
      return new Actions.TargettedAction(target, new Actions.Enter(val));
    }

    public IPerformable Into(IWebElement element)
    {
      return new Actions.TargettedAction(element, new Actions.Enter(val));
    }

    public static Enter TheText(string val)
    {
      return new Enter(val);
    }

    Enter(string val)
    {
      if(val == null)
        throw new ArgumentNullException(nameof(val));
      
      this.val = val;
    }
  }
}
