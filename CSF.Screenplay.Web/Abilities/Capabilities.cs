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
  }
}
