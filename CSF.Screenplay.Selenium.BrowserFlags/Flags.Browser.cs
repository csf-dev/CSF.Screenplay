using System;

namespace CSF.Screenplay.Selenium
{
  public static partial class Flags
  {
    /// <summary>
    /// Flags relating to the web browser itself, such as management of cookies.
    /// </summary>
    public static class Browser
    {
      /// <summary>
      /// Indicates that the web driver is incapable of clearing the cookies for the current domain.
      /// </summary>
      /// <remarks>
      /// <para>
      /// The current domain is the domain for the page upon which the web driver is currently viewing.
      /// </para>
      /// </remarks>
      public static readonly string CannotClearDomainCookies = "Browser.CannotClearDomainCookies";
    }
  }
}
