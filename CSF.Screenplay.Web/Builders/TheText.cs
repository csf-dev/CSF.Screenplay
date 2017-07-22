using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Builders
{
  /// <summary>
  /// Builds a question which gets the text from a target.
  /// </summary>
  public class TheText
  {
    readonly ITarget target;
    readonly IWebElement element;

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
    /// Gets a question which fetches the text from a web element.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="element">Element.</param>
    public static Performables.IQuestion<string> Of(IWebElement element)
    {
      return Question.Create(element, new TextQuery());
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
    /// Gets a question which fetches the text from a collection of web elements.
    /// </summary>
    /// <returns>A performable question instance.</returns>
    /// <param name="elements">Elements.</param>
    public static Performables.IQuestion<IReadOnlyList<string>> Of(IReadOnlyList<IWebElement> elements)
    {
      return Question.CreateMulti(elements, new TextQuery());
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
    /// Indicates the element from which the actor will be reading the text.
    /// </summary>
    /// <returns>A builder instance which may be further configured.</returns>
    /// <param name="element">Element.</param>
    public static TheText From(IWebElement element)
    {
      return new TheText(element);
    }

    /// <summary>
    /// Gets a question which fetches the text from the indicated target/element and also converts that text to
    /// another data-type.
    /// </summary>
    /// <typeparam name="T">The intended conversion-type for the data.</typeparam>
    public Performables.IQuestion<T> As<T>()
    {
      if(target != null)
        return Question.Create(target, new TextQuery<T>());

      return Question.Create(element, new TextQuery<T>());
    }

    TheText(ITarget target)
    {
      if(target == null)
        throw new ArgumentNullException(nameof(target));
      this.target = target;
    }

    TheText(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));
      this.element = element;
    }
  }
}
