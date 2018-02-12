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

        /// <summary>
        /// Indicates that the web driver may not perform a 'clear' action upon HTML5 date elements.
        /// Instead the web driver would have to use an alternative mechanism of clearing the value.
        /// </summary>
        public static readonly string CannotClearDateInteractively = "HtmlElements.InputTypeDate.CannotClearDateInteractively";
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
        /// Indicates that the web driver must send <c>Command+Click</c> in order to toggle the selection of a single
        /// option within the select element.  The Command key is the Mac command/logo key, equivalent to the
        /// Windows logo key.  It is sometimes called "Meta" or "Super" as well.
        /// Without Command, the click is interpreted as "change entire selection to just the one option clicked".
        /// </summary>
        public static readonly string RequiresCommandClickToToggleOptionSelection = "HtmlElements.SelectMultiple.RequiresCommandClickToToggleOptionSelection";
      }

      /// <summary>
      /// Flags relating to HTML <c>&lt;select&gt;</c> elements.
      /// </summary>
      public static class Select
      {
        /// <summary>
        /// Indicates that the browser cannot read the zero-based index of individual options.
        /// </summary>
        public static readonly string CannotReadOptionIndexes = "HtmlElements.Select.CannotReadOptionIndexes";
      }
    }
  }
}
