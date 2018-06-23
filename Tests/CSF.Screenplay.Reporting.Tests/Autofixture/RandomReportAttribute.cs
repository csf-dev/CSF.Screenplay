using System;
using System.Reflection;
using CSF.Screenplay.ReportModel;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class RandomReportAttribute : CustomizeAttribute
  {
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
      if(parameter.ParameterType != typeof(Report))
      {
        throw new InvalidOperationException($"`{nameof(RandomReportAttribute)}' is only valid for `{nameof(Report)}' parameters.");
      }

      return new RandomReportCustomization();
    }
  }
}
