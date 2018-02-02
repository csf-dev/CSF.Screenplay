using System;
using CSF.Screenplay.Actors;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class StageSteps
  {
    readonly ICast cast;
    readonly IStage stage;

    IActor retrievedActor;

    [Given("Joe is an actor in the spotlight")]
    public void GivenJoeIsAnActorInTheSpotlight()
    {
      var joe = cast.Get("Joe");
      stage.ShineTheSpotlightOn(joe);
    }

    [When("I get the actor in the spotlight")]
    public void WhenIGetTheActorInTheSpotlight()
    {
      retrievedActor = stage.GetTheActorInTheSpotlight();
    }

    [Then("that actor should be the same as Joe")]
    public void ThenThatActorShouldBeJoe()
    {
      var joe = cast.Get("Joe");
      Assert.That(retrievedActor, Is.SameAs(joe));
    }

    public StageSteps(ICast cast, IStage stage)
    {
      if(stage == null)
        throw new ArgumentNullException(nameof(stage));
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));

      this.cast = cast;
      this.stage = stage;
    }
  }
}
