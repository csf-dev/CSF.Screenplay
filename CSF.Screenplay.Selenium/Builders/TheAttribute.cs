using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds a question which gets the value of an attribute from a target.
  /// </summary>
  public class TheAttribute
  {
    readonly string name;

    /// <summary>
    /// Gets a question which fetches the attribute value from a single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public IQuestion<string> From(ITarget target)
    {
      return Question.Create(target, new AttributeQuery(name));
    }

    /// <summary>
    /// Gets a question which fetches the attribute value from a target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public IQuestion<IReadOnlyList<string>> FromAllOf(ITarget target)
    {
      return Question.CreateMulti(target, new AttributeQuery(name));
    }

    /// <summary>
    /// Indicates the name of the attribute which is to be retrieved.
    /// </summary>
    /// <returns>A builder instance which may be further configured.</returns>
    /// <param name="name">The attribute name.</param>
    public static TheAttribute Named(string name)
    {
      return new TheAttribute(name);
    }

    TheAttribute(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }
  }
}
