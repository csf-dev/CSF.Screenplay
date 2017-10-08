using System;
using OpenQA.Selenium;

namespace CSF.WebDriverFactory
{
  /// <summary>
  /// Indicates a type which is capable of marking a web driver with outcome-related information.
  /// </summary>
  public interface IMarksWebDriverWithOutcome
  {
    /// <summary>
    /// Marks the web driver as having passed the current test scenario.
    /// </summary>
    /// <param name="driver">Driver.</param>
    void MarkAsSuccess(IWebDriver driver);

    /// <summary>
    /// Marks the web driver as having failed the current test scenario.
    /// </summary>
    /// <param name="driver">Driver.</param>
    void MarkAsFailure(IWebDriver driver);
  }
}
