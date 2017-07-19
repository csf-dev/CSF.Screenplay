using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Matchers
{
  public interface IElementDataProvider
  {
    object GetElementData(IWebElement element);
    object GetElementData(IWebElementAdapter adapter);
  }
}
