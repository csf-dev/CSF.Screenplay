using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class XPath : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.XPath(search);
    }

    public XPath(string selector, string name) : base(selector, name) {}
  }
}
