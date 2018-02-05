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
  public class ElementCollectionFormatter : ObjectFormatter<ElementCollection>
  {
    /// <summary>
    /// Formats the given object.
    /// </summary>
    /// <param name="obj">Object.</param>
    public override string Format(ElementCollection obj)
      => $"a collection of elements representing '{obj.GetName()}'";
  }
}
