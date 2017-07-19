using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Matchers
{
  public interface IElementDataProvider<TElementData> : IElementDataProvider
  {
    new TElementData GetElementData(IWebElement element);
    new TElementData GetElementData(IWebElementAdapter adapter);
  }
}
