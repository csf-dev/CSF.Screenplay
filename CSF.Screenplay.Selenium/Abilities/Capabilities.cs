using System;
namespace CSF.Screenplay.Web.Abilities
{
  /// <summary>
  /// Enumerates the various capabilities which a web browser may or may not have.
  /// </summary>
  public static class Capabilities
  {
    /// <summary>
    /// A capability which indicates that the browser is able to clear all of its cookies for a given domain.
    /// </summary>
    public static readonly string ClearDomainCookies = "Clear all cookies for a domain";

    /// <summary>
    /// A capability which indicates that the browser allows the user to enter locale-formatted dates
    /// into <c>&lt;input type="date" /&gt;</c> controls.
    /// </summary>
    public static readonly string EnterDatesInLocaleFormat = "Enter values into input type='date' controls as a locale-formatted string";

    /// <summary>
    /// A capability which indicates that the browser allows the user to enter locale-formatted dates
    /// into <c>&lt;input type="date" /&gt;</c> controls.
    /// </summary>
    public static readonly string EnterDatesAsIsoStrings = "Enter values into input type='date' controls as an ISO-formatted string (Year-Month-Day)";
  }
}
