using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class ClassName : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.ClassName(search);
    }

    public ClassName(string selector, string name) : base(selector, name) {}
  }
}
