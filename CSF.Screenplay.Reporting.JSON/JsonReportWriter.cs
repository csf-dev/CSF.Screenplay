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
using CSF.Screenplay.Reporting.Models;
using Newtonsoft.Json;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// Implementation of an <see cref="IReportWriter"/> which writes Screenplay reports as JSON data.
  /// </summary>
  public class JsonReportWriter : IReportWriter
  {
    readonly JsonSerializer serializer;
    readonly TextWriter writer;

    /// <summary>
    /// Write the specified report to the destination.
    /// </summary>
    /// <param name="reportModel">Report model.</param>
    public void Write(IReport reportModel)
    {
      serializer.Serialize(writer, reportModel);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.JsonReportWriter"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    public JsonReportWriter(TextWriter writer)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));
      
      this.writer = writer;

      serializer = new JsonSerializer();
    }

    /// <summary>
    /// Write the report to a file path.
    /// </summary>
    /// <param name="report">The report.</param>
    /// <param name="path">Destination file path.</param>
    public static void WriteToFile(IReport report, string path)
    {
      using(var writer = new StreamWriter(path))
      {
        var reportWriter = new JsonReportWriter(writer);
        reportWriter.Write(report);
        writer.Flush();
      }
    }
  }
}
