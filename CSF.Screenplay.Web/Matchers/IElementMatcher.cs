using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Matchers
{
  public interface IElementMatcher
  {
    bool IsMatch(IWebElement element);

    bool IsMatch(IWebElementAdapter adapter);

    string GetDescription();
  }
}
