using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class CssSelector : StringBasedTarget
  {
    static readonly CssSelector all;

    public static CssSelector AllElements => all;

    protected override By GetWebDriverLocator(string search)
    {
      return By.CssSelector(search);
    }

    public CssSelector(string selector, string name) : base(selector, name) {}

    static CssSelector()
    {
      all = new CssSelector("*", "everything");
    }
  }
}
