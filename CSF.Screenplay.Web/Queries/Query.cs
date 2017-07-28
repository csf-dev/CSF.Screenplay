using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Base type for simple query implementations, which get data from elements.
  /// </summary>
  public abstract class Query<T> : IQuery<T>
  {
    /// <summary>
    /// Gets a report appropriate to a question which gets this value from a target.
    /// </summary>
    /// <returns>The question report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">Target name.</param>
    protected abstract string GetQuestionReport(INamed actor, string targetName);

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher"/>
    /// </summary>
    /// <returns>The match description.</returns>
    protected abstract string GetMatchDescription();

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Adapter.</param>
    protected abstract T GetElementData(IWebElementAdapter adapter);

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="element">Element.</param>
    protected virtual T GetElementData(IWebElement element)
    {
      var adapter = new SeleniumWebElementAdapter(element);
      return GetElementData(adapter);
    }

    T IQuery<T>.GetElementData(IWebElementAdapter adapter)
    {
      return GetElementData(adapter);
    }

    object IQuery.GetElementData(IWebElementAdapter adapter)
    {
      return GetElementData(adapter);
    }

    object IQuery.GetElementData(IWebElement element)
    {
      return GetElementData(element);
    }

    T IQuery<T>.GetElementData(IWebElement element)
    {
      return GetElementData(element);
    }

    string IQuery.GetQuestionReport(INamed actor, string targetName)
    {
      return GetQuestionReport(actor, targetName);
    }

    string IQuery.GetMatchDescription()
    {
      return GetMatchDescription();
    }
  }
}
