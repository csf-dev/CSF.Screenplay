using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using Moq;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class AbilityCustomisation : ICustomization
  {
    static readonly string[] Names = {
      "fly",
      "swim",
      "run",
      "walk",
      "sing",
      "dance",
      "hop",
      "skip",
      "jump",
      "climb"
    };

    public void Customize(IFixture fixture)
    {
      fixture.Customize<IAbility>(builder => builder.FromFactory(CreateAbility));
    }

    IAbility CreateAbility()
    {
      var abilityName = Names[ScenarioCustomisation.Randomiser.Next(0, Names.Length)];

      var ability = new Mock<IAbility>();
      ability
        .Setup(x => x.GetReport(It.IsAny<INamed>()))
        .Returns((INamed a) => $"{a.Name} is able to {abilityName}");

      return ability.Object;
    }
  }
}
