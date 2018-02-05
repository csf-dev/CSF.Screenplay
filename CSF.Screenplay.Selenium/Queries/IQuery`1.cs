using System;
using CSF.Screenplay.Web.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Queries
{
  /// <summary>
  /// Provides typed information from a Selenium web element (or a wrapper around an element).
  /// </summary>
  public interface IQuery<T> : IQuery
  {
    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="element">Element.</param>
    new T GetElementData(IWebElement element);

    /// <summary>
    /// Gets the element data.
    /// </summary>
    /// <returns>The element data.</returns>
    /// <param name="adapter">Adapter.</param>
    new T GetElementData(IWebElementAdapter adapter);
  }
}
