using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Questions
{
  /// <summary>
  /// A helper type for composing screenplay questions.
  /// </summary>
  public static class Question
  {
    /// <summary>
    /// Creates a targetted question from a given target and query.
    /// </summary>
    /// <returns>The question instance</returns>
    /// <param name="target">The target of the question.</param>
    /// <param name="query">The query.</param>
    /// <typeparam name="T">The data-type which the query will analyse.</typeparam>
    public static Performables.IQuestion<T> Create<T>(ITarget target,
                                                      IQuery<T> query)
    {
      return new TargettedQuestion<T>(target, query);
    }

    /// <summary>
    /// Creates a targetted multi-question from a given target (representing a collection of elements) and a query.
    /// </summary>
    /// <returns>The question instance</returns>
    /// <param name="target">A target representing a collection of elements.</param>
    /// <param name="query">The query.</param>
    /// <typeparam name="T">The data-type which the query will analyse.</typeparam>
    public static Performables.IQuestion<IReadOnlyList<T>> CreateMulti<T>(ITarget target,
                                                                          IQuery<T> query)
    {
      return new TargettedMultiQuestion<T>(target, query);
    }
  }
}
