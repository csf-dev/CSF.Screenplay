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
  static class ViewProvider
  {
    static readonly ResourceBundler bundler = new ResourceBundler();

    internal static Stream GetDocumentTemplate()
      => ThisAssembly.GetManifestResourceStream("DocumentTemplate.pt");

    static Assembly ThisAssembly => Assembly.GetExecutingAssembly();

    internal static string GetBundledStyles()
      => bundler.GetBundle("reset.css", "page.css", "reports.css", "interactions.css");

    internal static string GetBundledScripts()
      => bundler.GetBundle("jquery-3.2.1.slim.min.js", "PageInteractions.js", "Filter.js", "Folding.js", "Interactions.js");
  }
}
