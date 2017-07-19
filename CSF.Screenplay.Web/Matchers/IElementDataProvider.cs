using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Matchers
{
  public interface IElementDataProvider<TElementData>
  {
    TElementData GetElementData(IWebElement element);
    TElementData GetElementData(IWebElementAdapter adapter);
  }
}
