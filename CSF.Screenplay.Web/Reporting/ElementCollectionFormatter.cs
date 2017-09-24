using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Reporting
{
  /// <summary>
  /// Formatter for collections of HTML elements.
  /// </summary>
  public class ElementCollectionFormatter : GenericObjectFormatter<ElementCollection>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    protected override string GetFormattedName(ElementCollection obj)
      => $"a collection of elements representing '{obj.GetName()}'";
  }
}
