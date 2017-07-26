using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds a question which gets the value (for example of an <c>input</c> element) from a target.
  /// </summary>
  public class TheValue
  {
    readonly ITarget target;

    /// <summary>
    /// Gets a question which fetches the value from a single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static Performables.IQuestion<string> Of(ITarget target)
    {
      return Question.Create(target, new ValueQuery());
    }

    /// <summary>
    /// Gets a question which fetches the value from a target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static Performables.IQuestion<IReadOnlyList<string>> OfAll(ITarget target)
    {
      return Question.CreateMulti(target, new ValueQuery());
    }

    /// <summary>
    /// Indicates the target from which the actor will be reading the value.
    /// </summary>
    /// <returns>A builder instance which may be further configured.</returns>
    /// <param name="target">Target.</param>
    public static TheValue From(ITarget target)
    {
      return new TheValue(target);
    }

    /// <summary>
    /// Gets a question which fetches the value from the indicated target/element and also converts that value to
    /// another data-type.
    /// </summary>
    /// <typeparam name="T">The intended conversion-type for the data.</typeparam>
    public Performables.IQuestion<T> As<T>()
    {
      return Question.Create(target, new ValueQuery<T>());
    }

    TheValue(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      this.target = target;
    }
  }
}
