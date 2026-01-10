//
// SelectElementUsingModifierKey.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
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
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CSF.Selenium.Support.UI
{
  /// <summary>
  /// Specialisation of <see cref="SelectElement"/> which makes use of a modifier key in order to
  /// toggle the selection state of an option inside the select element when that select element
  /// allows multiple options to be selected.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Recent browsers are now requiring ctrl-click or command-click in order to toggle the selection
  /// of a single option.  This enables that behaviour.
  /// </para>
  /// <para>
  /// It works around the issue described at: https://github.com/SeleniumHQ/selenium/issues/4490
  /// </para>
  /// </remarks>
  public class SelectElementUsingModifierKey : SelectElement
  {
    readonly string modifierKey;
    readonly IWebDriver driver;

    /// <summary>
    /// Toggles the selected/deselected state of the given option element.
    /// </summary>
    /// <param name="option">The option element for which to toggle the selection state.</param>
    protected override void ToggleSelectedState(IWebElement option)
    {
      if(!IsMultiple)
      {
        base.ToggleSelectedState(option);
        return;
      }

      new Actions(driver)
        .KeyDown(modifierKey)
        .Click(option)
        .KeyUp(modifierKey)
        .Perform();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Selenium.Support.UI.SelectElementUsingModifierKey"/> class.
    /// </summary>
    /// <param name="element">Element.</param>
    /// <param name="modifierKey">Modifier key.</param>
    /// <param name="driver">Driver.</param>
    public SelectElementUsingModifierKey(IWebElement element, string modifierKey, IWebDriver driver) : base(element)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));
      if(modifierKey == null)
        throw new ArgumentNullException(nameof(modifierKey));
      
      this.driver = driver;
      this.modifierKey = modifierKey;
    }
  }
}
