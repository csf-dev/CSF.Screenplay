using System;
using System.Reflection;
using System.IO;
using System.Linq;
using CSF.Reflection;

namespace CSF.Screenplay.Reporting.Views
{
  /// <summary>
  /// A helper service which provides access to the ZPT document template which provides the 'view' for rendering
  /// Screenplay reports.
  /// </summary>
  public static class ViewProvider
  {
    const string DocumentTemplateName = "DocumentTemplate.pt";

    internal static Stream GetDocumentTemplate()
      => ThisAssembly.GetManifestResourceStream(DocumentTemplateName);

    static Assembly ThisAssembly => Assembly.GetExecutingAssembly();
  }
}
