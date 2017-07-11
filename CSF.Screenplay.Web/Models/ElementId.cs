using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class ElementId : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.Id(search);
    }

    public ElementId(string selector, string name) : base(selector, name) {}
  }
}
