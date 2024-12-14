//
// SetAnElementValueExtensions.cs
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

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Extension methods for a <see cref="ExecuteJavaScriptBuilder"/>, related to building performables which use the
  /// <see cref="SetAnElementValue"/> JavaScript.
  /// </summary>
  public static class SetAnElementValueExtensions
  {
    /// <summary>
    /// Gets a builder which assists in the creation of the performable.  In this method call, the target of the
    /// value-setting operation is decided.
    /// </summary>
    /// <returns>The set-element-value builder.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="element">The target element, which is to have its value set.</param>
    public static SetAnElementValueBuilder WhichSetsTheValueOf(this ExecuteJavaScriptBuilder builder,
                                                               IWebElementAdapter element)
    {
      if(builder == null)
        throw new ArgumentNullException(nameof(builder));
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      return new SetAnElementValueBuilder(builder, element);
    }

    /// <summary>
    /// A builder type which creates a performable which uses the <see cref="SetAnElementValue"/> JavaScript.
    /// </summary>
    public class SetAnElementValueBuilder
    {
      readonly IWebElementAdapter element;
      readonly ExecuteJavaScriptBuilder builder;

      /// <summary>
      /// Gets the performable action from the new value to set into the element's 'value' property.
      /// </summary>
      /// <param name="value">The new value.</param>
      public IPerformableJavaScript To(object value)
      {
        var webElement = element.GetUnderlyingElement();
        return builder.AsPerformableBuilder.BuildAction<SetAnElementValue>(webElement, value);
      }

      internal SetAnElementValueBuilder(ExecuteJavaScriptBuilder builder, IWebElementAdapter element)
      {
        if(builder == null)
          throw new ArgumentNullException(nameof(builder));
        if(element == null)
          throw new ArgumentNullException(nameof(element));
        
        this.builder = builder;
        this.element = element;
      }
    }
  }
}
