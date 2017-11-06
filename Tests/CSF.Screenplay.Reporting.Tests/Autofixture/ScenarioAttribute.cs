using System;
using System.Reflection;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class ScenarioAttribute : CustomizeAttribute
  {
    public string FeatureId { get; set; }

    public string ScenarioId { get; set; }

    public string FeatureName { get; set; }

    public string ScenarioName { get; set; }

    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
      if(parameter.ParameterType != typeof(Scenario))
      {
        throw new InvalidOperationException($"`{nameof(ScenarioAttribute)}' is only valid for `{nameof(Scenario)}' parameters.");
      }

      return new ScenarioCustomization(FeatureId, ScenarioId, FeatureName, ScenarioName);
    }
  }
}
