using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public interface IWebElementAdapter
  {
    IWebElement GetSourceElement();

    string GetAttributeValue(string attributeName);

    string GetCssValue(string propertyName);

    bool IsVisible();

    bool IsEnabled();

    bool IsActuallyVisible();

    bool IsActuallyEnabled();

    bool IsSelected();

    IReadOnlyList<Option> GetAllOptions();

    IReadOnlyList<Option> GetSelectedOptions();

    string GetValue();

    string GetText();

    Position GetLocation();

    Size GetSize();

    IReadOnlyList<IWebElement> Find(ITarget target);

    IReadOnlyList<IWebElement> Find();
  }
}
