using System;
using System.IO;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.ReportModel;
using CSF.Zpt;
using CSF.Zpt.Rendering;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of <see cref="IRendersReport"/> which writes Screenplay reports to a standalone HTML page.
  /// This includes a number of features for ease of reading, including limited filtering and collapsing of scenarios.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This report writer essentially works like a miniature MVC application, using ZPT-Sharp to render the report.
  /// You will find, in the <c>Views</c> directory, a number of embedded resources which make up the MVC view for
  /// presenting the report.
  /// </para>
  /// <para>
  /// The report received within the <see cref="Render"/> method of this type is passed to an instance of
  /// <see cref="ReportDocument"/>. which serves as the MVC model type.
  /// </para>
  /// <para>
  /// This class is then essentially the controller, which creates an instance of that view (as a ZPT document)
  /// renders it using ZPT-Sharp.
  /// </para>
  /// </remarks>
  public class HtmlReportRenderer : IRendersReport, IDisposable
  {
    readonly TextWriter writer;
    readonly IZptDocumentFactory documentFactory;
    readonly bool disposeWriter;

    /// <summary>
    /// Write the specified report to the destination.
    /// </summary>
    /// <param name="report">Report model.</param>
    public void Render(IReport report)
    {
      var doc = GetDocument();
      var emptyDoc = GetDocument();
      var model = GetDocumentModel(report, emptyDoc);

      doc.Render(model, writer);

      writer.Flush();
    }

    IZptDocument GetDocument()
    {
      using(var stream = GetDocumentStream())
      {
        return documentFactory.CreateDocument(stream, RenderingMode.Html);
      }
    }

    ReportDocument GetDocumentModel(IReport reportModel, IZptDocument document)
      => new ReportDocument(reportModel, document);

    Stream GetDocumentStream()
      => Views.ViewProvider.GetDocumentTemplate();

    #region IDisposable Support
    bool disposedValue = false;

    /// <summary>
    /// Dispose of the current instance
    /// </summary>
    /// <param name="disposing">If set to <c>true</c> then this disposal is explicit.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposedValue)
      {
        if(disposing)
        {
          if(disposeWriter)
            writer.Dispose();
        }

        disposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/>. The <see cref="Dispose()"/> method leaves the
    /// <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> in an unusable state. After calling
    /// <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> so the garbage collector can reclaim the memory that
    /// the <see cref="T:CSF.Screenplay.Reporting.TextReportWriter"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.HtmlReportWriter"/> class.
    /// </summary>
    /// <param name="writer">A text writer into which the report should be written.</param>
    /// <param name="disposeWriter">Indicates whether or not the <paramref name="writer"/> should be diposed with this instance.</param>
    public HtmlReportRenderer(TextWriter writer, bool disposeWriter = true)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));

      this.writer = writer;
      this.disposeWriter = disposeWriter;
      var pluginConfig = new HardCodedZptSharpConfigurationProvider();
      documentFactory = new ZptDocumentFactory(new ZptDocumentProviderService(pluginConfig));
    }

    /// <summary>
    /// Write the report to a file path.
    /// </summary>
    /// <param name="report">The report.</param>
    /// <param name="path">Destination file path.</param>
    public static void WriteToFile(IReport report, string path)
    {
      using(var reportWriter = new HtmlReportRenderer(new StreamWriter(path)))
      {
        reportWriter.Render(report);
      }
    }
  }
}
