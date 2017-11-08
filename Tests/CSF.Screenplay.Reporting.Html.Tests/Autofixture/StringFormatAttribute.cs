using System;
using System.Reflection;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class StringFormatAttribute : CustomizeAttribute
  {
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
      if(parameter.ParameterType != typeof(IObjectFormattingService))
      {
        throw new InvalidOperationException($"`{nameof(StringFormatAttribute)}' is only valid for `{nameof(IObjectFormattingService)}' parameters.");
      }

      return new StringFormatCustomisation();
    }

    public class StringFormatCustomisation : ICustomization
    {
      public void Customize(IFixture fixture)
      {
        fixture.Customize<IObjectFormattingService>(builder => {
          return builder
            .FromFactory(() => Mock.Of<IObjectFormattingService>())
            .Do(service => {
              Mock.Get(service)
                  .Setup(x => x.Format(It.IsAny<object>()))
                  .Returns((object obj) => obj?.ToString());
            });
        });
      }
    }
  }
}
