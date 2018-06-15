//
// ScenarioMetadataAdapter.cs
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
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Adapters
{
  public class ScenarioMetadataAdapter : IProvidesScenarioMetadata
  {
    readonly IScenario scenario;

    public IFeature Feature => scenario.Feature;

    public bool? Outcome => scenario.Outcome;

    public IdAndName Name => scenario.Name;

    public IReadOnlyList<Models.IReportable> Reportables => scenario.Reportables;

    public bool IsSuccess => Outcome.HasValue && Outcome.Value;

    public bool IsFailure => Outcome.HasValue && !Outcome.Value;

    public bool IsInconclusive => !Outcome.HasValue;

    public string GetFeatureIdIfMeaningful()
    {
      if((scenario.Feature?.Name?.IsIdGenerated).GetValueOrDefault(true))
        return null;

      var id = scenario.Feature?.Name?.Id;
      if(String.IsNullOrEmpty(id) || id == GetFeatureName())
        return null;

      return id;
    }

    public string GetFeatureName()
    {
      var name = scenario.Feature?.Name?.Name;
      if(!String.IsNullOrEmpty(name)) return name;

      return scenario.Feature?.Name?.Id;
    }

    public string GetScenarioIdIfMeaningful()
    {
      if((scenario.Name?.IsIdGenerated).GetValueOrDefault(true))
        return null;

      var id = scenario.Name?.Id;
      if(String.IsNullOrEmpty(id) || id == GetScenarioName())
        return null;

      return id;
    }

    public string GetScenarioName()
    {
      var name = scenario.Name?.Name;
      if(!String.IsNullOrEmpty(name)) return name;

      return scenario.Name?.Id;
    }

    public ScenarioMetadataAdapter(IScenario scenario)
    {
      if(scenario == null)
        throw new ArgumentNullException(nameof(scenario));
      this.scenario = scenario;
    }
  }
}
