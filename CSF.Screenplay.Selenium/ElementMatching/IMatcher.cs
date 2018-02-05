using System;
using CSF.Screenplay.Selenium.Models;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.ElementMatching
{
  /// <summary>
  /// Contains a predicate for matching Selenium web elements.
  /// </summary>
  public interface IMatcher
  {
    /// <summary>
    /// Gets a value indicating whether or not the given web element adapter matches the contained predicate or not.
    /// </summary>
    /// <returns><c>true</c>, if the adapter matches, <c>false</c> otherwise.</returns>
    /// <param name="adapter">The adapter to test.</param>
    bool IsMatch(IWebElementAdapter adapter);

    /// <summary>
    /// Gets a description for the current predicate.
    /// </summary>
    /// <returns>The description.</returns>
    string GetDescription();
  }
}
