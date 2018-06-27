using System;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class ReportableCustomisation : ICustomization
  {
    readonly ISpecimenBuilder performanceBuilder;

    public void Customize(IFixture fixture)
    {
      fixture.Customizations.Insert(0, performanceBuilder);
    }

    public ReportableCustomisation()
    {
      performanceBuilder = new ReportableSpecimenBuilder();
    }
  }
}
