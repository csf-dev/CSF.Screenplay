//
// JsonScenarioRenderer.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSF.Screenplay.Reporting.Builders;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
  /// <summary>
  /// An implementation of <see cref="IObservesScenarioCompletion"/> which writes the completed scenarios to JSON.
  /// </summary>
  public class JsonScenarioRenderer : IObservesScenarioCompletion, IDisposable
  {
    readonly ISet<Task> scenarioTasks;
    readonly IGetsReportMetadata metadataFactory;
    readonly IWritesReportOneScenarioAtATime writer;
    readonly object beginReportLock = new object();

    /// <summary>
    /// Subscribes to an object which exposes completed scenarios.
    /// </summary>
    /// <param name="scenarioProvider">Scenario provider.</param>
    public void Subscribe(IExposesCompletedScenarios scenarioProvider)
    {
      if(scenarioProvider == null)
        throw new ArgumentNullException(nameof(scenarioProvider));

      scenarioProvider.ScenarioCompleted += OnScenarioCompleted;
      scenarioProvider.TestRunCompleted += OnTestRunCompleted;
    }

    /// <summary>
    /// Unsubscribes from an object which exposes completed scenarios.
    /// </summary>
    /// <param name="scenarioProvider">Scenario provider.</param>
    public void Unsubscribe(IExposesCompletedScenarios scenarioProvider)
    {
      if(scenarioProvider == null)
        throw new ArgumentNullException(nameof(scenarioProvider));

      scenarioProvider.ScenarioCompleted -= OnScenarioCompleted;
      scenarioProvider.TestRunCompleted -= OnTestRunCompleted;
    }

    void QueueWriteScenario(Scenario scenario)
    {
      var writeTask = new Task(() => WriteScenario(scenario));
      scenarioTasks.Add(writeTask);
      writeTask.Start();
    }

    void WriteScenario(Scenario scenario)
    {
      lock(beginReportLock)
      {
        if(!writer.HasBegun) BeginReport();
      }

      writer.WriteScenario(scenario);
    }

    void OnScenarioCompleted(object sender, EventArgs ev)
    {
      var args = ev as ScenarioCompletedEventArgs;
      if(args?.Scenario == null) return;
      QueueWriteScenario(args.Scenario);
    }

    void OnTestRunCompleted(object sender, EventArgs ev)
    {
      Task.WaitAll(scenarioTasks.ToArray());

      lock(beginReportLock)
      {
        if(!writer.HasBegun) BeginReport();
      }

      writer.EndReport();
    }

    void BeginReport()
    {
      var metadata = metadataFactory.GetReportMetadata();
      writer.BeginReport(metadata);
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
          writer.Dispose();

        disposedValue = true;
      }
    }

    /// <summary>
    /// Releases all resource used by the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioRenderer"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose()"/> when you are finished using the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonScenarioRenderer"/>. The <see cref="Dispose()"/> method leaves the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonScenarioRenderer"/> in an unusable state. After calling
    /// <see cref="Dispose()"/>, you must release all references to the
    /// <see cref="T:CSF.Screenplay.Reporting.JsonScenarioRenderer"/> so the garbage collector can reclaim the memory
    /// that the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioRenderer"/> was occupying.</remarks>
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.JsonScenarioRenderer"/> class.
    /// </summary>
    /// <param name="writer">Writer.</param>
    /// <param name="metadataFactory">A report metadata factory</param>
    public JsonScenarioRenderer(IWritesReportOneScenarioAtATime writer, IGetsReportMetadata metadataFactory = null)
    {
      if(writer == null)
        throw new ArgumentNullException(nameof(writer));
      this.writer = writer;
      this.metadataFactory = metadataFactory ?? new ReportMetadataFactory();
      scenarioTasks = new HashSet<Task>();
    }

    /// <summary>
    /// Creates an instance of <see cref="JsonScenarioRenderer"/> for writing to a given file path.
    /// </summary>
    /// <param name="path">Destination file path.</param>
    public static JsonScenarioRenderer CreateForFile(string path)
    {
      var writer = JsonScenarioAtATimeReportWriter.CreateForFile(path);
      return new JsonScenarioRenderer(writer);
    }
  }
}
