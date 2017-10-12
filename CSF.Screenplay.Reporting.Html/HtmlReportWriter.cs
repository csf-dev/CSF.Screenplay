using System;
using System.IO;
using CSF.Screenplay.Reporting.Models;
using CSF.Zpt;

namespace CSF.Screenplay.Reporting
{
  public class HtmlReportWriter : IReportWriter
  {
    readonly TextWriter writer;
    readonly IObjectFormattingService formattingService;
    readonly IZptDocumentFactory documentFactory;

    public void Write(Report report)
    {
      var model = GetDocumentModel(report);

      using(var stream = GetDocumentStream())
      {
        var document = documentFactory.CreateDocument(stream, RenderingMode.Html);
        document.Render(model, writer);
      }
    }

    ReportDocument GetDocumentModel(Report reportModel)
    {
      // TODO: Write this implementation
      throw new NotImplementedException();
    }

    Stream GetDocumentStream() => Views.ViewProvider.GetDocumentTemplate();

    public HtmlReportWriter(TextWriter writer, IObjectFormattingService formattingService)
    {
      if(formattingService == null)
        throw new ArgumentNullException(nameof(formattingService));
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));

      this.writer = writer;
      this.formattingService = formattingService;

      documentFactory = new ZptDocumentFactory();
    }
  }
}
