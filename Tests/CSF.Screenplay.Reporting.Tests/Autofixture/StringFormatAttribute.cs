using System;
using System.Reflection;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;
using CSF.Screenplay.ReportFormatting;

namespace CSF.Screenplay.Reporting.Tests.Autofixture
{
  public class StringFormatAttribute : CustomizeAttribute
  {
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
      if(parameter.ParameterType != typeof(IFormatsObjectForReport))
      {
        throw new InvalidOperationException($"`{nameof(StringFormatAttribute)}' is only valid for `{nameof(IFormatsObjectForReport)}' parameters.");
      }

      return new StringFormatCustomisation();
    }

    public class StringFormatCustomisation : ICustomization
    {
      public void Customize(IFixture fixture)
      {
        fixture.Customize<IFormatsObjectForReport>(builder => {
          return builder.FromFactory(() => new DefaultObjectFormattingStrategy());
        });
      }
    }
  }
}
