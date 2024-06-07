//
// JsonReportWriter.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using CSF.Screenplay.ReportModel;
using Newtonsoft.Json;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An <see cref="IRendersReport"/> which writes the report to a <c>System.IO.TextWriter</c>, be that a text file or
  /// an output stream.  This writer uses a JSON-based format matching the report model in the namespace
  /// <c>CSF.Screenplay.ReportModel</c>.
  /// </summary>
  public class JsonReportRenderer : IRendersReport, IDisposable
  {
    readonly JsonSerializer serializer;
    readonly TextWriter writer;
    readonly bool disposeWriter;

    /// <summary>
    /// Write the specified report to the destination.
    /// </summary>
    /// <param name="reportModel">Report model.</param>
    public void Render(IReport reportModel)
    {
      if(reportModel == null)
        throw new ArgumentNullException(nameof(reportModel));
      
      serializer.Serialize(writer, reportModel);
      writer.Flush();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.JsonReportWriter"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    /// <param name="disposeWriter">Indicates whether or not the <paramref name="writer"/> should be diposed with this instance.</param>
    public JsonReportRenderer(TextWriter writer, bool disposeWriter = true)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));
      
      this.writer = writer;
      this.disposeWriter = disposeWriter;

      serializer = new JsonSerializer();
    }

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
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Reporting.JsonReportRenderer"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonReportRenderer"/>. The <see cref="Dispose()"/> method leaves the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonReportRenderer"/> in an unusable state. After calling
    /// <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonReportRenderer"/> so the garbage collector can reclaim the memory that
    /// the <see cref="T:CSF.Screenplay.Reporting.JsonReportRenderer"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Write the report to a file path.
    /// </summary>
    /// <param name="report">The report.</param>
    /// <param name="path">Destination file path.</param>
    public static void WriteToFile(IReport report, string path)
    {
      using(var reportWriter = new JsonReportRenderer(new StreamWriter(path)))
      {
        reportWriter.Render(report);
      }
    }
  }
}
