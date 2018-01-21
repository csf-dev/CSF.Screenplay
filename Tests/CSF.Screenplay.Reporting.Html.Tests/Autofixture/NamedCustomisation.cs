using System;
using CSF.Screenplay.Actors;
using Moq;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class NamedCustomisation : ICustomization
  {
    static readonly string[] Names = {
      "Joe",
      "Bob",
      "Jane",
      "Anne",
      "Siobahn",
      "Youssef",
      "Wong",
      "Nancy",
      "Jasmine",
      "Julian"
    };

    public void Customize(IFixture fixture)
    {
      fixture.Customize<INamed>(builder => builder.FromFactory(CreateNamed));
    }

    INamed CreateNamed()
    {
      var name = Names[ScenarioCustomisation.Randomiser.Next(0, Names.Length)];
      return Mock.Of<INamed>(x => x.Name == name);
    }
  }
}
