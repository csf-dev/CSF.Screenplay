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
      using(var stream = GetDocumentStream())
      {
        var document = GetDocument(stream);
        var model = GetDocumentModel(report, document);
        document.Render(model, writer);
      }
    }

    ReportDocument GetDocumentModel(Report reportModel, IZptDocument document)
      => new ReportDocument(reportModel, formattingService, document);

    IZptDocument GetDocument(Stream stream)
      => documentFactory.CreateDocument(stream, RenderingMode.Html, GetSourceInfo());

    ISourceInfo GetSourceInfo() => new StreamSourceInfo("ReportDocument");

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
