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
        public static readonly string RequiresEntryUsingLocaleFormat = "HtmlElements.InputTypeDate.RequiresEntryUsingLocaleFormat";

        /// <summary>
        /// Indicates that the web driver must use a JavaScript workaround to set the date, because it is impossible to do so
        /// by typing keys.
        /// </summary>
        public static readonly string RequiresInputViaJavaScriptWorkaround = "HtmlElements.InputTypeDate.RequiresInputViaJavaScriptWorkaround";
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
      }

      /// <summary>
      /// Flags relating to HTML <c>&lt;select&gt;</c> elements.
      /// </summary>
      public static class Select
      {
        /// <summary>
        /// Indicates that the browser is completely unable to change the selection state of an HTML <c>&lt;select&gt;</c>
        /// element.
        /// </summary>
        public static readonly string CannotChangeState = "HtmlElements.Select.CannotChangeState";
      }
    }
  }
}
