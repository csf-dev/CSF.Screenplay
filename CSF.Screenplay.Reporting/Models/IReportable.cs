//
// IReportable.cs
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
namespace CSF.Screenplay.Reporting.Models
{
  public interface IReportable : IProvidesReportables
  {
    /// <summary>
    /// Gets or sets the name of the actor.
    /// </summary>
    /// <value>The name of the actor.</value>
    string ActorName { get; }

    /// <summary>
    /// Gets or sets the report.
    /// </summary>
    /// <value>The report.</value>
    string Report { get; }

    /// <summary>
    /// Gets the category (given/when/then) of the reportable represented by the current instance.
    /// </summary>
    /// <value>The reportable category.</value>
    ReportableCategory Category { get; }

    /// <summary>
    /// Gets type of reportable represented by the current instance.
    /// </summary>
    /// <value>The reportable type.</value>
    ReportableType Type { get; }

    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>The result.</value>
    string Result { get; }

    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    /// <value>The error.</value>
    string Error { get; }
  }
}
