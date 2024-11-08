using System;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.ElementMatching
{
  /// <summary>
  /// A matcher which combines two others together with a logical OR operation.
  /// </summary>
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
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.ElementMatching.OrMatcher"/> class.
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
