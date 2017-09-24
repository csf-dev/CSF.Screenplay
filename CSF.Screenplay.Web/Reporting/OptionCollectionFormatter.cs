using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Reporting
{
  /// <summary>
  /// Formatter for collections of options (as would appear in an HTML <c>select</c> element).
  /// </summary>
  public class OptionCollectionFormatter : GenericObjectFormatter<IEnumerable<Option>>
  {
    /// <summary>
    /// Gets a formatted name for the given input.
    /// </summary>
    /// <param name="obj">Object.</param>
    protected override string GetFormattedName(IEnumerable<Option> obj)
    {
      var options = obj.Select(x => x.Text);
      return $"The options {String.Join(", ", options)}";
    }
  }
}
