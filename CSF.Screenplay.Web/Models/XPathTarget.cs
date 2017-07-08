using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class XPathTarget : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.XPath(search);
    }

    public XPathTarget(string selector, string name) : base(selector, name) {}
  }
}
