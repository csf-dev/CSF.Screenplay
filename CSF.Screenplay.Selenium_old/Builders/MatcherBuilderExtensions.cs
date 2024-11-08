//
// QueryBasedMatcherBuilderExtensions.cs
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
using System.Collections.Generic;
using CSF.Screenplay.Selenium.ElementMatching;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Queries;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Extension methods which operate upon builder instances for an <see cref="IMatcher"/>.
  /// </summary>
  public static class MatcherBuilderExtensions
  {
    /// <summary>
    /// Creates a matcher that executes a predicate upon the value of a named attribute.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="attributeName">Attribute name.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForAttributeValue(this IQueryBasedMatcherBuilder builder,
                                             string attributeName,
                                             Func<string,bool> predicate)
    {
      var query = new AttributeQuery(attributeName);
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that looks for an exact match of the value of a named attribute.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="attributeName">Attribute name.</param>
    /// <param name="expectedValue">Expected value.</param>
    public static IMatcher ForAttributeValue(this IQueryBasedMatcherBuilder builder,
                                             string attributeName,
                                             string expectedValue)
    {
      return ForAttributeValue(builder, attributeName, x => x == expectedValue);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the value of a named CSS property.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="propertyName">CSS property name.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForCssPropertyValue(this IQueryBasedMatcherBuilder builder,
                                               string propertyName,
                                               Func<string,bool> predicate)
    {
      var query = new CssQuery(propertyName);
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that looks for an exact match of the value of a named CSS property.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="propertyName">CSS property name.</param>
    /// <param name="expectedValue">Expected value.</param>
    public static IMatcher ForCssPropertyValue(this IQueryBasedMatcherBuilder builder,
                                               string propertyName,
                                               string expectedValue)
    {
      return ForCssPropertyValue(builder, propertyName, x => x == expectedValue);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the element's location/position within the browser window.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForLocationInBrowserWindow(this IQueryBasedMatcherBuilder builder,
                                                      Func<Position, bool> predicate)
    {
      var query = new LocationQuery();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the list of available <c>&lt;option&gt;</c> elements within
    /// a select element.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForAvailableOptions(this IQueryBasedMatcherBuilder builder,
                                               Func<IReadOnlyList<Option>, bool> predicate)
    {
      var query = new OptionsQuery();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the list of selected <c>&lt;option&gt;</c> elements within
    /// a select element.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForSelectedOptions(this IQueryBasedMatcherBuilder builder,
                                              Func<IReadOnlyList<Option>, bool> predicate)
    {
      var query = new SelectedOptionsQuery();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the size of the element.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForSize(this IQueryBasedMatcherBuilder builder,
                                   Func<Size, bool> predicate)
    {
      var query = new SizeQuery();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the text of the element.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForTheText(this IQueryBasedMatcherBuilder builder,
                                      Func<string, bool> predicate)
    {
      var query = new TextQuery();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that looks for an exact match upon the text of the element.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="expectedValue">Expected value.</param>
    public static IMatcher ForTheText(this IQueryBasedMatcherBuilder builder,
                                      string expectedValue)
    {
      return ForTheText(builder, x => x == expectedValue);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the <c>value</c> of the element (this being the current
    /// value and not neccesarily the contents of the value attribute).
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForTheValue(this IQueryBasedMatcherBuilder builder,
                                      Func<string, bool> predicate)
    {
      var query = new ValueQuery();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that looks for an exact match upon the <c>value</c> of the element (this being the current
    /// value and not neccesarily the contents of the value attribute).
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="expectedValue">Expected value.</param>
    public static IMatcher ForTheValue(this IQueryBasedMatcherBuilder builder,
                                      string expectedValue)
    {
      return ForTheValue(builder, x => x == expectedValue);
    }

    /// <summary>
    /// Creates a matcher that executes a predicate upon the <c>value</c> of the element (this being the current
    /// value and not neccesarily the contents of the value attribute). The value is converted to the specified
    /// data-type before the predicate is executed.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="predicate">Predicate.</param>
    public static IMatcher ForTheConvertedValue<TValue>(this IQueryBasedMatcherBuilder builder,
                                                        Func<TValue, bool> predicate)
    {
      var query = new ValueQuery<TValue>();
      return Matcher.Create(query, predicate);
    }

    /// <summary>
    /// Creates a matcher that looks for an exact match upon the <c>value</c> of the element (this being the current
    /// value and not neccesarily the contents of the value attribute). The value is converted to the specified
    /// data-type before the predicate is executed.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    /// <param name="expectedValue">Expected value.</param>
    public static IMatcher ForTheConvertedValue<TValue>(this IQueryBasedMatcherBuilder builder,
                                                        TValue expectedValue)
    {
      return ForTheConvertedValue<TValue>(builder, x => Equals(x, expectedValue));
    }

    /// <summary>
    /// Creates a matcher that looks for elements which are clickable.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    public static IMatcher IsClickable(this ICriteriaBasedMatcherBuilder builder)
    {
      var query = new ClickableQuery();
      return Matcher.Create(query, x => x);
    }

    /// <summary>
    /// Creates a matcher that looks for elements which are not clickable.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    public static IMatcher IsNotClickable(this ICriteriaBasedMatcherBuilder builder)
    {
      var query = new ClickableQuery();
      return Matcher.Create(query, x => !x);
    }

    /// <summary>
    /// Creates a matcher that looks for elements which are visible.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    public static IMatcher IsVisible(this ICriteriaBasedMatcherBuilder builder)
    {
      var query = new VisibilityQuery();
      return Matcher.Create(query, x => x);
    }

    /// <summary>
    /// Creates a matcher that looks for elements which are not visible.
    /// </summary>
    /// <returns>The created matcher.</returns>
    /// <param name="builder">Builder.</param>
    public static IMatcher IsNotVisible(this ICriteriaBasedMatcherBuilder builder)
    {
      var query = new VisibilityQuery();
      return Matcher.Create(query, x => !x);
    }
  }
}
