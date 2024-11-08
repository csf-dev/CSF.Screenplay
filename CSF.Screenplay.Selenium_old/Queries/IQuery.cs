using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Queries
{
  /// <summary>
  /// Provides information from a Selenium web element (or a wrapper around an element).
  /// </summary>
  public interface IQuery
  {
    /// <summary>
    /// Gets a report appropriate to a question which gets this value from a target.
    /// </summary>
    /// <returns>The question report.</returns>
    /// <param name="actor">Actor.</param>
    /// <param name="targetName">Target name.</param>
    string GetQuestionReport(INamed actor, string targetName);

    /// <summary>
    /// Gets a description for a match on this value, suitable for an <see cref="ElementMatching.IMatcher"/>
    /// </summary>
    /// <returns>The match description.</returns>
    string GetMatchDescription();

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="element">Element.</param>
    object GetElementData(IWebElement element);

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Adapter.</param>
    object GetElementData(IWebElementAdapter adapter);
  }
}
