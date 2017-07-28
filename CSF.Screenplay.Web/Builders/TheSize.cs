using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds a question in which an actor gets the size of a target or element.
  /// </summary>
  public class TheSize
  {
    /// <summary>
    /// Gets a question which asks the size of a given single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<Size> Of(ITarget target)
    {
      return Questions.Question.Create(target, new SizeQuery());
    }

    /// <summary>
    /// Gets a question which asks the size of a given target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<IReadOnlyList<Size>> OfAll(ITarget target)
    {
      return Questions.Question.CreateMulti(target, new SizeQuery());
    }
  }
}
