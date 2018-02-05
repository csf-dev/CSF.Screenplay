using System;
using CSF.Screenplay.Selenium.Queries;
using CSF.Screenplay.Selenium.ElementMatching;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// A helper type for composing <see cref="IMatcher"/> instances.
  /// </summary>
  public static class Matcher
  {
    /// <summary>
    /// Creates a matcher from a given query and predicate.
    /// </summary>
    /// <param name="query">The query which gets a value from the element.</param>
    /// <param name="predicate">The predicate which tests the retrieved value.</param>
    /// <typeparam name="T">The data type which the query and predicate work upon.</typeparam>
    public static IMatcher Create<T>(IQuery<T> query, Func<T,bool> predicate)
    {
      return new Matcher<T>(query, predicate);
    }

    /// <summary>
    /// Creates a matcher which combines two other matchers using a logical AND operation.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public static IMatcher And(this IMatcher first, IMatcher second)
    {
      return new AndMatcher(first, second);
    }

    /// <summary>
    /// Creates a matcher which combines two other matchers using a logical OR operation.
    /// </summary>
    /// <param name="first">First.</param>
    /// <param name="second">Second.</param>
    public static IMatcher Or(this IMatcher first, IMatcher second)
    {
      return new OrMatcher(first, second);
    }
  }
}
