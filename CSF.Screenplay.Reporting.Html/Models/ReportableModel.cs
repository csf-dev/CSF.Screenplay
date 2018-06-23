//
// ReportableModel.cs
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
using System.Linq;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Model for HTML reportables (wraps a <see cref="Reportable"/> instance).
  /// </summary>
  public class ReportableModel
  {
    readonly IReportable reportable;

    #region common to all reportables

    /// <summary>
    /// Gets the actor.
    /// </summary>
    /// <value>The actor.</value>
    public virtual string ActorName => reportable.ActorName;

    /// <summary>
    /// Gets the type of the performance.
    /// </summary>
    /// <value>The type of the performance.</value>
    public virtual string PerformanceType
    {
      get {
        if(reportable.Category.IsDefinedValue())
          return reportable.Category.ToString();

        return String.Empty;
      }
    }

    /// <summary>
    /// Gets the outcome of the reported-upon action.
    /// </summary>
    /// <value>The outcome.</value>
    public virtual ReportableType Outcome => reportable.Type;

    #endregion

    #region performances

    /// <summary>
    /// Gets the contained reportables.
    /// </summary>
    /// <value>The reportables.</value>
    public virtual IReadOnlyList<ReportableModel> Reportables
      => reportable.Reportables.Select(x => new ReportableModel(x)).ToArray();

    /// <summary>
    /// Gets a value indicating whether this performance has any child reportables or not.
    /// </summary>
    /// <value><c>true</c> if this performance has child reportables; otherwise, <c>false</c>.</value>
    public virtual bool HasReportables => reportable.Reportables.Any();

    /// <summary>
    /// Gets a value indicating whether this performance has a result.
    /// </summary>
    /// <value><c>true</c> if this performance has a result; otherwise, <c>false</c>.</value>
    public virtual bool HasResult => reportable.Type == ReportableType.SuccessWithResult;

    /// <summary>
    /// Gets a value indicating whether this performance has an exception.
    /// </summary>
    /// <value><c>true</c> if this performance has an exception; otherwise, <c>false</c>.</value>
    public virtual bool HasException
      => reportable.Type == ReportableType.FailureWithError && reportable.Error != null;

    /// <summary>
    /// Gets a value indicating whether this performance has an exception and has no children.
    /// </summary>
    /// <value><c>true</c> if has an exception but no children; otherwise, <c>false</c>.</value>
    public virtual bool ShouldReportFailure => !HasReportables;

    /// <summary>
    /// Gets a value indicating whether this performance has additional content (child reportables, a result or an
    /// exception).
    /// </summary>
    /// <value><c>true</c> if this performance has additional content; otherwise, <c>false</c>.</value>
    public virtual bool HasAdditionalContent
      => HasReportables || HasResult || HasException;

    /// <summary>
    /// Gets the result received from the performable.
    /// </summary>
    /// <value>The result.</value>
    public virtual string Result => reportable.Result;

    /// <summary>
    /// Gets an exception raised by the performable.
    /// </summary>
    /// <value>The exception.</value>
    public virtual string Error => reportable.Error;

    #endregion

    #region additional functionality

    /// <summary>
    /// Gets the HTML class attribute value indicating the outcome of a given reportable step within a scenario.
    /// </summary>
    /// <returns>The reportable's outcome class.</returns>
    public string GetOutcomeClass()
    {
      switch(reportable.Type)
      {
      case ReportableType.Success:
      case ReportableType.SuccessWithResult:
        return GetOutcomeClass(true, false);

      default:
        return GetOutcomeClass(false, true);
      }
    }

    /// <summary>
    /// Gets an HTML class attribute value which describes a reportable step within a scenario.
    /// </summary>
    /// <returns>The reportable class.</returns>
    public string GetReportableClass()
    {
      if(reportable.Type == ReportableType.GainAbility)
        return ReportConstants.AbilityClass;

      return ReportConstants.PerformanceClass;
    }

    /// <summary>
    /// Gets an HTML class attribute which describes whether a reportable has any child content or not.
    /// </summary>
    /// <returns>The content class.</returns>
    public string GetContentClass()
    {
      return HasAdditionalContent? "has_content" : "empty";
    }

    /// <summary>
    /// Gets the string report for an ability.
    /// </summary>
    /// <returns>The ability report.</returns>
    public string GetAbilityReport() => reportable.Report;

    /// <summary>
    /// Gets the string report for a performance.
    /// </summary>
    /// <returns>The performance report.</returns>
    public string GetPerformanceReport() => reportable.Report;

    /// <summary>
    /// Formats a given object using a formatting service.
    /// </summary>
    public string GetFormattedException() => Error;

    /// <summary>
    /// Gets a value indicating whether or not an exception can be formatted.
    /// </summary>
    /// <returns><c>true</c>, if exception can be formatted, <c>false</c> otherwise.</returns>
    public bool CanFormatException() => false;

    /// <summary>
    /// Gets the name of the METAL macro to use for rendering the specified reportable.
    /// </summary>
    /// <returns>The macro name.</returns>
    public string GetMacroName()
    {
      switch(reportable.Type)
      {
      case ReportableType.GainAbility:
        return ReportConstants.AbilityMacro;

      case ReportableType.SuccessWithResult:
        return ReportConstants.PerformanceWithResultMacro;

      case ReportableType.FailureWithError:
        return ReportConstants.PerformanceWithExceptionMacro;

      default:
        return ReportConstants.PerformanceMacro;
      }
    }

    string GetOutcomeClass(bool success, bool failure)
    {
      if(success) return ReportConstants.SuccessClass;
      if(failure) return ReportConstants.FailureClass;
      return ReportConstants.InconclusiveClass;
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.ReportableModel"/> class.
    /// </summary>
    /// <param name="reportable">Reportable.</param>
    public ReportableModel(IReportable reportable)
    {
      if(reportable == null)
        throw new ArgumentNullException(nameof(reportable));

      this.reportable = reportable;
    }
  }
}
