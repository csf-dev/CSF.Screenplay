using System;
using System.IO;
using CSF.Screenplay.Reporting.Models;
using CSF.Zpt;
using CSF.Zpt.Rendering;

namespace CSF.Screenplay.Reporting
{
  public class HtmlReportWriter : IReportWriter
  {
    readonly TextWriter writer;
    readonly IObjectFormattingService formattingService;
    readonly IZptDocumentFactory documentFactory;

    public void Write(Report report)
    {
      var doc = GetDocument();
      var emptyDoc = GetDocument();
      var model = GetDocumentModel(report, emptyDoc);

      doc.Render(model, writer);
    }

    IZptDocument GetDocument()
    {
      using(var stream = GetDocumentStream())
      {
        return documentFactory.CreateDocument(stream, RenderingMode.Html);
      }
    }

    ReportDocument GetDocumentModel(Report reportModel, IZptDocument document)
      => new ReportDocument(reportModel, formattingService, document);

    Stream GetDocumentStream()
      => Views.ViewProvider.GetDocumentTemplate();

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
