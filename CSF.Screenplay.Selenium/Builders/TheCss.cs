using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds a question which gets the value of a named CSS property from a target.
  /// </summary>
  public class TheCss
  {
    readonly string property;

    /// <summary>
    /// Gets a question which fetches the CSS value from a single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public IQuestion<string> From(ITarget target)
    {
      return Questions.Question.Create(target, new CssQuery(property));
    }

    /// <summary>
    /// Gets a question which fetches the CSS value from a target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public IQuestion<IReadOnlyList<string>> FromAllOf(ITarget target)
    {
      return Questions.Question.CreateMulti(target, new CssQuery(property));
    }

    /// <summary>
    /// Indicates the name of the CSS property which is to be retrieved.
    /// </summary>
    /// <returns>A builder instance which may be further configured.</returns>
    /// <param name="name">The property name.</param>
    public static TheCss Property(string name)
    {
      return new TheCss(name);
    }

    TheCss(string property)
    {
      if(property == null)
        throw new ArgumentNullException(nameof(property));

      this.property = property;
    }
  }
}
