using System;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Queries;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.ElementMatching
{
  /// <summary>
  /// Implementation of <see cref="IMatcher"/> which uses a query and a predicate delegate.
  /// </summary>
  public class Matcher<T> : IMatcher
  {
    readonly Func<T,bool> predicate;
    readonly IQuery<T> query;

    /// <summary>
    /// Gets the predicate.
    /// </summary>
    /// <value>The predicate.</value>
    protected Func<T,bool> Predicate => predicate;

    /// <summary>
    /// Gets the query.
    /// </summary>
    /// <value>The query.</value>
    protected IQuery<T> Query => query;

    /// <summary>
    /// Gets a description for the current predicate.
    /// </summary>
    /// <returns>The description.</returns>
    public virtual string GetDescription() => query.GetMatchDescription();

    /// <summary>
    /// Gets a value indicating whether or not the given Selenium web element matches the contained predicate or not.
    /// </summary>
    /// <returns><c>true</c>, if the element matches, <c>false</c> otherwise.</returns>
    /// <param name="element">The element to test.</param>
    public virtual bool IsMatch(IWebElement element)
    {
      var adapter = GetAdapter(element);
      return IsMatch(adapter);
    }

    /// <summary>
    /// Gets a value indicating whether or not the given web element adapter matches the contained predicate or not.
    /// </summary>
    /// <returns><c>true</c>, if the adapter matches, <c>false</c> otherwise.</returns>
    /// <param name="adapter">The adapter to test.</param>
    public virtual bool IsMatch(IWebElementAdapter adapter)
    {
      var data = query.GetElementData(adapter);
      return IsMatch(adapter, data);
    }

    /// <summary>
    /// Gets a value indicating whether the given data matches the predicate or not.
    /// </summary>
    /// <returns><c>true</c>, if the data matches, <c>false</c> otherwise.</returns>
    /// <param name="adapter">The adapter to test.</param>
    /// <param name="data">The data to test.</param>
    protected virtual bool IsMatch(IWebElementAdapter adapter, T data)
    {
      return Predicate(data);
    }

    /// <summary>
    /// Gets a web element adapter for the given element.
    /// </summary>
    /// <returns>The adapter.</returns>
    /// <param name="element">Element.</param>
    protected IWebElementAdapter GetAdapter(IWebElement element)
    {
      if(element == null)
        throw new ArgumentNullException(nameof(element));

      return new SeleniumWebElementAdapter(element);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Matcher{1}"/> class.
    /// </summary>
    /// <param name="query">Query.</param>
    protected Matcher(IQuery<T> query)
    {
      if(query == null)
        throw new ArgumentNullException(nameof(query));

      this.query = query;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Matcher{1}"/> class.
    /// </summary>
    /// <param name="query">Query.</param>
    /// <param name="predicate">Predicate.</param>
    public Matcher(IQuery<T> query, Func<T,bool> predicate) : this(query)
    {
      if(predicate == null)
        throw new ArgumentNullException(nameof(predicate));

      this.predicate = predicate;
    }
  }
}
