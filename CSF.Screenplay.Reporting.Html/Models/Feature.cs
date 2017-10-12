using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  public class Feature
  {
    public string Identifier { get; set; }

    public string Name { get; set; }

    public IReadOnlyList<Scenario> Scenarios { get; set; }

    public bool IsInconclusive => Scenarios.Any(x => x.IsInconclusive);

    public bool IsFailure => Scenarios.Any(x => x.IsFailure);

    public bool IsSuccess => Scenarios.All(x => x.IsSuccess);
  }
}
