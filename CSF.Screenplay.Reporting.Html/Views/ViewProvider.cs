using System;
using System.Reflection;
using System.IO;

namespace CSF.Screenplay.Reporting.Views
{
  public static class ViewProvider
  {
    const string
      DocumentTemplateName = "DocumentTemplate.pt";

    internal static Stream GetDocumentTemplate()
      => ThisAssembly.GetManifestResourceStream(DocumentTemplateName);

    static Assembly ThisAssembly => Assembly.GetExecutingAssembly();
  }
}
