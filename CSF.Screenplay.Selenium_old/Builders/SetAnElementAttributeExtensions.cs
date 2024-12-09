//
// SetAnElementAttributeExtensions.cs
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
  /// <see cref="SetAnElementAttribute"/> JavaScript.
  /// </summary>
  public static class SetAnElementAttributeExtensions
  {
    /// <summary>
    /// Gets a performable which represents an invocation of the <see cref="SetAnElementAttribute"/> JavaScript to
    /// remove a given attribute from an element.
    /// </summary>
    /// <returns>The JavaScript question performable.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="name">The attribute name.</param>
    public static RemoveAttributeBuilder WhichRemovesTheAttribute(this ExecuteJavaScriptBuilder builder,
                                                                  string name)
    {
      return new RemoveAttributeBuilder(builder, name);
    }

    /// <summary>
    /// Gets a performable which represents an invocation of the <see cref="SetAnElementAttribute"/> JavaScript to
    /// set a given attribute upon an element to a new value.
    /// </summary>
    /// <returns>The JavaScript question performable.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="name">The attribute name.</param>
    public static ChooseElementBuilder WhichSetsTheAttribute(this ExecuteJavaScriptBuilder builder,
                                                             string name)
    {
      return new ChooseElementBuilder(builder, name);
    }

    /// <summary>
    /// Builder for an action which removes an attribute.
    /// </summary>
    public class RemoveAttributeBuilder
    {
      readonly string attributeName;
      readonly ExecuteJavaScriptBuilder builder;

      /// <summary>
      /// Choose the element from which to remove the attribute.
      /// </summary>
      /// <param name="element">Element.</param>
      public IPerformableJavaScript From(IWebElementAdapter element)
      {
        return builder.AsPerformableBuilder.BuildAction<SetAnElementAttribute>(element.GetUnderlyingElement(),
                                                                               attributeName,
                                                                               null);
      }

      internal RemoveAttributeBuilder(ExecuteJavaScriptBuilder builder, string attributeName)
      {
        if(builder == null)
          throw new ArgumentNullException(nameof(builder));
        if(attributeName == null)
          throw new ArgumentNullException(nameof(attributeName));

        this.builder = builder;
        this.attributeName = attributeName;
      }
    }

    /// <summary>
    /// Builder for an action which alters an attribute value.
    /// </summary>
    public class ChooseElementBuilder
    {
      readonly string attributeName;
      readonly ExecuteJavaScriptBuilder builder;

      /// <summary>
      /// Choose the element for which to set the attribute.
      /// </summary>
      /// <param name="element">Element.</param>
      public ChooseNewValueBuilder For(IWebElementAdapter element)
      {
        return new ChooseNewValueBuilder(builder, attributeName, element);
      }

      internal ChooseElementBuilder(ExecuteJavaScriptBuilder builder, string attributeName)
      {
        if(builder == null)
          throw new ArgumentNullException(nameof(builder));
        if(attributeName == null)
          throw new ArgumentNullException(nameof(attributeName));

        this.builder = builder;
        this.attributeName = attributeName;
      }
    }

    /// <summary>
    /// Builder for an action which alters an attribute value.
    /// </summary>
    public class ChooseNewValueBuilder
    {
      readonly string attributeName;
      readonly ExecuteJavaScriptBuilder builder;
      readonly IWebElementAdapter element;

      /// <summary>
      /// Choose the new value for the attribute.
      /// </summary>
      /// <param name="newValue">New value.</param>
      public IPerformableJavaScript To(string newValue)
      {
        return builder.AsPerformableBuilder.BuildAction<SetAnElementAttribute>(element.GetUnderlyingElement(),
                                                                               attributeName,
                                                                               newValue);
      }

      internal ChooseNewValueBuilder(ExecuteJavaScriptBuilder builder, string attributeName, IWebElementAdapter element)
      {
        if(element == null)
          throw new ArgumentNullException(nameof(element));
        if(builder == null)
          throw new ArgumentNullException(nameof(builder));
        if(attributeName == null)
          throw new ArgumentNullException(nameof(attributeName));

        this.builder = builder;
        this.attributeName = attributeName;
        this.element = element;
      }
    }
  }
}
