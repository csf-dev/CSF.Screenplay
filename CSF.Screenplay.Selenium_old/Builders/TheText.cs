using System;
using System.Collections.Generic;
using CSF.Screenplay.Selenium.Models;
using CSF.Screenplay.Selenium.Questions;
using CSF.Screenplay.Selenium.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds a question which gets the text from a target.
  /// </summary>
  public class TheText
  {
    readonly ITarget target;

    /// <summary>
    /// Gets a question which fetches the text from a single target.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static Performables.IQuestion<string> Of(ITarget target)
    {
      return Question.Create(target, new TextQuery());
    }

    /// <summary>
    /// Gets a question which fetches the text from a target which represents a collection of elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="target">Target.</param>
    public static Performables.IQuestion<IReadOnlyList<string>> OfAll(ITarget target)
    {
      return Question.CreateMulti(target, new TextQuery());
    }

    /// <summary>
    /// Indicates the target from which the actor will be reading the text.
    /// </summary>
    /// <returns>A builder instance which may be further configured.</returns>
    /// <param name="target">Target.</param>
    public static TheText From(ITarget target)
    {
      return new TheText(target);
    }

    /// <summary>
    /// Gets a question which fetches the text from the indicated target/element and also converts that text to
    /// another data-type.
    /// </summary>
    /// <typeparam name="T">The intended conversion-type for the data.</typeparam>
    public Performables.IQuestion<T> As<T>()
    {
      return Question.Create(target, new TextQuery<T>());
    }

    TheText(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      this.target = target;
    }
  }
}
