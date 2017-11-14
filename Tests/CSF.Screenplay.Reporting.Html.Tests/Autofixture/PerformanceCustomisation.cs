using System;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class PerformanceCustomisation : ICustomization
  {
    readonly ISpecimenBuilder performanceBuilder;

    public void Customize(IFixture fixture)
    {
      fixture.Customize<Performance>(builder => builder.FromFactory(performanceBuilder));
    }

    public PerformanceCustomisation()
    {
      performanceBuilder = new PerformanceSpecimenBuilder();
    }
  }
}
