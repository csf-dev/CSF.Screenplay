using System;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class NamedScenarioCustomization : ICustomization
  {
    readonly string featureId, scenarioId, featureName, scenarioName;

    public void Customize(IFixture fixture)
    {
      fixture.Customize<Scenario>(builder => {
        return builder.FromFactory((string sId, string sName, string fId, string fName) => { 
          return new Scenario(scenarioId?? sId,
                              scenarioName?? sName,
                              featureName?? fName,
                              featureId?? fId);
        });
      });
    }

    public NamedScenarioCustomization(string featureId, string scenarioId, string featureName, string scenarioName)
    {
      this.scenarioName = scenarioName;
      this.featureName = featureName;
      this.scenarioId = scenarioId;
      this.featureId = featureId;
    }
  }
}
