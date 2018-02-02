using System;
using CSF.Screenplay.Actors;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class StageSteps
  {
    readonly IScenario ctx;
    IActor retrievedActor;

    [Given("Joe is an actor in the spotlight")]
    public void GivenJoeIsAnActorInTheSpotlight()
    {
      var joe = ctx.CreateActor("Joe");
      var cast = ctx.GetCast();
      cast.Add(joe);

      var stage = ctx.GetStage();
      stage.ShineTheSpotlightOn(joe);
    }

    [When("I get the actor in the spotlight")]
    public void WhenIGetTheActorInTheSpotlight()
    {
      var stage = ctx.GetStage();
      retrievedActor = stage.GetTheActorInTheSpotlight();
    }

    [Then("that actor should be the same as Joe")]
    public void ThenThatActorShouldBeJoe()
    {
      var joe = ctx.GetCast().GetExisting("Joe");
      Assert.That(retrievedActor, Is.SameAs(joe));
    }

    public StageSteps(IScenario ctx)
    {
      if(ctx == null)
        throw new ArgumentNullException(nameof(ctx));

      this.ctx = ctx;
    }
  }
}
