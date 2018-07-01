using CSF.Screenplay.ReportFormatting;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Reporting
{
  /// <summary>
  /// Formatter for collections of HTML elements.
  /// </summary>
  public class ElementCollectionFormatter : ObjectFormattingStrategy<ElementCollection>
  {
    /// <summary>
    /// Formats the given object.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string FormatForReport(ElementCollection obj)
      => $"a collection of elements representing '{obj.GetName()}'";
  }
}
