﻿//
// SelectElement.cs
//
// Author:
//       Various Selenium authors
//       Screenplay modifications: Craig Fowler <craig@csf-dev.com>
//
//       This file began its life as a copy of the following:
//       https://github.com/SeleniumHQ/selenium/blob/selenium-3.4.0/dotnet/src/support/UI/SelectElement.cs
//
// The original file is copyright 2011-2016 [various authors].
// Further modifications are copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace CSF.Selenium.Support.UI
{
  /// <summary>
  /// Provides a convenience method for manipulating selections of options in an HTML select element.
  /// </summary>
  public class SelectElement : IWrapsElement
  {
    private readonly IWebElement element;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectElement"/> class.
    /// </summary>
    /// <param name="element">The element to be wrapped</param>
    /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
    /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a &lt;select&gt; element.</exception>
    public SelectElement(IWebElement element)
    {
      if (element == null)
      {
        throw new ArgumentNullException("element", "element cannot be null");
      }

      if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "select", StringComparison.OrdinalIgnoreCase) != 0)
      {
        throw new UnexpectedTagNameException("select", element.TagName);
      }

      this.element = element;

      // let check if it's a multiple
      string attribute = element.GetAttribute("multiple");
      this.IsMultiple = attribute != null && attribute.ToLowerInvariant() != "false";
    }

    /// <summary>
    /// Gets the <see cref="IWebElement"/> wrapped by this object.
    /// </summary>
    public IWebElement WrappedElement
    {
      get { return this.element; }
    }

    /// <summary>
    /// Gets a value indicating whether the parent element supports multiple selections.
    /// </summary>
    public bool IsMultiple { get; private set; }

    /// <summary>
    /// Gets the list of options for the select element.
    /// </summary>
    public IList<IWebElement> Options
    {
      get
      {
        return this.element.FindElements(By.TagName("option"));
      }
    }

    /// <summary>
    /// Gets the selected item within the select element.
    /// </summary>
    /// <remarks>If more than one item is selected this will return the first item.</remarks>
    /// <exception cref="NoSuchElementException">Thrown if no option is selected.</exception>
    public IWebElement SelectedOption
    {
      get
      {
        foreach (IWebElement option in this.Options)
        {
          if (option.Selected)
          {
            return option;
          }
        }

        throw new NoSuchElementException("No option is selected");
      }
    }

    /// <summary>
    /// Gets all of the selected options within the select element.
    /// </summary>
    public IList<IWebElement> AllSelectedOptions
    {
      get
      {
        List<IWebElement> returnValue = new List<IWebElement>();
        foreach (IWebElement option in this.Options)
        {
          if (option.Selected)
          {
            returnValue.Add(option);
          }
        }

        return returnValue;
      }
    }

    /// <summary>
    /// Select all options by the text displayed.
    /// </summary>
    /// <param name="text">The text of the option to be selected. If an exact match is not found,
    /// this method will perform a substring match.</param>
    /// <remarks>When given "Bar" this method would select an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;
    /// </para>
    /// </remarks>
    /// <exception cref="NoSuchElementException">Thrown if there is no element with the given text present.</exception>
    public void SelectByText(string text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text", "text must not be null");
      }

      // try to find the option via XPATH ...
      IList<IWebElement> options = this.element.FindElements(By.XPath(".//option[normalize-space(.) = " + EscapeQuotes(text) + "]"));

      bool matched = false;
      foreach (IWebElement option in options)
      {
        SetSelected(option, true);
        if (!this.IsMultiple)
        {
          return;
        }

        matched = true;
      }

      if (options.Count == 0 && text.Contains(" "))
      {
        string substringWithoutSpace = GetLongestSubstringWithoutSpace(text);
        IList<IWebElement> candidates;
        if (string.IsNullOrEmpty(substringWithoutSpace))
        {
          // hmm, text is either empty or contains only spaces - get all options ...
          candidates = this.element.FindElements(By.TagName("option"));
        }
        else
        {
          // get candidates via XPATH ...
          candidates = this.element.FindElements(By.XPath(".//option[contains(., " + EscapeQuotes(substringWithoutSpace) + ")]"));
        }

        foreach (IWebElement option in candidates)
        {
          if (text == option.Text)
          {
            SetSelected(option, true);
            if (!this.IsMultiple)
            {
              return;
            }

            matched = true;
          }
        }
      }

      if (!matched)
      {
        throw new NoSuchElementException("Cannot locate element with text: " + text);
      }
    }

    /// <summary>
    /// Select an option by the value.
    /// </summary>
    /// <param name="value">The value of the option to be selected.</param>
    /// <remarks>When given "foo" this method will select an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;
    /// </para>
    /// </remarks>
    /// <exception cref="NoSuchElementException">Thrown when no element with the specified value is found.</exception>
    public void SelectByValue(string value)
    {
      StringBuilder builder = new StringBuilder(".//option[@value = ");
      builder.Append(EscapeQuotes(value));
      builder.Append("]");
      IList<IWebElement> options = this.element.FindElements(By.XPath(builder.ToString()));

      bool matched = false;
      foreach (IWebElement option in options)
      {
        SetSelected(option, true);
        if (!this.IsMultiple)
        {
          return;
        }

        matched = true;
      }

      if (!matched)
      {
        throw new NoSuchElementException("Cannot locate option with value: " + value);
      }
    }

    /// <summary>
    /// Select the option by the index, as determined by the "index" attribute of the element.
    /// </summary>
    /// <param name="index">The value of the index attribute of the option to be selected.</param>
    /// <exception cref="NoSuchElementException">Thrown when no element exists with the specified index attribute.</exception>
    public void SelectByIndex(int index)
    {
      string match = index.ToString(CultureInfo.InvariantCulture);

      foreach (IWebElement option in this.Options)
      {
        if (option.GetAttribute("index") == match)
        {
          SetSelected(option, true);
          return;
        }
      }

      throw new NoSuchElementException("Cannot locate option with index: " + index);
    }

    /// <summary>
    /// Clear all selected entries. This is only valid when the SELECT supports multiple selections.
    /// </summary>
    /// <exception cref="WebDriverException">Thrown when attempting to deselect all options from a SELECT
    /// that does not support multiple selections.</exception>
    public void DeselectAll()
    {
      if (!this.IsMultiple)
      {
        throw new InvalidOperationException("You may only deselect all options if multi-select is supported");
      }

      foreach (IWebElement option in this.Options)
      {
        SetSelected(option, false);
      }
    }

    /// <summary>
    /// Deselect the option by the text displayed.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to deselect option from a SELECT
    /// that does not support multiple selections.</exception>
    /// <exception cref="NoSuchElementException">Thrown when no element exists with the specified test attribute.</exception>
    /// <param name="text">The text of the option to be deselected.</param>
    /// <remarks>When given "Bar" this method would deselect an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;
    /// </para>
    /// </remarks>
    public void DeselectByText(string text)
    {
      if (!this.IsMultiple)
      {
        throw new InvalidOperationException("You may only deselect option if multi-select is supported");
      }

      bool matched = false;
      StringBuilder builder = new StringBuilder(".//option[normalize-space(.) = ");
      builder.Append(EscapeQuotes(text));
      builder.Append("]");
      IList<IWebElement> options = this.element.FindElements(By.XPath(builder.ToString()));
      foreach (IWebElement option in options)
      {
        SetSelected(option, false);
        matched = true;
      }

      if (!matched)
      {
        throw new NoSuchElementException("Cannot locate option with text: " + text);
      }
    }

    /// <summary>
    /// Deselect the option having value matching the specified text.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to deselect option from a SELECT
    /// that does not support multiple selections.</exception>
    /// <exception cref="NoSuchElementException">Thrown when no element exists with the specified value attribute.</exception>
    /// <param name="value">The value of the option to deselect.</param>
    /// <remarks>When given "foo" this method will deselect an option like:
    /// <para>
    /// &lt;option value="foo"&gt;Bar&lt;/option&gt;
    /// </para>
    /// </remarks>
    public void DeselectByValue(string value)
    {
      if (!this.IsMultiple)
      {
        throw new InvalidOperationException("You may only deselect option if multi-select is supported");
      }

      bool matched = false;
      StringBuilder builder = new StringBuilder(".//option[@value = ");
      builder.Append(EscapeQuotes(value));
      builder.Append("]");
      IList<IWebElement> options = this.element.FindElements(By.XPath(builder.ToString()));
      foreach (IWebElement option in options)
      {
        SetSelected(option, false);
        matched = true;
      }

      if (!matched)
      {
        throw new NoSuchElementException("Cannot locate option with value: " + value);
      }
    }

    /// <summary>
    /// Deselect the option by the index, as determined by the "index" attribute of the element.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to deselect option from a SELECT
    /// that does not support multiple selections.</exception>
    /// <exception cref="NoSuchElementException">Thrown when no element exists with the specified index attribute.</exception>
    /// <param name="index">The value of the index attribute of the option to deselect.</param>
    public void DeselectByIndex(int index)
    {
      if (!this.IsMultiple)
      {
        throw new InvalidOperationException("You may only deselect option if multi-select is supported");
      }

      string match = index.ToString(CultureInfo.InvariantCulture);
      foreach (IWebElement option in this.Options)
      {
        if (match == option.GetAttribute("index"))
        {
          SetSelected(option, false);
          return;
        }
      }

      throw new NoSuchElementException("Cannot locate option with index: " + index);
    }

    private static string EscapeQuotes(string toEscape)
    {
      // Convert strings with both quotes and ticks into: foo'"bar -> concat("foo'", '"', "bar")
      if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) > -1 && toEscape.IndexOf("'", StringComparison.OrdinalIgnoreCase) > -1)
      {
        bool quoteIsLast = false;
        if (toEscape.LastIndexOf("\"", StringComparison.OrdinalIgnoreCase) == toEscape.Length - 1)
        {
          quoteIsLast = true;
        }

        List<string> substrings = new List<string>(toEscape.Split('\"'));
        if (quoteIsLast && string.IsNullOrEmpty(substrings[substrings.Count - 1]))
        {
          // If the last character is a quote ('"'), we end up with an empty entry
          // at the end of the list, which is unnecessary. We don't want to split
          // ignoring *all* empty entries, since that might mask legitimate empty
          // strings. Instead, just remove the empty ending entry.
          substrings.RemoveAt(substrings.Count - 1);
        }

        StringBuilder quoted = new StringBuilder("concat(");
        for (int i = 0; i < substrings.Count; i++)
        {
          quoted.Append("\"").Append(substrings[i]).Append("\"");
          if (i == substrings.Count - 1)
          {
            if (quoteIsLast)
            {
              quoted.Append(", '\"')");
            }
            else
            {
              quoted.Append(")");
            }
          }
          else
          {
            quoted.Append(", '\"', ");
          }
        }

        return quoted.ToString();
      }

      // Escape string with just a quote into being single quoted: f"oo -> 'f"oo'
      if (toEscape.IndexOf("\"", StringComparison.OrdinalIgnoreCase) > -1)
      {
        return string.Format(CultureInfo.InvariantCulture, "'{0}'", toEscape);
      }

      // Otherwise return the quoted string
      return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", toEscape);
    }

    private static string GetLongestSubstringWithoutSpace(string s)
    {
      string result = string.Empty;
      string[] substrings = s.Split(' ');
      foreach (string substring in substrings)
      {
        if (substring.Length > result.Length)
        {
          result = substring;
        }
      }

      return result;
    }

    private static void SetSelected(IWebElement option, bool select)
    {
      bool isSelected = option.Selected;
      if ((!isSelected && select) || (isSelected && !select))
      {
        option.Click();
      }
    }
  }
}