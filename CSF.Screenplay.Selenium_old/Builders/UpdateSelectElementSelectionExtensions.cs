//
// UpdateSelectElementSelectionExtensions.cs
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
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.ScriptResources;
using static CSF.Screenplay.Selenium.ScriptResources.UpdateSelectElementSelection;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Extension methods for a <see cref="ExecuteJavaScriptBuilder"/>, related to building performables which use the
  /// <see cref="UpdateSelectElementSelection"/> JavaScript.
  /// </summary>
  public static class UpdateSelectElementSelectionExtensions
  {
    /// <summary>
    /// Creates a builder for selecting a single option within an HTML <c>&lt;select&gt;</c> element.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <param name="builder">A JavaScript execution builder.</param>
    /// <param name="element">Element.</param>
    public static OptionChoiceBuilder WhichSelectsTheOptionFrom(this ExecuteJavaScriptBuilder builder,
                                                                IWebElementAdapter element)
    {
      return new OptionChoiceBuilder(builder, element, true);
    }

    /// <summary>
    /// Creates a builder for deselecting a single option within an HTML <c>&lt;select&gt;</c> element.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <param name="builder">A JavaScript execution builder.</param>
    /// <param name="element">Element.</param>
    public static OptionChoiceBuilder WhichDeselectsTheOptionFrom(this ExecuteJavaScriptBuilder builder,
                                                                  IWebElementAdapter element)
    {
      return new OptionChoiceBuilder(builder, element, false);
    }

    /// <summary>
    /// Creates a performable action for deselecting every option from an HTML <c>&lt;select&gt;</c> element.
    /// </summary>
    /// <returns>The builder.</returns>
    /// <param name="builder">A JavaScript execution builder.</param>
    /// <param name="element">Element.</param>
    public static IPerformableJavaScript WhichDeselectsEverythingFrom(this ExecuteJavaScriptBuilder builder,
                                                                      IWebElementAdapter element)
    {
      var jsBuilder = builder.AsPerformableBuilder;
      return jsBuilder.BuildAction<UpdateSelectElementSelection>(element.GetUnderlyingElement(), DeselectAllActionName);
    }

    /// <summary>
    /// A builder type for choosing a single option element.
    /// </summary>
    public class OptionChoiceBuilder
    {
      readonly IWebElementAdapter element;
      readonly bool select;
      readonly ExecuteJavaScriptBuilder builder;

      /// <summary>
      /// Chooses an option by its zero-based index and returns a performable JavaScript object
      /// </summary>
      /// <returns>A performable JavaScript.</returns>
      /// <param name="index">Index.</param>
      public IPerformableJavaScript ByIndex(int index)
      {
        var actionName = select? SelectByIndexActionName : DeselectByIndexActionName;
        return BuildAction(actionName, index);
      }

      /// <summary>
      /// Chooses an option by its underlying value and returns a performable JavaScript object
      /// </summary>
      /// <returns>A performable JavaScript.</returns>
      /// <param name="value">Value.</param>
      public IPerformableJavaScript ByValue(string value)
      {
        var actionName = select? SelectByValueActionName : DeselectByValueActionName;
        return BuildAction(actionName, value);
      }

      /// <summary>
      /// Chooses an option by its displayed text and returns a performable JavaScript object
      /// </summary>
      /// <returns>A performable JavaScript.</returns>
      /// <param name="text">Text.</param>
      public IPerformableJavaScript ByText(string text)
      {
        var actionName = select? SelectByTextActionName : DeselectByTextActionName;
        return BuildAction(actionName, text);
      }

      IPerformableJavaScript BuildAction(string actionName, object actionValue)
      {
        var jsBuilder = builder.AsPerformableBuilder;
        return jsBuilder.BuildAction<UpdateSelectElementSelection>(element.GetUnderlyingElement(), actionName, actionValue);
      }

      internal OptionChoiceBuilder(ExecuteJavaScriptBuilder builder, IWebElementAdapter element, bool select)
      {
        if(builder == null)
          throw new ArgumentNullException(nameof(builder));
        if(element == null)
          throw new ArgumentNullException(nameof(element));

        this.builder = builder;
        this.element = element;
        this.select = select;
      }
    }
  }
}
