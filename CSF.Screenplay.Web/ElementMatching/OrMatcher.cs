using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.ElementMatching
{
  public class OrMatcher : IMatcher
  {
    readonly IMatcher first, second;

    /// <summary>
    /// Gets a description for the current predicate.
    /// </summary>
    /// <returns>The description.</returns>
    public string GetDescription() => $"{GetDescription(first)} or {GetDescription(second)}";

    string GetDescription(IMatcher matcher)
    {
      var description = matcher.GetDescription();

      if(RequiresParentheses(matcher))
      {
        return String.Concat("(", description, ")");
      }

      return description;
    }

    bool RequiresParentheses(IMatcher matcher)
    {
      return (matcher is AndMatcher);
    }

    /// <summary>
    /// Gets a value indicating whether or not the given web element adapter matches the contained predicate or not.
    /// </summary>
    /// <returns><c>true</c>, if the adapter matches, <c>false</c> otherwise.</returns>
    /// <param name="adapter">The adapter to test.</param>
    public bool IsMatch(IWebElementAdapter adapter)
    {
      return first.IsMatch(adapter) || second.IsMatch(adapter);
    }

    /// <summary>
    /// Gets a value indicating whether or not the given Selenium web element matches the contained predicate or not.
    /// </summary>
    /// <returns><c>true</c>, if the element matches, <c>false</c> otherwise.</returns>
    /// <param name="element">The element to test.</param>
    public bool IsMatch(IWebElement element)
    {
      return first.IsMatch(element) && second.IsMatch(element);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.ElementMatching.OrMatcher"/> class.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public OrMatcher(IMatcher first, IMatcher second)
    {
      if(first == null)
        throw new ArgumentNullException(nameof(first));
      if(second == null)
        throw new ArgumentNullException(nameof(second));

      this.first = first;
      this.second = second;
    }
  }
}
