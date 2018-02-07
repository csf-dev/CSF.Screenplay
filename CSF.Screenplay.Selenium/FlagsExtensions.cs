using System;
using CSF.WebDriverExtras;

namespace CSF.Screenplay.Selenium
{
  /// <summary>
  /// Extension methods relating to an <c>IHasFlags</c> web driver.
  /// </summary>
  public static class FlagsExtensions
  {
    /// <summary>
    /// Ensures that the web driver has the named flag.  If it does not then an exception is raised.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    /// <param name="flagName">Flag name.</param>
    public static void RequireFlag(this IHasFlags webDriver, string flagName)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));
      if(flagName == null)
        throw new ArgumentNullException(nameof(flagName));

      if(!webDriver.HasFlag(flagName))
        throw new FlagRequiredException($"The flag '{flagName}' is required but the current web driver does not possess it.");
    }

    /// <summary>
    /// Ensures that the web driver does not have the named flag.  If it does then an exception is raised.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    /// <param name="flagName">Flag name.</param>
    public static void ThrowOnFlag(this IHasFlags webDriver, string flagName)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));
      if(flagName == null)
        throw new ArgumentNullException(nameof(flagName));

      if(webDriver.HasFlag(flagName))
        throw new FlagRequiredException($"The flag '{flagName}' must not be present but the current web driver possesses it.");
    }
  }
}
