//
// HierarchicalReportVisitor.cs
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
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Adapters
{
  public class HierarchicalReportAdapter : IProvidesHierarchicalFeatures
  {
    readonly object syncRoot = new object();
    readonly IReport report;
    IReadOnlyCollection<IHierarchicalFeature> cachedFeatures;

    public IReadOnlyCollection<IHierarchicalFeature> GetFeatures()
    {
      lock(syncRoot)
      {
        if(cachedFeatures == null)
          cachedFeatures = GenerateFeatures();
      }

      return cachedFeatures;
    }

    IReadOnlyCollection<IHierarchicalFeature> GenerateFeatures()
    {
      var allFeatureIds = report.Scenarios
        .Select(x => x?.Feature?.Name?.Id)
        .Where(x => !String.IsNullOrEmpty(x))
        .Distinct();

      return (from id in allFeatureIds
              let feature = report.Scenarios.First(x => x?.Feature?.Name?.Id == id).Feature
              select new HierarchicalFeature(feature, report))
        .ToArray();
    }

    public HierarchicalReportAdapter(IReport report)
    {
      if(report == null)
        throw new ArgumentNullException(nameof(report));
      this.report = report;
    }
	}
}
