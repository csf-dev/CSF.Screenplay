using System;
namespace CSF.Screenplay.Selenium
{
  public static partial class Flags
  {
    /// <summary>
    /// Flags relating to specific HTML elements.
    /// </summary>
    public static class HtmlElements
    {
      /// <summary>
      /// Flags relating to HTML <c>&lt;input type="date"&gt;</c> elements.
      /// </summary>
      public static class InputTypeDate
      {
        /// <summary>
        /// Indicates that the web driver may be used to enter a date using a format that conforms to the web browser's
        /// current locale setting.
        /// </summary>
        public static readonly string CanEnterUsingLocaleFormat = "HtmlElements.InputTypeDate.CanEnterUsingLocaleFormat";

        /// <summary>
        /// Indicates that the web driver may be used to enter a date using an ISO date string: <c>yyyy-MM-dd</c>.
        /// </summary>
        public static readonly string CanEnterUsingIsoFormat = "HtmlElements.InputTypeDate.CanEnterUsingIsoFormat";
      }
    }
  }
}
