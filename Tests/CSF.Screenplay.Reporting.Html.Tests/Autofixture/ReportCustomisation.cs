using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class ReportCustomisation : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
             .ToList()
             .ForEach(x => fixture.Behaviors.Remove(x));
      
      new FeatureCustomisation().Customize(fixture);
      fixture.Customize<Report>(builder => builder.FromFactory<IList<Feature>>(CreateReport));
    }

    Report CreateReport(IList<Feature> features) => new Report(features.ToArray());
  }
}
