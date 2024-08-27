//
// IWritesScenariosToReport.cs
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
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An object which writes a report, one scenario at a time.
  /// </summary>
  public interface IWritesReportOneScenarioAtATime : IDisposable
  {
    /// <summary>
    /// Gets a value indicating whether the report has begun writing.
    /// </summary>
    /// <value><c>true</c> if the report has begun; otherwise, <c>false</c>.</value>
    bool HasBegun { get; }

    /// <summary>
    /// Gets a value indicating whether the report has finished writing.
    /// </summary>
    /// <value><c>true</c> if the report has ended; otherwise, <c>false</c>.</value>
    bool HasEnded { get; }

    /// <summary>
    /// Begins the report.
    /// </summary>
    /// <param name="metadata">Metadata.</param>
    void BeginReport(ReportMetadata metadata);

    /// <summary>
    /// Writes the scenario.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    void WriteScenario(Scenario scenario);

    /// <summary>
    /// Ends the report.
    /// </summary>
    void EndReport();
  }
}
