using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class CssSelectorTarget : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.CssSelector(search);
    }

    public CssSelectorTarget(string selector, string name) : base(selector, name) {}
  }
}
