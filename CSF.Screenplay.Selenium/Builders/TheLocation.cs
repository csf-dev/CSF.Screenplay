using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds a question in which an actor gets the location of a target or element in the browser window.
  /// </summary>
  public class TheLocation
  {
    /// <summary>
    /// Gets a question which asks the location of a given single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<Position> Of(ITarget target)
    {
      return Questions.Question.Create(target, new LocationQuery());
    }

    /// <summary>
    /// Gets a question which asks the location of a given target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<IReadOnlyList<Position>> OfAll(ITarget target)
    {
      return Questions.Question.CreateMulti(target, new LocationQuery());
    }
  }
}
