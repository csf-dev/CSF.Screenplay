using System;
using System.IO;
using CSF.Screenplay.Reporting.Models;
using CSF.Zpt;
using CSF.Zpt.Rendering;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of an <see cref="IReportWriter"/> which writes Screenplay reports to a standalone HTML page.
  /// This includes a number of features for ease of reading, including limited filtering and collapsing of scenarios.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This report writer essentially works like a miniature MVC application, using ZPT-Sharp to render the report.
  /// You will find, in the <c>Views</c> directory, a number of embedded resources which make up the MVC view for
  /// presenting the report.
  /// </para>
  /// <para>
  /// The report received within the <see cref="Write"/> method of this type is passed to an instance of
  /// <see cref="ReportDocument"/>. which serves as the MVC model type.
  /// </para>
  /// <para>
  /// This class is then essentially the controller, which creates an instance of that view (as a ZPT document)
  /// renders it using ZPT-Sharp.
  /// </para>
  /// </remarks>
  public class HtmlReportWriter : IReportWriter
  {
    readonly TextWriter writer;
    readonly IObjectFormattingService formattingService;
    readonly IZptDocumentFactory documentFactory;

    /// <summary>
    /// Write the specified report to the destination.
    /// </summary>
    /// <param name="report">Report model.</param>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.HtmlReportWriter"/> class.
    /// </summary>
    /// <param name="writer">A text writer into which the report should be written.</param>
    /// <param name="formattingService">An object formatting service.</param>
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

    /// <summary>
    /// Write the report to a file path.
    /// </summary>
    /// <param name="report">The report.</param>
    /// <param name="path">Destination file path.</param>
    /// <param name="formattingService">Object formatting service.</param>
    public static void WriteToFile(Report report, string path, IObjectFormattingService formattingService = null)
    {
      using(var writer = new StreamWriter(path))
      {
        var reportWriter = new HtmlReportWriter(writer, formattingService);
        reportWriter.Write(report);
        writer.Flush();
      }
    }
  }
}
