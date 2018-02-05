using System;
using System.Collections.Generic;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using CSF.Screenplay.Web.Questions;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds a question which fetches representations of HTML <c>option</c> elements (within a <c>select</c> element).
  /// </summary>
  public class TheOptions
  {
    /// <summary>
    /// Gets a question which will fetch all of the options in a given single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<IReadOnlyList<Option>> In(ITarget target)
    {
      return Question.Create(target, new OptionsQuery());
    }

    /// <summary>
    /// Gets a question which will fetch all of the options which are selected in a given single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<IReadOnlyList<Option>> SelectedIn(ITarget target)
    {
      return Question.Create(target, new SelectedOptionsQuery());
    }

    /// <summary>
    /// Gets a question which will fetch all of the options in a given target which represents a collection of
    /// select elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<IReadOnlyList<IReadOnlyList<Option>>> InAllOf(ITarget target)
    {
      return Question.CreateMulti(target, new OptionsQuery());
    }

    /// <summary>
    /// Gets a question which will fetch all of the selected options in a given target which represents a
    /// collection of select elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static IQuestion<IReadOnlyList<IReadOnlyList<Option>>> SelectedInAllOf(ITarget target)
    {
      return Question.CreateMulti(target, new SelectedOptionsQuery());
    }
  }
}
