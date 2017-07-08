using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class IdTarget : StringBasedTarget
  {
    protected override By GetWebDriverLocator(string search)
    {
      return By.Id(search);
    }

    public IdTarget(string selector, string name) : base(selector, name) {}
  }
}
