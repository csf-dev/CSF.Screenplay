using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Models
{
  /// <summary>
  /// An adapter/wrapper around an underlying Selenium web element.
  /// </summary>
  public interface IWebElementAdapter : ITarget
  {
    /// <summary>
    /// Gets the underlying Selenium Web Element.
    /// </summary>
    /// <returns>The underlying element.</returns>
    IWebElement GetUnderlyingElement();

    /// <summary>
    /// Gets the value of a named HTML attribute.
    /// </summary>
    /// <returns>The attribute value.</returns>
    /// <param name="attributeName">The attribute name.</param>
    string GetAttributeValue(string attributeName);

    /// <summary>
    /// Gets the value of a named CSS property.
    /// </summary>
    /// <returns>The CSS property value.</returns>
    /// <param name="propertyName">Property name.</param>
    string GetCssValue(string propertyName);

    /// <summary>
    /// Gets a value indicating whether or not the element is explicitly hidden via either of CSS <c>display: none</c>
    /// or <c>visibility: hidden</c>.  This method does not consider other mechanisms by which the element could be
    /// hidden.
    /// </summary>
    /// <returns><c>true</c>, if the element is not explicitly hidden, <c>false</c> otherwise.</returns>
    bool IsNotExplicitlyHidden();

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
    bool IsNotExplicitlyDisabled();

    /// <summary>
    /// Gets a value indicating whether or not the element is visible to a user or not.  This considers more than
    /// <see cref="IsNotExplicitlyHidden"/>, and only indicates visibility if an actual user could see it.
    /// </summary>
    /// <returns><c>true</c>, if the element is visible, <c>false</c> otherwise.</returns>
    bool IsVisible();

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
    bool IsEnabled();

    /// <summary>
    /// Gets a value indicating whether or not the current element (which should be an HTML <c>option</c> element)
    /// is currently selected.
    /// </summary>
    /// <returns><c>true</c>, if the element is selected, <c>false</c> otherwise.</returns>
    bool IsSelected();

    /// <summary>
    /// Gets a collection of all of the HTML <c>option</c> elements within the current element (which should be
    /// an HTML <c>select</c> element).
    /// </summary>
    /// <returns>The options.</returns>
    IReadOnlyList<Option> GetAllOptions();

    /// <summary>
    /// Gets a collection of all of the selected HTML <c>option</c> elements within the current element (which
    /// should be an HTML <c>select</c> element).
    /// </summary>
    /// <returns>The selected options.</returns>
    IReadOnlyList<Option> GetSelectedOptions();

    /// <summary>
    /// Gets the current value of the element.  This is typically used for <c>input</c> or other form-related elements.
    /// </summary>
    /// <returns>The value.</returns>
    string GetValue();

    /// <summary>
    /// Gets the inner text of the element.  This discards any HTML tags from the return value.
    /// </summary>
    /// <returns>The text.</returns>
    string GetText();

    /// <summary>
    /// Gets the location of the element in the browser window.
    /// </summary>
    /// <returns>The location.</returns>
    Position GetLocation();

    /// <summary>
    /// Gets the pixel size of the element.
    /// </summary>
    /// <returns>The size.</returns>
    Size GetSize();

    /// <summary>
    /// Gets a collection of elements, contained within the current element, which match the given target.
    /// </summary>
    /// <param name="target">A target specification.</param>
    IReadOnlyList<IWebElement> Find(ILocatorBasedTarget target);

    /// <summary>
    /// Gets a collection of the elements contained within the current element.
    /// </summary>
    IReadOnlyList<IWebElement> Find();
  }
}
