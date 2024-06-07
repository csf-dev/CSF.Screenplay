//
// JsonScenarioAtATimeReportWriter.cs
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
  /// This object writes a report one object at a time (typically as the report is being 
  /// </summary>
  public class JsonScenarioAtATimeReportWriter : IWritesReportOneScenarioAtATime, IDisposable
  {
    readonly JsonSerializer serializer;
    readonly JsonTextWriter jsonWriter;
    readonly TextWriter writer;
    readonly bool disposeWriter;
    bool hasReportBegun, hasReportEnded;
    readonly object writeLock = new object();

    /// <summary>
    /// Gets a value indicating whether the report has begun writing.
    /// </summary>
    /// <value>
    /// <c>true</c> if the report has begun; otherwise, <c>false</c>.</value>
    public bool HasBegun => hasReportBegun;

    /// <summary>
    /// Gets a value indicating whether the report has finished writing.
    /// </summary>
    /// <value>
    /// <c>true</c> if the report has ended; otherwise, <c>false</c>.</value>
    public bool HasEnded => hasReportEnded;

    /// <summary>
    /// Begins the report.
    /// </summary>
    /// <param name="metadata">Metadata.</param>
    public void BeginReport(ReportMetadata metadata)
    {
      if(metadata == null)
        throw new ArgumentNullException(nameof(metadata));
      AssertNotEnded();
      AssertNotBegun();

      lock(writeLock)
      {
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(nameof(Report.Metadata));
        serializer.Serialize(jsonWriter, metadata);
        jsonWriter.WritePropertyName(nameof(Report.Scenarios));
        jsonWriter.WriteStartArray();

        hasReportBegun = true;
      }
    }

    /// <summary>
    /// Ends the report.
    /// </summary>
    public void EndReport()
    {
      AssertHasBegun();
      AssertNotEnded();

      lock(writeLock)
      {
        jsonWriter.WriteEndArray();
        jsonWriter.WriteEndObject();
      }

      hasReportEnded = true;

      jsonWriter.Flush();
    }

    /// <summary>
    /// Writes the scenario.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    public void WriteScenario(Scenario scenario)
    {
      AssertHasBegun();
      AssertNotEnded();

      lock(writeLock)
      {
        serializer.Serialize(jsonWriter, scenario);
      }
    }

    void AssertNotEnded()
    {
      if(hasReportEnded)
        throw new InvalidOperationException("The report may not be modified after it has ended.");
    }

    void AssertHasBegun()
    {
      if(!hasReportBegun)
        throw new InvalidOperationException("The report must be begun before scenarios or an ending is written.");
    }

    void AssertNotBegun()
    {
      if(hasReportBegun)
        throw new InvalidOperationException("The report may only begin once.");
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
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioAtATimeReportWriter"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonScenarioAtATimeReportWriter"/>. The <see cref="Dispose()"/> method
    /// leaves the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioAtATimeReportWriter"/> in an unusable state. After
    /// calling <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonScenarioAtATimeReportWriter"/> so the garbage collector can reclaim
    /// the memory that the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioAtATimeReportWriter"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioAtATimeReportWriter"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    /// <param name="disposeWriter">If set to <c>true</c> dispose writer.</param>
    public JsonScenarioAtATimeReportWriter(TextWriter writer, bool disposeWriter = true)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));
      
      this.writer = writer;
      this.disposeWriter = disposeWriter;

      jsonWriter = new JsonTextWriter(writer);
      serializer = new JsonSerializer();
    }

    /// <summary>
    /// Creates an instance of <see cref="JsonScenarioAtATimeReportWriter"/> for writing to a given file path.
    /// </summary>
    /// <param name="path">Destination file path.</param>
    public static JsonScenarioAtATimeReportWriter CreateForFile(string path)
    {
      var writer = new StreamWriter(path);
      return new JsonScenarioAtATimeReportWriter(writer);
    }
  }
}
