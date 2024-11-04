using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Selenium.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// Implementation of <see cref="IWebElementAdapter"/> for a Selenium web element.
  /// </summary>
  public class SeleniumWebElementAdapter : IWebElementAdapter
  {
    const string
      DisabledAttribute = "disabled",
      VisibleCssProperty = "visibility",
      DisplayCssProperty = "display",
      HiddenVisibility = "hidden",
      NoneDisplay = "none",
      ValueAttribute = "value";

    static readonly ILocatorBasedTarget OptionElements = new CssSelector("option", "option elements");

    readonly IWebElement wrappedElement;
    readonly string name;

    /// <summary>
    /// Gets a reference to the source <c>IWebElement</c> instance.
    /// </summary>
    /// <returns>The source element.</returns>
    public IWebElement GetSourceElement() => wrappedElement;

    /// <summary>
    /// Gets a collection of all of the HTML <c>option</c> elements within the current element (which should be
    /// an HTML <c>select</c> element).
    /// </summary>
    /// <returns>The options.</returns>
    public IReadOnlyList<Option> GetAllOptions()
    {
      return GetOptions();
    }

    /// <summary>
    /// Gets the value of a named HTML attribute.
    /// </summary>
    /// <returns>The attribute value.</returns>
    /// <param name="attributeName">The attribute name.</param>
    public string GetAttributeValue(string attributeName)
    {
      return wrappedElement.GetAttribute(attributeName);
    }

    /// <summary>
    /// Gets the value of a named CSS property.
    /// </summary>
    /// <returns>The CSS property value.</returns>
    /// <param name="propertyName">Property name.</param>
    public string GetCssValue(string propertyName)
    {
      return wrappedElement.GetCssValue(propertyName);
    }

    /// <summary>
    /// Gets the location of the element in the browser window.
    /// </summary>
    /// <returns>The location.</returns>
    public Position GetLocation()
    {
      var loc = wrappedElement.Location;
      return new Position(loc.Y, loc.X);
    }

    /// <summary>
    /// Gets a collection of all of the selected HTML <c>option</c> elements within the current element (which
    /// should be an HTML <c>select</c> element).
    /// </summary>
    /// <returns>The selected options.</returns>
    public IReadOnlyList<Option> GetSelectedOptions()
    {
      return GetOptions(x => x.Selected);
    }

    /// <summary>
    /// Gets the pixel size of the element.
    /// </summary>
    /// <returns>The size.</returns>
    public Size GetSize()
    {
      var size = wrappedElement.Size;
      return new Size(size.Height, size.Width);
    }

    /// <summary>
    /// Gets the inner text of the element.  This discards any HTML tags from the return value.
    /// </summary>
    /// <returns>The text.</returns>
    public string GetText()
    {
      return wrappedElement.Text;
    }

    /// <summary>
    /// Gets the current value of the element.  This is typically used for <c>input</c> or other form-related elements.
    /// </summary>
    /// <returns>The value.</returns>
    public string GetValue()
    {
      return GetAttributeValue(ValueAttribute);
    }

    /// <summary>
    /// Gets a value indicating whether the element is actually enabled for user interaction.
    /// This differs from <see cref="IsNotExplicitlyDisabled"/> in that it fully resolves the 'actual' enabled state
    /// as a user would see it, instead of just checking the one attribute.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Imagine a <c>fieldset</c> element containing some <c>input</c> elements.  If the fieldset has the
    /// <c>disabled</c> attribute, then all of its child input elements are also disabled, even though they do
    /// not carry the disabled attribute themselves.
    /// </para>
    /// <para>
    /// This method would return <c>false</c> for those input elements.
    /// </para>
    /// </remarks>
    /// <returns><c>true</c>, if the element is enabled, <c>false</c> otherwise.</returns>
    public bool IsEnabled()
    {
      return wrappedElement.Enabled;
    }

    /// <summary>
    /// Gets a value indicating whether or not the element is visible to a user or not.  This considers more than
    /// <see cref="IsNotExplicitlyHidden"/>, and only indicates visibility if an actual user could see it.
    /// </summary>
    /// <returns><c>true</c>, if the element is visible, <c>false</c> otherwise.</returns>
    public bool IsVisible()
    {
      return wrappedElement.Displayed;
    }

    /// <summary>
    /// Gets a value indicating whether or not the <c>disabled</c> attribute is present on this element.
    /// Note that this differs from <see cref="IsEnabled"/>, because even though this returns <c>true</c>, the element
    /// might still not be enabled.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method does not check for the disabled/enabled state of parent elements.  Imagine a <c>fieldset</c>
    /// element containing some <c>input</c> elements.  If the fieldset has the <c>disabled</c> attribute, then all
    /// of its child input elements are also disabled, even though they do not carry the disabled attribute
    /// themselves.
    /// </para>
    /// <para>
    /// This method would return <c>true</c> for those input elements even thoguh they are disabled from the users
    /// perspective.
    /// </para>
    /// </remarks>
    /// <returns><c>true</c>, if is the <c>disabled</c> attribute is not present, <c>false</c> if it is.</returns>
    public bool IsNotExplicitlyDisabled()
    {
      return GetAttributeValue(DisabledAttribute) == null;
    }

    /// <summary>
    /// Gets a value indicating whether or not the current element (which should be an HTML <c>option</c> element)
    /// is currently selected.
    /// </summary>
    /// <returns><c>true</c>, if the element is selected, <c>false</c> otherwise.</returns>
    public bool IsSelected()
    {
      return wrappedElement.Selected;
    }

    /// <summary>
    /// Gets a value indicating whether or not the element is explicitly hidden via either of CSS <c>display: none</c>
    /// or <c>visibility: hidden</c>.  This method does not consider other mechanisms by which the element could be
    /// hidden.
    /// </summary>
    /// <returns><c>true</c>, if the element is not explicitly hidden, <c>false</c> otherwise.</returns>
    public bool IsNotExplicitlyHidden()
    {
      var visibility = GetCssValue(VisibleCssProperty);
      var display = GetCssValue(DisplayCssProperty);

      return (visibility != HiddenVisibility
              && display != NoneDisplay);
    }

    /// <summary>
    /// Gets a collection of elements, contained within the current element, which match the given target.
    /// </summary>
    /// <param name="target">A target specification.</param>
    public IReadOnlyList<IWebElement> Find(ILocatorBasedTarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      
      return wrappedElement.FindElements(target.GetWebDriverLocator());
    }

    /// <summary>
    /// Gets a collection of the elements contained within the current element.
    /// </summary>
    public IReadOnlyList<IWebElement> Find()
    {
      return Find(CssSelector.AllElements);
    }

    /// <summary>
    /// Gets a web element adapter from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="driver">The web driver.</param>
    public IWebElementAdapter GetWebElementAdapter(IWebDriver driver) => this;

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using a given Selenium web driver.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="driver">The web driver.</param>
    public ElementCollection GetWebElementAdapters(IWebDriver driver)
      => new ElementCollection(new [] { this }, name);

    /// <summary>
    /// Gets the underlying Selenium Web Element.
    /// </summary>
    /// <returns>The underlying element.</returns>
    public IWebElement GetUnderlyingElement() => wrappedElement;

    /// <summary>
    /// Gets a web element adapter from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapter.</returns>
    /// <param name="ability">Ability.</param>
    public IWebElementAdapter GetWebElementAdapter(BrowseTheWeb ability) => this;

    /// <summary>
    /// Gets a collection of web element adapters from the current instance, using the given web-browsing ability.
    /// </summary>
    /// <returns>The web element adapters.</returns>
    /// <param name="ability">Ability.</param>
    public ElementCollection GetWebElementAdapters(BrowseTheWeb ability)
      =>  new ElementCollection(new [] { this }, name);

    /// <summary>
    /// Gets the human-readable target name.
    /// </summary>
    /// <returns>The name.</returns>
    public string GetName() => name ?? $"An HTML element";

    IReadOnlyList<Option> GetOptions(Func<IWebElement,bool> predicate = null)
    {
      var options = Find(OptionElements);

      if(predicate != null)
      {
        options = options.Where(predicate).ToArray();
      }

      return options
        .Select(x => new Option(x.Text, x.GetAttribute(ValueAttribute)))
        .ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SeleniumWebElementAdapter"/> class.
    /// </summary>
    /// <param name="wrappedElement">Wrapped element.</param>
    /// <param name="name">A name for the wrapped element.</param>
    public SeleniumWebElementAdapter(IWebElement wrappedElement, string name = null)
    {
      if(wrappedElement == null)
        throw new ArgumentNullException(nameof(wrappedElement));

      this.wrappedElement = wrappedElement;
      this.name = name;
    }
  }
}
