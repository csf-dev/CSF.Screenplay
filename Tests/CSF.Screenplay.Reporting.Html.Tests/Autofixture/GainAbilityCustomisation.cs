using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting.Models;
using Moq;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Html.Tests.Autofixture
{
  public class GainAbilityCustomisation : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      fixture.Customize<GainAbility>(builder => builder.FromFactory<PerformanceOutcome,INamed,IAbility>(CreateGainAbility));
    }

    GainAbility CreateGainAbility(PerformanceOutcome outcome,
                                  INamed actor,
                                  IAbility ability)
    {
      return new GainAbility(actor, outcome, ability, PerformanceType.Given);
    }
  }
}
