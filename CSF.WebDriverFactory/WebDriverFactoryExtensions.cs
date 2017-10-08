using System;
using OpenQA.Selenium;

namespace CSF.WebDriverFactory
{
  /// <summary>
  /// Extension methods to web driver factory types.
  /// </summary>
  public static class WebDriverFactoryExtensions
  {
    /// <summary>
    /// Marks the web driver as having passed the current test.
    /// </summary>
    /// <param name="factory">Factory.</param>
    /// <param name="driver">Driver.</param>
    public static void MarkTestAsPassed(this IWebDriverFactory factory, IWebDriver driver)
    {
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var outcomeFactory = factory as IMarksWebDriverWithOutcome;
      if(outcomeFactory == null)
        return;

      outcomeFactory.MarkAsSuccess(driver);
    }

    /// <summary>
    /// Marks the web driver as having failed the current test.
    /// </summary>
    /// <param name="factory">Factory.</param>
    /// <param name="driver">Driver.</param>
    public static void MarkTestAsFailed(this IWebDriverFactory factory, IWebDriver driver)
    {
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var outcomeFactory = factory as IMarksWebDriverWithOutcome;
      if(outcomeFactory == null)
        return;

      outcomeFactory.MarkAsFailure(driver);
    }
  }
}
