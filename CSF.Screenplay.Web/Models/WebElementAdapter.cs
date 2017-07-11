using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Models
{
  public class WebElementAdapter : IWebElementAdapter
  {
    const string
      DisabledAttribute = "disabled",
      VisibleCssProperty = "visibility",
      DisplayCssProperty = "display",
      HiddenVisibility = "hidden",
      NoneDisplay = "none",
      ValueAttribute = "value";

    static readonly ITarget OptionElements = new CssSelector("option", "option elements");

    readonly IWebElement wrappedElement;

    public IWebElement GetSourceElement() => wrappedElement;

    public IReadOnlyList<Option> GetAllOptions()
    {
      return GetOptions();
    }

    public string GetAttributeValue(string attributeName)
    {
      return wrappedElement.GetAttribute(attributeName);
    }

    public string GetCssValue(string propertyName)
    {
      return wrappedElement.GetCssValue(propertyName);
    }

    public Position GetLocation()
    {
      var loc = wrappedElement.Location;
      return new Position(loc.Y, loc.X);
    }

    public IReadOnlyList<Option> GetSelectedOptions()
    {
      return GetOptions(x => x.Selected);
    }

    public Size GetSize()
    {
      var size = wrappedElement.Size;
      return new Size(size.Height, size.Width);
    }

    public string GetText()
    {
      return wrappedElement.Text;
    }

    public string GetValue()
    {
      return GetAttributeValue(ValueAttribute);
    }

    public bool IsActuallyEnabled()
    {
      return wrappedElement.Enabled;
    }

    public bool IsActuallyVisible()
    {
      return wrappedElement.Displayed;
    }

    public bool IsEnabled()
    {
      return GetAttributeValue(DisabledAttribute) == null;
    }

    public bool IsSelected()
    {
      return wrappedElement.Selected;
    }

    public bool IsVisible()
    {
      var visibility = GetCssValue(VisibleCssProperty);
      var display = GetCssValue(DisplayCssProperty);

      return (visibility != HiddenVisibility
              && display != NoneDisplay);
    }

    public IReadOnlyList<IWebElement> Find(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      
      return wrappedElement.FindElements(target.GetWebDriverLocator());
    }

    public IReadOnlyList<IWebElement> Find()
    {
      return Find(CssSelector.AllElements);
    }

    IReadOnlyList<Option> GetOptions(Func<IWebElement,bool> predicate = null)
    {
      var options = Find(OptionElements);

      if(predicate != null)
      {
        options = options.Where(predicate).ToArray();
      }

      return options
        .Select(x => new Option {
          Text = x.Text,
          Value = x.GetAttribute(ValueAttribute)
        })
        .ToArray();
    }

    public WebElementAdapter(IWebElement wrappedElement)
    {
      if(wrappedElement == null)
        throw new ArgumentNullException(nameof(wrappedElement));

      this.wrappedElement = wrappedElement;
    }
  }
}
