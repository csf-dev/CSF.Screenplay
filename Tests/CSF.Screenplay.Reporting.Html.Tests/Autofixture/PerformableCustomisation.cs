using System;
using CSF.Screenplay.Performables;
using Ploeh.AutoFixture;
using Moq;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class PerformableCustomisation : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      fixture.Customize<IPerformable>(builder => builder.FromFactory<string>(CreatePerformable));
    }

    IPerformable CreatePerformable(string id)
    {
      var output = new Mock<IPerformable>();
      output
        .Setup(x => x.GetReport(It.IsAny<INamed>()))
        .Returns((INamed actor) => $"{actor.Name} executes performable {id}");
      return output.Object;
    }
  }
}
