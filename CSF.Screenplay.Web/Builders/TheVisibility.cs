using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;
using CSF.Screenplay.Web.Queries;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds a question which gets the visibility of a target.
  /// </summary>
  public class TheVisibility
  {
    readonly ITarget target;

    /// <summary>
    /// Gets a question which fetches the text from a single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static Performables.IQuestion<bool> Of(ITarget target)
    {
      return Question.Create(target, new VisibilityQuery());
    }

    /// <summary>
    /// Gets a question which fetches the text from a target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static Performables.IQuestion<IReadOnlyList<bool>> OfAll(ITarget target)
    {
      return Question.CreateMulti(target, new VisibilityQuery());
    }

    TheVisibility(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      this.target = target;
    }
  }
}
