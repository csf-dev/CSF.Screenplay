//
// JsonReportReader.cs
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
  /// An implementation of <see cref="IGetsReport"/> which reads a JSON report from a <c>System.IO.TextReader</c>.
  /// </summary>
  public class JsonReportReader : IGetsReport, IDisposable
  {
    readonly JsonSerializer serializer;
    readonly TextReader reader;
    readonly bool disposeTextReader;

    /// <summary>
    /// Gets the report.
    /// </summary>
    /// <returns>The report.</returns>
    public IReport GetReport()
    {
      using(var jsonReader = new JsonTextReader(reader))
      {
        return serializer.Deserialize<Report>(jsonReader);
      }
    }

    #region IDisposable Support
    bool disposedValue;

    /// <summary>
    /// Disposes of the current instance.
    /// </summary>
    /// <returns>The dispose.</returns>
    /// <param name="disposing">If set to <c>true</c> disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if(!disposedValue)
      {
        if(disposing)
        {
          if(disposeTextReader)
            reader.Dispose();
        }

        disposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Reporting.JsonReportReader"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonReportReader"/>. The <see cref="Dispose()"/> method leaves the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonReportReader"/> in an unusable state. After calling
    /// <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonReportReader"/> so the garbage collector can reclaim the memory that
    /// the <see cref="T:CSF.Screenplay.Reporting.JsonReportReader"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.JsonReportReader"/> class.
    /// </summary>
    /// <param name="reader">Reader.</param>
    /// <param name="disposeTextReader">If set to <c>true</c> dispose text reader.</param>
    public JsonReportReader(TextReader reader, bool disposeTextReader = true)
    {
      if(reader == null)
        throw new ArgumentNullException(nameof(reader));
      
      this.reader = reader;
      this.disposeTextReader = disposeTextReader;

      serializer = new JsonSerializer();
    }

    /// <summary>
    /// Creates a report reader instance for a given file path and then immediately reads the report from that file
    /// into an object instance.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="path">The file path.</param>
    public static IReport ReadFromFile(string path)
    {
      using(var reportReader = CreateFromFile(path))
      {
        return reportReader.GetReport();
      }
    }

    /// <summary>
    /// Creates an instance of <see cref="JsonReportReader"/> from a JSON report which is at a file path.
    /// </summary>
    /// <returns>The report reader.</returns>
    /// <param name="path">The file path.</param>
    public static JsonReportReader CreateFromFile(string path)
    {
      return new JsonReportReader(new StreamReader(path));
    }
  }
}
