using System;
using System.Collections.Generic;
using CSF.Screenplay.Reporting.Tests.Autofixture;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.Screenplay.Reporting.Tests
{
  public class AutoMoqDataAttribute : AutoDataAttribute
  {
    static IFixture GetFixture()
    {
      IFixture fixture = new Fixture();
      var customizations = GetCustomizations();

      foreach(var customization in customizations)
        fixture = fixture.Customize(customization);

      return fixture;
    }

    static IEnumerable<ICustomization> GetCustomizations()
    {
      return new ICustomization[] {
        new AutoMoqCustomization(),
        new ReportBuilderDependenciesCustomisation(),
      };
    }

    public AutoMoqDataAttribute() : base(GetFixture())
    {
    }
  }
}
