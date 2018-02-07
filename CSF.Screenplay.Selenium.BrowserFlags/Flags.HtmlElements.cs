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

      /// <summary>
      /// Flags relating to HTML <c>&lt;select multiple&gt;</c> elements.
      /// </summary>
      public static class SelectMultiple
      {
        /// <summary>
        /// Indicates that the web driver must send <c>Ctrl+Click</c> in order to toggle the selection of a single
        /// option within the select element.  Without Ctrl, the click is interpreted as "change entire selection to just
        /// the one option clicked".
        /// </summary>
        public static readonly string RequiresCtrlClickToToggleOptionSelection = "HtmlElements.SelectMultiple.RequiresCtrlClickToToggleOptionSelection";

        /// <summary>
        /// Indicates that the web driver must send <c>Command+Click</c> (Command key = Mac) in order to
        /// toggle the selection of a single option within the select element.  Without Command, the click
        /// is interpreted as "change entire selection to just the one option clicked".
        /// </summary>
        public static readonly string RequiresCommandClickToToggleOptionSelection = "HtmlElements.SelectMultiple.RequiresCommandClickToToggleOptionSelection";
      }
    }
  }
}
