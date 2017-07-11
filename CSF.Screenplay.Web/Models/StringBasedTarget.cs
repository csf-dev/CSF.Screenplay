using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public abstract class StringBasedTarget : ITarget
  {
    readonly string searchString, name;

    string IHasTargetName.GetName()
    {
      return name;
    }

    By ITarget.GetWebDriverLocator()
    {
      return GetWebDriverLocator(searchString);
    }

    protected abstract By GetWebDriverLocator(string search);

    public StringBasedTarget(string searchString, string name)
    {
      if(searchString == null)
        throw new ArgumentNullException(nameof(searchString));
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.searchString = searchString;
      this.name = name;
    }
  }
}
