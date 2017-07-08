using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class ClassNameTarget : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.ClassName(search);
    }

    public ClassNameTarget(string selector, string name) : base(selector, name) {}
  }
}
