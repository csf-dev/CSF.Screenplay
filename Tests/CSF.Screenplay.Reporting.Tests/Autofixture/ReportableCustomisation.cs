using System;
using CSF.Screenplay.Reporting.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class ReportableCustomisation : ICustomization
  {
    readonly ISpecimenBuilder performanceBuilder;

    public void Customize(IFixture fixture)
    {
      fixture.Customize<Reportable>(builder => builder.FromFactory(performanceBuilder));
    }

    public ReportableCustomisation()
    {
      performanceBuilder = new ReportableSpecimenBuilder();
    }
  }
}
