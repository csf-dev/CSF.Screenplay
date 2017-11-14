using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class FeatureCustomisation : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      new ScenarioCustomisation().Customize(fixture);
      fixture.Customize<Feature>(builder => builder.FromFactory<string,string,IList<Scenario>>(CreateFeature));
    }

    Feature CreateFeature(string id, string name, IList<Scenario> scenarios)
      => new Feature($"FeatureId {id}", $"Feature {name}", scenarios.ToArray());
  }
}
