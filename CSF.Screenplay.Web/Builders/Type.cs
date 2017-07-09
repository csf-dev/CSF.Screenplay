using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  public class Type
  {
    readonly string val;

    public IPerformable Into(ITarget target)
    {
      return new Actions.Type(target, val);
    }

    public IPerformable Into(IWebElement element)
    {
      return new Actions.Type(element, val);
    }

    public static Type TheText(string val)
    {
      return new Type(val);
    }

    Type(string val)
    {
      if(val == null)
        throw new ArgumentNullException(nameof(val));
      
      this.val = val;
    }
  }
}
